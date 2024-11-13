using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Project.Models;

public partial class UserManagermentContext : DbContext
{
    public UserManagermentContext()
    {
    }

    public UserManagermentContext(DbContextOptions<UserManagermentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //Đọc connectionstring từ file appsettings.json
        // để kết nối đến DB 
        var builder = new ConfigurationBuilder()
                               .SetBasePath(Directory.GetCurrentDirectory())
                               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        IConfigurationRoot configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyCnn"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE1AB17823D6");

            entity.ToTable("Role");

            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4C18C62D6B");

            entity.ToTable("User");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.PassWord).HasMaxLength(100);
            entity.Property(e => e.UserName).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__User__RoleId__4CA06362");
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.UserProfileId).HasName("PK__UserProf__9E267F6242A54A65");

            entity.ToTable("UserProfile");

            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Avatar).HasMaxLength(200);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.District).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Sex).HasMaxLength(10);
            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.Ward).HasMaxLength(100);

            entity.HasOne(d => d.User).WithMany(p => p.UserProfiles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserProfi__UserI__4F7CD00D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
