using Contract.Architecture.Backend.Core.Contract.Logic.Modules.Bankwesen.Banken;
using System;

namespace Contract.Architecture.Backend.Core.Logic.Tests.Modules.Bankwesen.Banken
{
    internal class BankCreateTest : IBankCreate
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime EroeffnetAm { get; set; }

        public bool IsPleite { get; set; }

        public static IBankCreate ForCreate()
        {
            return new BankCreateTest()
            {
                Id = BankTestValues.IdForCreate,
                Name = BankTestValues.NameForCreate,
                EroeffnetAm = BankTestValues.EroeffnetAmForCreate,
                IsPleite = BankTestValues.IsPleiteForCreate,
            };
        }
    }
}