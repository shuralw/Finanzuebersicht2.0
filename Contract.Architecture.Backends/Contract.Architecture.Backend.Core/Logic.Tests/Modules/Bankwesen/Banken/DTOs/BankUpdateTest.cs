using Contract.Architecture.Backend.Core.Contract.Logic.Modules.Bankwesen.Banken;
using System;

namespace Contract.Architecture.Backend.Core.Logic.Tests.Modules.Bankwesen.Banken
{
    internal class BankUpdateTest : IBankUpdate
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime EroeffnetAm { get; set; }

        public bool IsPleite { get; set; }

        public static IBankUpdate ForUpdate()
        {
            return new BankUpdateTest()
            {
                Id = BankTestValues.IdDefault,
                Name = BankTestValues.NameForUpdate,
                EroeffnetAm = BankTestValues.EroeffnetAmForUpdate,
                IsPleite = BankTestValues.IsPleiteForUpdate,
            };
        }
    }
}