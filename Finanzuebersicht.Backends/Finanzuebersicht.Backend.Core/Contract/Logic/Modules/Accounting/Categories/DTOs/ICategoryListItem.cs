using Finanzuebersicht.Backend.Core.Contract.Logic.Modules.Accounting.Categories;
using System;

namespace Finanzuebersicht.Backend.Core.Contract.Logic.Modules.Accounting.Categories
{
    public interface ICategoryListItem
    {
        Guid Id { get; set; }

        ICategory SuperCategory { get; set; }

        string Title { get; set; }

        string Color { get; set; }
    }
}