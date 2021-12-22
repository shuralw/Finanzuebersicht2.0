using Finanzuebersicht.Backend.Core.Contract.Logic.Modules.Accounting.CategorySearchTerms;
using System;
using System.ComponentModel.DataAnnotations;

namespace Finanzuebersicht.Backend.Core.API.Modules.Accounting.CategorySearchTerms
{
    public class CategorySearchTermCreate : ICategorySearchTermCreate
    {
        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string Term { get; set; }
    }
}