using Contract.Architecture.Backend.Core.Contract.Contexts;
using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Kundenstamm.Kunden;
using Contract.Architecture.Backend.Core.Contract.Persistence.Tools.Pagination;
using Contract.Architecture.Backend.Core.Persistence.Modules.Kundenstamm.Kunden;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace Contract.Architecture.Backend.Core.Persistence.Tests.Modules.Kundenstamm.Kunden
{
    [TestClass]
    public class KundenCrudRepositoryTests
    {
        [TestMethod]
        public void CreateKundeTest()
        {
            // Arrange
            KundenCrudRepository kundenCrudRepository = this.GetKundenCrudRepositoryEmpty();

            // Act
            kundenCrudRepository.CreateKunde(DbKundeTest.ForCreate());

            // Assert
            IDbKunde dbKunde = kundenCrudRepository.GetKunde(KundeTestValues.IdForCreate);
            DbKundeTest.AssertForCreate(dbKunde);
        }

        [TestMethod]
        public void DeleteKundeTest()
        {
            // Arrange
            KundenCrudRepository kundenCrudRepository = this.GetKundenCrudRepositoryDefault();

            // Act
            kundenCrudRepository.DeleteKunde(KundeTestValues.IdDbDefault);

            // Assert
            bool doesKundeExist = kundenCrudRepository.DoesKundeExist(KundeTestValues.IdDbDefault);
            Assert.IsFalse(doesKundeExist);
        }

        [TestMethod]
        public void DoesKundeExistFalseTest()
        {
            // Arrange
            KundenCrudRepository kundenCrudRepository = this.GetKundenCrudRepositoryEmpty();

            // Act
            bool doesKundeExist = kundenCrudRepository.DoesKundeExist(KundeTestValues.IdDbDefault);

            // Assert
            Assert.IsFalse(doesKundeExist);
        }

        [TestMethod]
        public void DoesKundeExistTrueTest()
        {
            // Arrange
            KundenCrudRepository kundenCrudRepository = this.GetKundenCrudRepositoryDefault();

            // Act
            bool doesKundeExist = kundenCrudRepository.DoesKundeExist(KundeTestValues.IdDbDefault);

            // Assert
            Assert.IsTrue(doesKundeExist);
        }

        [TestMethod]
        public void GetKundeDefaultTest()
        {
            // Arrange
            KundenCrudRepository kundenCrudRepository = this.GetKundenCrudRepositoryDefault();

            // Act
            IDbKunde dbKunde = kundenCrudRepository.GetKunde(KundeTestValues.IdDbDefault);

            // Assert
            DbKundeTest.AssertDbDefault(dbKunde);
        }

        [TestMethod]
        public void GetKundeDetailDefaultTest()
        {
            // Arrange
            KundenCrudRepository kundenCrudRepository = this.GetKundenCrudRepositoryDefault();

            // Act
            IDbKundeDetail dbKundeDetail = kundenCrudRepository.GetKundeDetail(KundeTestValues.IdDbDefault);

            // Assert
            DbKundeDetailTest.AssertDbDefault(dbKundeDetail);
        }

        [TestMethod]
        public void GetKundeDetailNullTest()
        {
            // Arrange
            KundenCrudRepository kundenCrudRepository = this.GetKundenCrudRepositoryEmpty();

            // Act
            IDbKundeDetail dbKundeDetail = kundenCrudRepository.GetKundeDetail(KundeTestValues.IdDbDefault);

            // Assert
            Assert.IsNull(dbKundeDetail);
        }

        [TestMethod]
        public void GetAllKundenDefaultTest()
        {
            // Arrange
            KundenCrudRepository kundenCrudRepository = this.GetKundenCrudRepositoryDefault();

            // Act
            IDbKunde[] dbKunden = kundenCrudRepository.GetAllKunden().ToArray();

            // Assert
            Assert.AreEqual(2, dbKunden.Length);
            DbKundeTest.AssertDbDefault(dbKunden[0]);
            DbKundeTest.AssertDbDefault2(dbKunden[1]);
        }

        [TestMethod]
        public void GetPagedKundenDefaultTest()
        {
            // Arrange
            KundenCrudRepository kundenCrudRepository = this.GetKundenCrudRepositoryDefault();

            // Act
            IDbPagedResult<IDbKundeListItem> dbKundenPagedResult =
                kundenCrudRepository.GetPagedKunden();

            // Assert
            IDbKundeListItem[] dbKunden = dbKundenPagedResult.Data.ToArray();
            Assert.AreEqual(2, dbKunden.Length);
            DbKundeListItemTest.AssertDbDefault(dbKunden[0]);
            DbKundeListItemTest.AssertDbDefault2(dbKunden[1]);
        }

        [TestMethod]
        public void GetKundeNullTest()
        {
            // Arrange
            KundenCrudRepository kundenCrudRepository = this.GetKundenCrudRepositoryEmpty();

            // Act
            IDbKunde dbKunde = kundenCrudRepository.GetKunde(KundeTestValues.IdDbDefault);

            // Assert
            Assert.IsNull(dbKunde);
        }

        [TestMethod]
        public void UpdateKundeTest()
        {
            // Arrange
            KundenCrudRepository kundenCrudRepository = this.GetKundenCrudRepositoryDefault();

            // Act
            kundenCrudRepository.UpdateKunde(DbKundeTest.ForUpdate());

            // Assert
            IDbKunde dbKunde = kundenCrudRepository.GetKunde(KundeTestValues.IdDbDefault);
            DbKundeTest.AssertForUpdate(dbKunde);
        }

        private KundenCrudRepository GetKundenCrudRepositoryDefault()
        {
            return new KundenCrudRepository(
                this.GetSessionContext(),
                this.GetPaginationContext(),
                InMemoryDbContext.CreatePersistenceDbContextWithDbDefault());
        }

        private KundenCrudRepository GetKundenCrudRepositoryEmpty()
        {
            return new KundenCrudRepository(
                this.GetSessionContext(),
                this.GetPaginationContext(),
                InMemoryDbContext.CreatePersistenceDbContextEmpty());
        }

        private ISessionContext GetSessionContext()
        {
            Mock<ISessionContext> sessionContext = new Mock<ISessionContext>(MockBehavior.Strict);
            sessionContext.Setup(sessionContext => sessionContext.MandantId)
                .Returns(KundeTestValues.MandantIdDbDefault);
            return sessionContext.Object;
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