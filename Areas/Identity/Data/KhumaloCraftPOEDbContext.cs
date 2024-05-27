using KhumaloCraftPOE.Areas.Identity.Data;
using KhumaloCraftPOE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace KhumaloCraftPOE.Data;

public class KhumaloCraftPOEDbContext : IdentityDbContext<User>
{
    public KhumaloCraftPOEDbContext(DbContextOptions<KhumaloCraftPOEDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());

        builder.Entity<Order>()
            .HasOne(c => c.User)  // Defines a one-to-one or one-to-many relationship
            .WithMany(t => t.Orders);
            
    }
}

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(100);
        builder.Property(u => u.LastName).HasMaxLength(100);
    }
}


