using Finanzuebersicht.Backend.Core.Contract.Logic.Modules.Accounting.Categories;
using System;

namespace Finanzuebersicht.Backend.Core.Contract.Logic.Modules.Accounting.CategorySearchTerms
{
    public interface ICategorySearchTermDetail
    {
        Guid Id { get; set; }

        ICategory Category { get; set; }

        string Term { get; set; }
    }
}