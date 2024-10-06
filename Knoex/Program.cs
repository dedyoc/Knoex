using Knoex.Data;
using Knoex.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Knoex.ActionFilter;
using Microsoft.AspNetCore.Mvc;
using Knoex.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// var connectionString = builder.Configuration.GetConnectionString("PostgreSQL"); // Use this for Docker

// For local development (during testing before Docker):
var connectionString = "Host=localhost;Port=5432;Database=knoex;Username=knoex;Password=knoex;";

#region Configure services
builder.Services.AddDbContext<KnoexContext>(options =>
{
    options.UseNpgsql(connectionString);
    options.UseSnakeCaseNamingConvention();
});

builder.Services.AddIdentity<User, Role>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<KnoexContext>();

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

builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-CSRF-TOKEN";
    options.FormFieldName = "csrf";
    options.Cookie.Name = "csrf";
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Strict;
});

builder.Services.AddMvc(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
    options.Filters.Add<ViewBagActionFilter>();
});

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddHttpContextAccessor();
#endregion

#region Configure app
var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
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
#endregion

app.Run();