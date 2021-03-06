using Finanzuebersicht.Backend.Core.Contract.Persistence.Modules.Accounting.Categories;
using System;

namespace Finanzuebersicht.Backend.Core.Contract.Persistence.Modules.Accounting.Categories
{
    public interface IDbCategoryListItem
    {
        Guid Id { get; set; }

        IDbCategory SuperCategory { get; set; }

        string Title { get; set; }

        string Color { get; set; }
    }
}