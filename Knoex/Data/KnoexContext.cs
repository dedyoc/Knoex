using Knoex.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Knoex.Data
{
    public class KnoexContext : IdentityDbContext<User, Role, int>
    {
        public KnoexContext(DbContextOptions<KnoexContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(entity => entity.ToTable(name: "users"));
            builder.Entity<User>().Ignore(c => c.PhoneNumber).Ignore(c => c.PhoneNumberConfirmed).Ignore(c => c.TwoFactorEnabled);
            builder.Entity<Role>(entity => entity.ToTable(name: "roles"));
            builder.Entity<IdentityUserClaim<int>>(entity => entity.ToTable(name: "user_claims"));
            builder.Entity<IdentityUserLogin<int>>(entity => entity.ToTable(name: "user_logins"));
            builder.Entity<IdentityRoleClaim<int>>(entity => entity.ToTable(name: "role_claims"));
            builder.Entity<IdentityUserRole<int>>(entity => entity.ToTable(name: "user_roles"));
            builder.Entity<IdentityUserToken<int>>(entity => entity.ToTable(name: "user_tokens"));

            builder.Entity<Post>().ToTable("post");

            builder.Entity<Post>().HasOne(p => p.AcceptedAnswer).WithOne()
                .HasForeignKey<Post>(p => p.AcceptedAnswerId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Post>().HasMany(p => p.Answers).WithOne(p => p.Parent)
                .HasForeignKey(p => p.ParentId).OnDelete(DeleteBehavior.Restrict);
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public override DbSet<User>? Users { get; set; }
    }
}