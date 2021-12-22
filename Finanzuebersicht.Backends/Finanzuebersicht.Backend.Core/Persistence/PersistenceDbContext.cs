using Finanzuebersicht.Backend.Core.Persistence.Modules.Accounting.AccountingEntries;
using Finanzuebersicht.Backend.Core.Persistence.Modules.Accounting.Categories;
using Finanzuebersicht.Backend.Core.Persistence.Modules.Accounting.CategorySearchTerms;
using Finanzuebersicht.Backend.Core.Persistence.Modules.SessionManagement.Sessions;
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

        // Fachliche Module
        public virtual DbSet<EfAccountingEntry> AccountingEntries { get; set; }

        public virtual DbSet<EfCategory> Categories { get; set; }

        public virtual DbSet<EfCategorySearchTerm> CategorySearchTerms { get; set; }

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

            modelBuilder.Entity<EfAccountingEntry>(entity =>
            {
                entity.ToTable("AccountingEntries");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.AccountingEntries)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_AccountingEntries_CategoryId");

                entity.Property(e => e.Auftragskonto)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Buchungsdatum).HasColumnType("datetime");

                entity.Property(e => e.ValutaDatum).HasColumnType("datetime");

                entity.Property(e => e.Buchungstext)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.Verwendungszweck)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.GlaeubigerId)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Mandatsreferenz)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Sammlerreferenz)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.AuslagenersatzRuecklastschrift)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Beguenstigter)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.IBAN)
                    .IsRequired()
                    .HasMaxLength(22);

                entity.Property(e => e.BIC)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Waehrung)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Info)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<EfCategory>(entity =>
            {
                entity.ToTable("Categories");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.SuperCategory)
                    .WithMany(p => p.ChildCategories)
                    .HasForeignKey(d => d.SuperCategoryId)
                    .HasConstraintName("FK_Categories_SuperCategoryId");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<EfCategorySearchTerm>(entity =>
            {
                entity.ToTable("CategorySearchTerms");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CategorySearchTerms)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_CategorySearchTerms_CategoryId");

                entity.Property(e => e.Term)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            this.OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}