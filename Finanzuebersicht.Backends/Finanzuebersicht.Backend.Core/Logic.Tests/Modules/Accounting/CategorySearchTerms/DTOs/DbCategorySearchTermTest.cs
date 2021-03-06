using Finanzuebersicht.Backend.Core.Contract.Persistence.Modules.Accounting.CategorySearchTerms;
using Finanzuebersicht.Backend.Core.Contract.Persistence.Tools.Pagination;
using Finanzuebersicht.Backend.Core.Logic.Tests.Tools.Pagination;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Finanzuebersicht.Backend.Core.Logic.Tests.Modules.Accounting.CategorySearchTerms
{
    internal class DbCategorySearchTermTest : IDbCategorySearchTerm
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }

        public string Term { get; set; }

        public static IDbCategorySearchTerm Default()
        {
            return new DbCategorySearchTermTest()
            {
                Id = CategorySearchTermTestValues.IdDefault,
                CategoryId = CategorySearchTermTestValues.CategoryIdDefault,
                Term = CategorySearchTermTestValues.TermDefault,
            };
        }

        public static IDbCategorySearchTerm Default2()
        {
            return new DbCategorySearchTermTest()
            {
                Id = CategorySearchTermTestValues.IdDefault2,
                CategoryId = CategorySearchTermTestValues.CategoryIdDefault2,
                Term = CategorySearchTermTestValues.TermDefault2,
            };
        }

        public static void AssertDefault(IDbCategorySearchTerm dbCategorySearchTerm)
        {
            Assert.AreEqual(CategorySearchTermTestValues.IdDefault, dbCategorySearchTerm.Id);
            Assert.AreEqual(CategorySearchTermTestValues.CategoryIdDefault, dbCategorySearchTerm.CategoryId);
            Assert.AreEqual(CategorySearchTermTestValues.TermDefault, dbCategorySearchTerm.Term);
        }

        public static void AssertDefault2(IDbCategorySearchTerm dbCategorySearchTerm)
        {
            Assert.AreEqual(CategorySearchTermTestValues.IdDefault2, dbCategorySearchTerm.Id);
            Assert.AreEqual(CategorySearchTermTestValues.CategoryIdDefault2, dbCategorySearchTerm.CategoryId);
            Assert.AreEqual(CategorySearchTermTestValues.TermDefault2, dbCategorySearchTerm.Term);
        }

        public static void AssertCreated(IDbCategorySearchTerm dbCategorySearchTerm)
        {
            Assert.AreEqual(CategorySearchTermTestValues.IdForCreate, dbCategorySearchTerm.Id);
            Assert.AreEqual(CategorySearchTermTestValues.CategoryIdForCreate, dbCategorySearchTerm.CategoryId);
            Assert.AreEqual(CategorySearchTermTestValues.TermForCreate, dbCategorySearchTerm.Term);
        }
    }
}