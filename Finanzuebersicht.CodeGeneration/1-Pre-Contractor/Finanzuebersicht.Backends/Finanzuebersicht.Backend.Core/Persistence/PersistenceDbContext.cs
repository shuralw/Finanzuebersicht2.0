﻿using Finanzuebersicht.Backend.Core.Persistence.Modules.SessionManagement.Sessions;
using Finanzuebersicht.Backend.Core.Persistence.Modules.UserManagement.EmailUserPasswortReset;
using Finanzuebersicht.Backend.Core.Persistence.Modules.UserManagement.EmailUsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Finanzuebersicht.Backend.Core.Persistence
{
    public partial class PersistenceDbContext : DbContext
    {
        private readonly IConfiguration configuration;

        public PersistenceDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        private PersistenceDbContext(DbContextOptions<PersistenceDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EfEmailUser> EmailUsers { get; set; }

        public virtual DbSet<EfEmailUserPasswordResetToken> EmailUserPasswordResetTokens { get; set; }

        public virtual DbSet<EfSession> Sessions { get; set; }

        public static PersistenceDbContext CustomInstantiate(DbContextOptions<PersistenceDbContext> options)
        {
            return new PersistenceDbContext(options);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(this.configuration.GetConnectionString("Finanzuebersicht.Backend.Core.Database"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EfEmailUserPasswordResetToken>(entity =>
            {
                entity.HasKey(e => e.Token)
                    .HasName("PK_EmailUserPasswordResetToken_Token");

                entity.Property(e => e.Token).HasMaxLength(64);

                entity.Property(e => e.EmailUserId).IsRequired();

                entity.Property(e => e.ExpiresOn).HasColumnType("datetime");

                entity.HasOne(d => d.EmailUser)
                    .WithMany(p => p.EmailUserPasswordResetTokens)
                    .HasForeignKey(d => d.EmailUserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_EmailUserPasswordResetToken_EmailUserId");
            });

            modelBuilder.Entity<EfEmailUser>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasDatabaseName("EmailUsersEmailUnique")
                    .IsUnique()
                    .HasFilter("([Email] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(88);

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasMaxLength(54);
            });

            modelBuilder.Entity<EfSession>(entity =>
            {
                entity.HasKey(e => e.Token)
                    .HasName("PK_Sessions_Token");

                entity.Property(e => e.Token).HasMaxLength(64);

                entity.Property(e => e.ExpiresOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            this.OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}