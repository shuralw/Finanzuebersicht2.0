using Finanzuebersicht.Backend.Core.Contract.Persistence.Modules.Accounting.CategorySearchTerms;
using System;

namespace Finanzuebersicht.Backend.Core.Logic.Modules.Accounting.CategorySearchTerms
{
    internal class DbCategorySearchTerm : IDbCategorySearchTerm
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }

        public string Term { get; set; }
    }
}