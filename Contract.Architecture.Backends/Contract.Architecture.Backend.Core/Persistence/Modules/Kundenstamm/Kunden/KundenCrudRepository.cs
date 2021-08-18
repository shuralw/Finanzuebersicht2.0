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
        private readonly ISessionContext sessionContext;
        private readonly IPaginationContext paginationContext;

        private readonly PersistenceDbContext dbContext;

        public KundenCrudRepository(
            ISessionContext sessionContext,
            IPaginationContext paginationContext,
            PersistenceDbContext dbContext)
        {
            this.sessionContext = sessionContext;
            this.paginationContext = paginationContext;

            this.dbContext = dbContext;
        }

        public void CreateKunde(IDbKunde dbKunde)
        {
            this.dbContext.Kunden.Add(DbKunde.ToEfKunde(dbKunde, this.sessionContext.MandantId));
            this.dbContext.SaveChanges();
        }

        public void DeleteKunde(Guid kundeId)
        {
            EfKunde efKunde = this.dbContext.Kunden
                .Where(efKunde => efKunde.Id == kundeId)
                .Where(efKunde => efKunde.MandantId == this.sessionContext.MandantId)
                .FirstOrDefault();

            this.dbContext.Kunden.Remove(efKunde);
            this.dbContext.SaveChanges();
        }

        public bool DoesKundeExist(Guid kundeId)
        {
            return this.dbContext.Kunden
                .Any(efKunde => efKunde.MandantId == this.sessionContext.MandantId && efKunde.Id == kundeId);
        }

        public IDbKunde GetKunde(Guid kundeId)
        {
            EfKunde efKunde = this.dbContext.Kunden
                .Where(efKunde => efKunde.Id == kundeId)
                .Where(efKunde => efKunde.MandantId == this.sessionContext.MandantId)
                .FirstOrDefault();

            return DbKunde.FromEfKunde(efKunde);
        }

        public IDbKundeDetail GetKundeDetail(Guid kundeId)
        {
            EfKunde efKunde = this.dbContext.Kunden
                .Include(efKunde => efKunde.Bank)
                .Where(efKunde => efKunde.Id == kundeId)
                .Where(efKunde => efKunde.MandantId == this.sessionContext.MandantId)
                .FirstOrDefault();

            return DbKundeDetail.FromEfKunde(efKunde);
        }

        public IDbPagedResult<IDbKundeListItem> GetPagedKunden()
        {
            var efKunden = this.dbContext.Kunden
                .Include(efKunde => efKunde.Bank)
                .Where(efKunde => efKunde.MandantId == this.sessionContext.MandantId);

            return Pagination.Filter(
                efKunden,
                this.paginationContext,
                efKunde => DbKundeListItem.FromEfKunde(efKunde));
        }

        public IEnumerable<IDbKunde> GetAllKunden()
        {
            return this.dbContext.Kunden
                .Where(efKunde => efKunde.MandantId == this.sessionContext.MandantId)
                .Select(efKunde => DbKunde.FromEfKunde(efKunde));
        }

        public void UpdateKunde(IDbKunde dbKunde)
        {
            EfKunde efKunde = this.dbContext.Kunden
                .Where(efKunde => efKunde.Id == dbKunde.Id)
                .Where(efKunde => efKunde.MandantId == this.sessionContext.MandantId)
                .FirstOrDefault();

            DbKunde.UpdateEfKunde(efKunde, dbKunde);

            this.dbContext.Kunden.Update(efKunde);
            this.dbContext.SaveChanges();
        }
    }
}