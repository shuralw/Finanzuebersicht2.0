using Contract.Architecture.Backend.Core.Contract.Logic.Modules.Bankwesen.Banken;
using System;
using System.ComponentModel.DataAnnotations;

namespace Contract.Architecture.Backend.Core.API.Modules.Bankwesen.Banken
{
    public class BankUpdate : IBankUpdate
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        public DateTime EroeffnetAm { get; set; }

        [Required]
        public bool IsPleite { get; set; }
    }
}