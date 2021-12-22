using Finanzuebersicht.Backend.Core.Contract.Persistence.Modules.Accounting.Categories;
using System;

namespace Finanzuebersicht.Backend.Core.Contract.Persistence.Modules.Accounting.CategorySearchTerms
{
    public interface IDbCategorySearchTermDetail
    {
        Guid Id { get; set; }

        IDbCategory Category { get; set; }

        string Term { get; set; }
    }
}