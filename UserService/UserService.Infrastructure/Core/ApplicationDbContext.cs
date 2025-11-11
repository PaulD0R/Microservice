using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Core;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {   
        const string ADMIN_ID = "B9C6AA99-E98D-4E3D-87DE-C93E17592919";
        const string USER_ID = "19EF95CD-413A-49EA-B4F1-448E9D86D81C";

        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        List<IdentityRole> roles =
        [
            new()
            {   
                Id = Guid.Parse(ADMIN_ID).ToString(),
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.Parse(ADMIN_ID).ToString()
            },

            new()
            {
                Id = Guid.Parse(USER_ID).ToString(),
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = Guid.Parse(USER_ID).ToString()
            }
        ];

        builder.Entity<IdentityRole>().HasData(roles);
    }
}