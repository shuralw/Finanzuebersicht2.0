using Contract.Architecture.Backend.Core.Contract.Contexts;
using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Bankwesen.Banken;
using Contract.Architecture.Backend.Core.Contract.Persistence.Tools.Pagination;
using Contract.Architecture.Backend.Core.Persistence.Modules.Bankwesen.Banken;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace Contract.Architecture.Backend.Core.Persistence.Tests.Modules.Bankwesen.Banken
{
    [TestClass]
    public class BankenCrudRepositoryTests
    {
        [TestMethod]
        public void CreateBankTest()
        {
            // Arrange
            BankenCrudRepository bankenCrudRepository = this.GetBankenCrudRepositoryEmpty();

            // Act
            bankenCrudRepository.CreateBank(DbBankTest.ForCreate());

            // Assert
            IDbBank dbBank = bankenCrudRepository.GetBank(BankTestValues.IdForCreate);
            DbBankTest.AssertForCreate(dbBank);
        }

        [TestMethod]
        public void DeleteBankTest()
        {
            // Arrange
            BankenCrudRepository bankenCrudRepository = this.GetBankenCrudRepositoryDefault();

            // Act
            bankenCrudRepository.DeleteBank(BankTestValues.IdDbDefault);

            // Assert
            bool doesBankExist = bankenCrudRepository.DoesBankExist(BankTestValues.IdDbDefault);
            Assert.IsFalse(doesBankExist);
        }

        [TestMethod]
        public void DoesBankExistFalseTest()
        {
            // Arrange
            BankenCrudRepository bankenCrudRepository = this.GetBankenCrudRepositoryEmpty();

            // Act
            bool doesBankExist = bankenCrudRepository.DoesBankExist(BankTestValues.IdDbDefault);

            // Assert
            Assert.IsFalse(doesBankExist);
        }

        [TestMethod]
        public void DoesBankExistTrueTest()
        {
            // Arrange
            BankenCrudRepository bankenCrudRepository = this.GetBankenCrudRepositoryDefault();

            // Act
            bool doesBankExist = bankenCrudRepository.DoesBankExist(BankTestValues.IdDbDefault);

            // Assert
            Assert.IsTrue(doesBankExist);
        }

        [TestMethod]
        public void GetBankDefaultTest()
        {
            // Arrange
            BankenCrudRepository bankenCrudRepository = this.GetBankenCrudRepositoryDefault();

            // Act
            IDbBank dbBank = bankenCrudRepository.GetBank(BankTestValues.IdDbDefault);

            // Assert
            DbBankTest.AssertDbDefault(dbBank);
        }

        [TestMethod]
        public void GetBankDetailDefaultTest()
        {
            // Arrange
            BankenCrudRepository bankenCrudRepository = this.GetBankenCrudRepositoryDefault();

            // Act
            IDbBankDetail dbBankDetail = bankenCrudRepository.GetBankDetail(BankTestValues.IdDbDefault);

            // Assert
            DbBankDetailTest.AssertDbDefault(dbBankDetail);
        }

        [TestMethod]
        public void GetBankDetailNullTest()
        {
            // Arrange
            BankenCrudRepository bankenCrudRepository = this.GetBankenCrudRepositoryEmpty();

            // Act
            IDbBankDetail dbBankDetail = bankenCrudRepository.GetBankDetail(BankTestValues.IdDbDefault);

            // Assert
            Assert.IsNull(dbBankDetail);
        }

        [TestMethod]
        public void GetAllBankenDefaultTest()
        {
            // Arrange
            BankenCrudRepository bankenCrudRepository = this.GetBankenCrudRepositoryDefault();

            // Act
            IDbBank[] dbBanken = bankenCrudRepository.GetAllBanken().ToArray();

            // Assert
            Assert.AreEqual(2, dbBanken.Length);
            DbBankTest.AssertDbDefault(dbBanken[0]);
            DbBankTest.AssertDbDefault2(dbBanken[1]);
        }

        [TestMethod]
        public void GetPagedBankenDefaultTest()
        {
            // Arrange
            BankenCrudRepository bankenCrudRepository = this.GetBankenCrudRepositoryDefault();

            // Act
            IDbPagedResult<IDbBankListItem> dbBankenPagedResult =
                bankenCrudRepository.GetPagedBanken();

            // Assert
            IDbBankListItem[] dbBanken = dbBankenPagedResult.Data.ToArray();
            Assert.AreEqual(2, dbBanken.Length);
            DbBankListItemTest.AssertDbDefault(dbBanken[0]);
            DbBankListItemTest.AssertDbDefault2(dbBanken[1]);
        }

        [TestMethod]
        public void GetBankNullTest()
        {
            // Arrange
            BankenCrudRepository bankenCrudRepository = this.GetBankenCrudRepositoryEmpty();

            // Act
            IDbBank dbBank = bankenCrudRepository.GetBank(BankTestValues.IdDbDefault);

            // Assert
            Assert.IsNull(dbBank);
        }

        [TestMethod]
        public void UpdateBankTest()
        {
            // Arrange
            BankenCrudRepository bankenCrudRepository = this.GetBankenCrudRepositoryDefault();

            // Act
            bankenCrudRepository.UpdateBank(DbBankTest.ForUpdate());

            // Assert
            IDbBank dbBank = bankenCrudRepository.GetBank(BankTestValues.IdDbDefault);
            DbBankTest.AssertForUpdate(dbBank);
        }

        private BankenCrudRepository GetBankenCrudRepositoryDefault()
        {
            return new BankenCrudRepository(
                this.GetPaginationContext(),
                InMemoryDbContext.CreatePersistenceDbContextWithDbDefault());
        }

        private BankenCrudRepository GetBankenCrudRepositoryEmpty()
        {
            return new BankenCrudRepository(
                this.GetPaginationContext(),
                InMemoryDbContext.CreatePersistenceDbContextEmpty());
        }

        private IPaginationContext GetPaginationContext()
        {
            Mock<IPaginationContext> paginationContext = new Mock<IPaginationContext>();
            paginationContext.Setup(context => context.Limit).Returns(10);
            paginationContext.Setup(context => context.Offset).Returns(0);
            paginationContext.Setup(context => context.Sort).Returns(Array.Empty<IPaginationSortItem>());
            paginationContext.Setup(context => context.Filter).Returns(Array.Empty<IPaginationFilterItem>());
            return paginationContext.Object;
        }
    }
}