using System;

namespace Finanzuebersicht.Backend.Core.Contract.Logic.Modules.Accounting.Categories
{
    public interface ICategoryCreate
    {
        Guid SuperCategoryId { get; set; }

        string Title { get; set; }

        string Color { get; set; }
    }
}