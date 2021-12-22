using System;

namespace Finanzuebersicht.Backend.Core.Contract.Persistence.Modules.Accounting.Categories
{
    public interface IDbCategory
    {
        Guid Id { get; set; }

        Guid SuperCategoryId { get; set; }

        string Title { get; set; }

        string Color { get; set; }
    }
}