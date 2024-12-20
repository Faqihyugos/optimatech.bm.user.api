﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OptimaTech.BuildingManager.User.Infrastructure.Repositories;

#nullable disable

namespace OptimaTech.BuildingManager.User.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241204005815_ProjectCreate")]
    partial class ProjectCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("OptimaTech.BuildingManager.User.Application.Models.ProjectApplicationModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("CreatedUserId")
                        .HasColumnType("uuid");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("DeletedUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("UpdatedUserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasDatabaseName("IX_Project_Code");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("OptimaTech.BuildingManager.User.Application.Models.RoleApplicationModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("CreatedUserId")
                        .HasColumnType("uuid");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("DeletedUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("UpdatedUserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasDatabaseName("IX_Role_Code");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("OptimaTech.BuildingManager.User.Application.Models.TenantApplicationModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("CreatedUserId")
                        .HasColumnType("uuid");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("DeletedUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("UpdatedUserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasDatabaseName("IX_Tenant_Code");

                    b.ToTable("Tenants");
                });
#pragma warning restore 612, 618
        }
    }
}
