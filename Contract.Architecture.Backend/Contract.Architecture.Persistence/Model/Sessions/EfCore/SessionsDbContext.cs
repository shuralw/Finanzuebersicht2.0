using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Contract.Architecture.Persistence.Model.Sessions.EfCore
{
    public partial class SessionsDbContext : DbContext
    {
        private readonly IConfiguration configuration;

        public SessionsDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        private SessionsDbContext(DbContextOptions<SessionsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EfSession> Sessions { get; set; }

        public static SessionsDbContext CustomInstantiate(DbContextOptions<SessionsDbContext> options)
        {
            return new SessionsDbContext(options);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(this.configuration.GetConnectionString("Contract.Architecture.Database"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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