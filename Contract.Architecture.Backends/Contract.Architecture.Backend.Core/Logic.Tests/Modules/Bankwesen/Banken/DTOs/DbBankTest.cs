using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Bankwesen.Banken;
using Contract.Architecture.Backend.Core.Contract.Persistence.Tools.Pagination;
using Contract.Architecture.Backend.Core.Logic.Tests.Tools.Pagination;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Contract.Architecture.Backend.Core.Logic.Tests.Modules.Bankwesen.Banken
{
    internal class DbBankTest : IDbBank
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime EroeffnetAm { get; set; }

        public bool IsPleite { get; set; }

        public static IDbBank Default()
        {
            return new DbBankTest()
            {
                Id = BankTestValues.IdDefault,
                Name = BankTestValues.NameDefault,
                EroeffnetAm = BankTestValues.EroeffnetAmDefault,
                IsPleite = BankTestValues.IsPleiteDefault,
            };
        }

        public static IDbBank Default2()
        {
            return new DbBankTest()
            {
                Id = BankTestValues.IdDefault2,
                Name = BankTestValues.NameDefault2,
                EroeffnetAm = BankTestValues.EroeffnetAmDefault2,
                IsPleite = BankTestValues.IsPleiteDefault2,
            };
        }

        public static IDbBank ForUpdate()
        {
            return new DbBankTest()
            {
                Id = BankTestValues.IdDefault,
                Name = BankTestValues.NameForUpdate,
                EroeffnetAm = BankTestValues.EroeffnetAmForUpdate,
                IsPleite = BankTestValues.IsPleiteForUpdate,
            };
        }

        public static void AssertDefault(IDbBank dbBank)
        {
            Assert.AreEqual(BankTestValues.IdDefault, dbBank.Id);
            Assert.AreEqual(BankTestValues.NameDefault, dbBank.Name);
            Assert.AreEqual(BankTestValues.EroeffnetAmDefault, dbBank.EroeffnetAm);
            Assert.AreEqual(BankTestValues.IsPleiteDefault, dbBank.IsPleite);
        }

        public static void AssertDefault2(IDbBank dbBank)
        {
            Assert.AreEqual(BankTestValues.IdDefault2, dbBank.Id);
            Assert.AreEqual(BankTestValues.NameDefault2, dbBank.Name);
            Assert.AreEqual(BankTestValues.EroeffnetAmDefault2, dbBank.EroeffnetAm);
            Assert.AreEqual(BankTestValues.IsPleiteDefault2, dbBank.IsPleite);
        }

        public static void AssertCreated(IDbBank dbBank)
        {
            Assert.AreEqual(BankTestValues.IdForCreate, dbBank.Id);
            Assert.AreEqual(BankTestValues.NameForCreate, dbBank.Name);
            Assert.AreEqual(BankTestValues.EroeffnetAmForCreate, dbBank.EroeffnetAm);
            Assert.AreEqual(BankTestValues.IsPleiteForCreate, dbBank.IsPleite);
        }

        public static void AssertUpdated(IDbBank dbBank)
        {
            Assert.AreEqual(BankTestValues.IdDefault, dbBank.Id);
            Assert.AreEqual(BankTestValues.NameForUpdate, dbBank.Name);
            Assert.AreEqual(BankTestValues.EroeffnetAmForUpdate, dbBank.EroeffnetAm);
            Assert.AreEqual(BankTestValues.IsPleiteForUpdate, dbBank.IsPleite);
        }
    }
}