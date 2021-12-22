using System;

namespace Finanzuebersicht.Backend.Core.Contract.Logic.Modules.Accounting.CategorySearchTerms
{
    public interface ICategorySearchTermCreate
    {
        Guid CategoryId { get; set; }

        string Term { get; set; }
    }
}