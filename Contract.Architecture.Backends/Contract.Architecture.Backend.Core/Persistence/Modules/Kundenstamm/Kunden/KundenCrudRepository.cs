using Contract.Architecture.Backend.Core.Contract.Contexts;
using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Kundenstamm.Kunden;
using Contract.Architecture.Backend.Core.Contract.Persistence.Tools.Pagination;
using Contract.Architecture.Backend.Core.Persistence.Tools.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contract.Architecture.Backend.Core.Persistence.Modules.Kundenstamm.Kunden
{
    internal class KundenCrudRepository : IKundenCrudRepository
    {
        private readonly IPaginationContext paginationContext;

        private readonly PersistenceDbContext dbContext;

        public KundenCrudRepository(
            IPaginationContext paginationContext,
            PersistenceDbContext dbContext)
        {
            this.paginationContext = paginationContext;

            this.dbContext = dbContext;
        }

        public void CreateKunde(IDbKunde dbKunde)
        {
            this.dbContext.Kunden.Add(DbKunde.ToEfKunde(dbKunde));
            this.dbContext.SaveChanges();
        }

        public void DeleteKunde(Guid kundeId)
        {
            EfKunde efKunde = this.dbContext.Kunden
                .Where(efKunde => efKunde.Id == kundeId)
                .FirstOrDefault();

            this.dbContext.Kunden.Remove(efKunde);
            this.dbContext.SaveChanges();
        }

        public bool DoesKundeExist(Guid kundeId)
        {
            return this.dbContext.Kunden.Any(efKunde => efKunde.Id == kundeId);
        }

        public IDbKunde GetKunde(Guid kundeId)
        {
            EfKunde efKunde = this.dbContext.Kunden
                .Where(efKunde => efKunde.Id == kundeId)
                .FirstOrDefault();

            return DbKunde.FromEfKunde(efKunde);
        }

        public IDbKundeDetail GetKundeDetail(Guid kundeId)
        {
            EfKunde efKunde = this.dbContext.Kunden
                .Include(efKunde => efKunde.Bank)
                .Where(efKunde => efKunde.Id == kundeId)
                .FirstOrDefault();

            return DbKundeDetail.FromEfKunde(efKunde);
        }

        public IDbPagedResult<IDbKundeListItem> GetPagedKunden()
        {
            var efKunden = this.dbContext.Kunden
                .Include(efKunde => efKunde.Bank);

            return Pagination.Filter(
                efKunden,
                this.paginationContext,
                efKunde => DbKundeListItem.FromEfKunde(efKunde));
        }

        public IEnumerable<IDbKunde> GetAllKunden()
        {
            return this.dbContext.Kunden
                .Select(efKunde => DbKunde.FromEfKunde(efKunde));
        }

        public void UpdateKunde(IDbKunde dbKunde)
        {
            EfKunde efKunde = this.dbContext.Kunden
                .Where(efKunde => efKunde.Id == dbKunde.Id)
                .FirstOrDefault();

            DbKunde.UpdateEfKunde(efKunde, dbKunde);

            this.dbContext.Kunden.Update(efKunde);
            this.dbContext.SaveChanges();
        }
    }
}