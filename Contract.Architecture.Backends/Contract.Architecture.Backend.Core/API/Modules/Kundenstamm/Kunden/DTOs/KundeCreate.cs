using Contract.Architecture.Backend.Core.Contract.Logic.Modules.Kundenstamm.Kunden;
using System;
using System.ComponentModel.DataAnnotations;

namespace Contract.Architecture.Backend.Core.API.Modules.Kundenstamm.Kunden
{
    public class KundeCreate : IKundeCreate
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        public int Balance { get; set; }

        [Required]
        public Guid BankId { get; set; }
    }
}