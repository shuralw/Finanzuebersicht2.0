using Finanzuebersicht.Backend.Core.Contract.Persistence.Modules.Accounting.Categories;
using System;

namespace Finanzuebersicht.Backend.Core.Logic.Modules.Accounting.Categories
{
    internal class DbCategory : IDbCategory
    {
        public Guid Id { get; set; }

        public Guid SuperCategoryId { get; set; }

        public string Title { get; set; }

        public string Color { get; set; }
    }
}