using System;
using System.Collections.Generic;
using BookStoreApp.API.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Data;

public partial class BookStoreContext : IdentityDbContext<ApiUser>
{
    public BookStoreContext()
    {
    }

    public BookStoreContext(DbContextOptions<BookStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);



        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Authors__3214EC07D2573103");

            entity.Property(e => e.Bio).HasMaxLength(250);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Books__3214EC07C95A6099");

            entity.HasIndex(e => e.Isbn, "UQ__Books__447D36EAC89B4E3F").IsUnique();

            entity.Property(e => e.Image).HasMaxLength(50);
            entity.Property(e => e.Isbn)
                .HasMaxLength(50)
                .HasColumnName("ISBN");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Summary).HasMaxLength(250);
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK_Books_ToAuthors");
        });

        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Name = "User",
                NormalizedName = "USER",
                Id = "eadfecc0-7a4d-466f-9ab9-bb20a9e42a8e"
            },
            new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN",
                Id = "2a3c2ce1-c765-42c0-856b-03645d4f3f31"
            }
            );

        var hasher = new PasswordHasher<ApiUser>();

        modelBuilder.Entity<ApiUser>().HasData(
            new ApiUser
            {
                Id = "17360169-1c03-4089-95b3-0294ad07aec6",
                Email = "user@bookstore.com",
                NormalizedEmail = "USER@BOOKSTORE.COM",
                UserName = "User",
                NormalizedUserName = "USER",
                FirstName = "System",
                LastName = "User",
                PasswordHash = hasher.HashPassword(null!, "P@ssword2")
            },
            new ApiUser
            {
                Id = "c6835835-8c82-4eef-8839-6e3abeb62103",
                Email = "admin@bookstore.com",
                NormalizedEmail = "ADMIN@BOOKSTORE.COM",
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                FirstName = "System",
                LastName = "Admin",
                PasswordHash = hasher.HashPassword(null!, "P@ssword1")

            }            
            );


        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
        {
            RoleId = "eadfecc0-7a4d-466f-9ab9-bb20a9e42a8e",
            UserId = "17360169-1c03-4089-95b3-0294ad07aec6"
        }, 
            new IdentityUserRole<string>
            {
                RoleId = "2a3c2ce1-c765-42c0-856b-03645d4f3f31", 
                UserId= "c6835835-8c82-4eef-8839-6e3abeb62103"
            });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
