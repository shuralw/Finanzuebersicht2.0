using Finanzuebersicht.Backend.Core.Persistence.Modules.Accounting.AccountingEntries;
using Finanzuebersicht.Backend.Core.Persistence.Modules.Accounting.Categories;
using Finanzuebersicht.Backend.Core.Persistence.Modules.Accounting.CategorySearchTerms;
using Finanzuebersicht.Backend.Core.Persistence.Tests.Modules.Accounting.AccountingEntries;
using Finanzuebersicht.Backend.Core.Persistence.Tests.Modules.Accounting.Categories;
using Finanzuebersicht.Backend.Core.Persistence.Tests.Modules.Accounting.CategorySearchTerms;
using Microsoft.EntityFrameworkCore;

namespace Finanzuebersicht.Backend.Core.Persistence.Tests
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

            persistenceDbContext.AccountingEntries.Add(DbAccountingEntry.ToEfAccountingEntry(DbAccountingEntryTest.DbDefault(), AccountingEntryTestValues.EmailUserIdDbDefault));
            persistenceDbContext.AccountingEntries.Add(DbAccountingEntry.ToEfAccountingEntry(DbAccountingEntryTest.DbDefault2(), AccountingEntryTestValues.EmailUserIdDbDefault));

            persistenceDbContext.Categories.Add(DbCategory.ToEfCategory(DbCategoryTest.DbDefault(), CategoryTestValues.EmailUserIdDbDefault));
            persistenceDbContext.Categories.Add(DbCategory.ToEfCategory(DbCategoryTest.DbDefault2(), CategoryTestValues.EmailUserIdDbDefault));

            persistenceDbContext.CategorySearchTerms.Add(DbCategorySearchTerm.ToEfCategorySearchTerm(DbCategorySearchTermTest.DbDefault(), CategorySearchTermTestValues.EmailUserIdDbDefault));
            persistenceDbContext.CategorySearchTerms.Add(DbCategorySearchTerm.ToEfCategorySearchTerm(DbCategorySearchTermTest.DbDefault2(), CategorySearchTermTestValues.EmailUserIdDbDefault));

            persistenceDbContext.SaveChanges();

            return persistenceDbContext;
        }
    }
}