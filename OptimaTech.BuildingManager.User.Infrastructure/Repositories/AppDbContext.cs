using Microsoft.EntityFrameworkCore;
using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Core.Entities;

namespace OptimaTech.BuildingManager.User.Infrastructure.Repositories;

public class AppDbContext : DbContext
{
    public DbSet<TenantApplicationModel> Tenants { get; set; }
    public DbSet<RoleApplicationModel> Roles { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Configure the PostgreSQL connection string
        optionsBuilder.UseNpgsql("Host=localhost;Database=OptimaTechBuildingManagerUserDB;Username=postgres;Password=postgres");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TenantApplicationModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Code).IsRequired();
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Deleted).IsRequired();
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.CreatedUserId);
            entity.Property(e => e.UpdatedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.UpdatedUserId);
            entity.Property(e => e.DeletedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.DeletedUserId);
            entity.HasIndex(o => o.Code).HasDatabaseName("IX_Tenant_Code").IsUnique();
        });

        modelBuilder.Entity<RoleApplicationModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Code).IsRequired();
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Deleted).IsRequired();
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.CreatedUserId);
            entity.Property(e => e.UpdatedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.UpdatedUserId);
            entity.Property(e => e.DeletedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.DeletedUserId);
            entity.HasIndex(o => o.Code).HasDatabaseName("IX_Role_Code").IsUnique();
        });

    }
}