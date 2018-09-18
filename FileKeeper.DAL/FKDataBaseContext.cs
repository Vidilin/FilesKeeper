using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using FileKeeper.DAL.Models.Db;

namespace FileKeeper.DAL
{
    public partial class FKDataBaseContext : DbContext
    {
        public FKDataBaseContext()
        {
        }

        public FKDataBaseContext(DbContextOptions<FKDataBaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DbFile> Files { get; set; }
        public virtual DbSet<DbUserfile> Userfiles { get; set; }
        public virtual DbSet<DbUser> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=FKDataBase;Username=postgres;Password=1_2");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbFile>(entity =>
            {
                entity.ToTable("files");

                entity.HasIndex(e => e.Hash)
                    .HasName("files_hash_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bin).HasColumnName("bin");

                entity.Property(e => e.Hash)
                    .IsRequired()
                    .HasColumnName("hash")
                    .HasMaxLength(44);
            });

            modelBuilder.Entity<DbUserfile>(entity =>
            {
                entity.ToTable("userfiles");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Filename)
                    .IsRequired()
                    .HasColumnName("filename")
                    .HasColumnType("character varying");

                entity.Property(e => e.Contenttype)
                    .IsRequired()
                    .HasColumnName("contenttype")
                    .HasColumnType("character varying");

                entity.Property(e => e.Hash)
                    .IsRequired()
                    .HasColumnName("hash")
                    .HasMaxLength(44);

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.HashNavigation)
                    .WithMany(p => p.Userfiles)
                    .HasPrincipalKey(p => p.Hash)
                    .HasForeignKey(d => d.Hash)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userfiles_hash_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Userfiles)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userfiles_userid_fkey");
            });

            modelBuilder.Entity<DbUser>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login")
                    .HasColumnType("character varying");
            });
        }
    }
}
