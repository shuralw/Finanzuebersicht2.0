using Contract.Architecture.Backend.Core.Persistence.Modules.Bankwesen.Banken;
using Contract.Architecture.Backend.Core.Persistence.Modules.Kundenstamm.Kunden;
using Contract.Architecture.Backend.Core.Persistence.Tests.Modules.Bankwesen.Banken;
using Contract.Architecture.Backend.Core.Persistence.Tests.Modules.Kundenstamm.Kunden;
using Microsoft.EntityFrameworkCore;

namespace Contract.Architecture.Backend.Core.Persistence.Tests
{
    public static class InMemoryDbContext
    {
        public static PersistenceDbContext CreatePersistenceDbContextEmpty()
        {
            DbContextOptions<PersistenceDbContext> options;
            var builder = new DbContextOptionsBuilder<PersistenceDbContext>();
            builder.UseInMemoryDatabase("in-memory-db");
            options = builder.Options;

            PersistenceDbContext persistenceDbContext = PersistenceDbContext.CustomInstantiate(options);
            persistenceDbContext.Database.EnsureDeleted();
            persistenceDbContext.Database.EnsureCreated();

            return persistenceDbContext;
        }

        public static PersistenceDbContext CreatePersistenceDbContextWithDbDefault()
        {
            PersistenceDbContext persistenceDbContext = CreatePersistenceDbContextEmpty();

            persistenceDbContext.Banken.Add(DbBank.ToEfBank(DbBankTest.DbDefault(), BankTestValues.MandantIdDbDefault));
            persistenceDbContext.Banken.Add(DbBank.ToEfBank(DbBankTest.DbDefault2(), BankTestValues.MandantIdDbDefault));

            persistenceDbContext.Kunden.Add(DbKunde.ToEfKunde(DbKundeTest.DbDefault(), KundeTestValues.MandantIdDbDefault));
            persistenceDbContext.Kunden.Add(DbKunde.ToEfKunde(DbKundeTest.DbDefault2(), KundeTestValues.MandantIdDbDefault));

            persistenceDbContext.SaveChanges();

            return persistenceDbContext;
        }
    }
}