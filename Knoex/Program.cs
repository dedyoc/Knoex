using Knoex.Data;
using Knoex.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// var connectionString = builder.Configuration.GetConnectionString("PostgreSQL"); // Use this for Docker

// For local development (during testing before Docker):
var connectionString = "Host=localhost;Port=5432;Database=knoex;Username=knoex;Password=knoex;";

builder.Services.AddDbContext<KnoexContext>(options => // Use your DbContext name
    {
        options.UseNpgsql(connectionString);
        options.UseSnakeCaseNamingConvention();
    });

builder.Services.AddIdentity<User, Role>(options => options.SignIn.RequireConfirmedAccount = true) // Use your User and Role model classes
    .AddEntityFrameworkStores<KnoexContext>(); // Use your DbContext name
builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
});
builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/login";
                options.LogoutPath = "/logout";
                options.Cookie.Name = "session";
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Strict;
            });
var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Add this line!  Essential for Identity
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<KnoexContext>();
        context.Database.Migrate();
        logger.LogInformation("Database connection successful.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while connecting to the database.");
    }
}

app.Run();