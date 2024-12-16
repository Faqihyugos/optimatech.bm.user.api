using Microsoft.EntityFrameworkCore;
using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Core.Entities;

namespace OptimaTech.BuildingManager.User.Infrastructure.Repositories;

public class AppDbContext : DbContext
{
    public DbSet<TenantApplicationModel> Tenants { get; set; }
    public DbSet<RoleApplicationModel> Roles { get; set; }
    public DbSet<ProjectApplicationModel> Projects { get; set; }
    public DbSet<UnitApplicationModel> Units { get; set; }
    public DbSet<UserApplicationModel> Users { get; set; }
    public DbSet<UserUnitApplicationModel> UserUnits { get; set; }

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

        modelBuilder.Entity<ProjectApplicationModel>(entity =>
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
            entity.HasIndex(o => o.Code).HasDatabaseName("IX_Project_Code").IsUnique();
        });

        modelBuilder.Entity<UnitApplicationModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Code).IsRequired();
            entity.Property(e => e.Name).IsRequired();
            entity.HasOne(e => e.Project)
                .WithMany().HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Restrict).IsRequired();
            entity.Property(e => e.BuildingName).IsRequired();
            entity.Property(e => e.FloorNumber).IsRequired();
            entity.Property(e => e.UnitNumber).IsRequired();
            entity.Property(e => e.UnitStatus).IsRequired();
            entity.Property(e => e.UnitType).IsRequired();
            entity.Property(e => e.Deleted).IsRequired();
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.CreatedUserId);
            entity.Property(e => e.UpdatedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.UpdatedUserId);
            entity.Property(e => e.DeletedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.DeletedUserId);
            entity.HasIndex(o => o.Code).HasDatabaseName("IX_Unit_Code").IsUnique();
        });

        modelBuilder.Entity<UserApplicationModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Code).IsRequired();
            entity.Property(e => e.Name).IsRequired();
            entity.HasOne(e => e.Role)
                .WithMany().HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Restrict).IsRequired();
            entity.Property(e => e.UserName).IsRequired();
            entity.Property(e => e.Password).IsRequired();
            entity.Property(e => e.BirthDate).IsRequired().HasColumnType("timestamp without time zone");
            entity.Property(e => e.NIK);
            entity.Property(e => e.NPWP);
            entity.Property(e => e.Address);
            entity.Property(e => e.Village);
            entity.Property(e => e.District);
            entity.Property(e => e.City);
            entity.Property(e => e.Province);
            entity.Property(e => e.Email);
            entity.Property(e => e.MobilePhone);
            entity.Property(e => e.Religion);
            entity.Property(e => e.Occupation).IsRequired();
            entity.Property(e => e.UserStatus).IsRequired();
            entity.Property(e => e.UserType).IsRequired();
            entity.Property(e => e.ApprovalStatus).IsRequired();
            entity.Property(e => e.Deleted).IsRequired();
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.CreatedUserId);
            entity.Property(e => e.UpdatedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.UpdatedUserId);
            entity.Property(e => e.DeletedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.DeletedUserId);
            entity.HasIndex(o => o.Code).HasDatabaseName("IX_User_Code").IsUnique();
        });

        modelBuilder.Entity<UserUnitApplicationModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Code).IsRequired();
            entity.Property(e => e.Name).IsRequired();
            entity.HasOne(e => e.User)
                .WithMany().HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict).IsRequired();
            entity.HasOne(e => e.Unit)
                .WithMany().HasForeignKey(e => e.UnitId)
                .OnDelete(DeleteBehavior.Restrict).IsRequired();
            entity.Property(e => e.StartDate).IsRequired().HasColumnType("timestamp without time zone");
            entity.Property(e => e.EndDate).IsRequired().HasColumnType("timestamp without time zone");
            entity.Property(e => e.RelationType).IsRequired();
            entity.Property(e => e.RelationStatus).IsRequired();
            entity.Property(e => e.NoBAST).IsRequired();
            entity.Property(e => e.DateBAST).IsRequired().HasColumnType("timestamp without time zone");
            entity.Property(e => e.Deleted).IsRequired();
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.CreatedUserId);
            entity.Property(e => e.UpdatedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.UpdatedUserId);
            entity.Property(e => e.DeletedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.DeletedUserId);
            entity.HasIndex(o => o.Code).HasDatabaseName("IX_UserUnit_Code").IsUnique();
        });

    }
}