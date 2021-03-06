using Finanzuebersicht.Backend.Core.Contract.Logic.LogicResults;
using Finanzuebersicht.Backend.Core.Contract.Logic.Modules.Accounting.Categories;
using Finanzuebersicht.Backend.Core.Contract.Logic.Tools.Identifier;
using Finanzuebersicht.Backend.Core.Contract.Logic.Tools.Pagination;
using Finanzuebersicht.Backend.Core.Contract.Persistence.Modules.Accounting.Categories;
using Finanzuebersicht.Backend.Core.Contract.Persistence.Tools.Pagination;
using Finanzuebersicht.Backend.Core.Logic.LogicResults;
using Finanzuebersicht.Backend.Core.Logic.Tools.Pagination;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace Finanzuebersicht.Backend.Core.Logic.Modules.Accounting.Categories
{
    internal class CategoriesCrudLogic : ICategoriesCrudLogic
    {
        private readonly ICategoriesCrudRepository categoriesCrudRepository;

        private readonly IGuidGenerator guidGenerator;
        private readonly ILogger<CategoriesCrudLogic> logger;

        public CategoriesCrudLogic(
            ICategoriesCrudRepository categoriesCrudRepository,
            IGuidGenerator guidGenerator,
            ILogger<CategoriesCrudLogic> logger)
        {
            this.categoriesCrudRepository = categoriesCrudRepository;

            this.guidGenerator = guidGenerator;
            this.logger = logger;
        }

        public ILogicResult<Guid> CreateCategory(ICategoryCreate categoryCreate)
        {
            if (!this.categoriesCrudRepository.DoesCategoryExist(categoryCreate.SuperCategoryId))
            {
                this.logger.LogDebug("SuperCategory konnte nicht gefunden werden.");
                return LogicResult<Guid>.NotFound("SuperCategory konnte nicht gefunden werden.");
            }

            Guid newCategoryId = this.guidGenerator.NewGuid();
            IDbCategory dbCategoryToCreate = Category.CreateDbCategory(newCategoryId, categoryCreate);
            this.categoriesCrudRepository.CreateCategory(dbCategoryToCreate);

            this.logger.LogInformation($"Category ({newCategoryId}) angelegt");
            return LogicResult<Guid>.Ok(newCategoryId);
        }

        public ILogicResult DeleteCategory(Guid categoryId)
        {
            if (!this.categoriesCrudRepository.DoesCategoryExist(categoryId))
            {
                this.logger.LogDebug($"Category ({categoryId}) konnte nicht gefunden werden.");
                return LogicResult.NotFound($"Category ({categoryId}) konnte nicht gefunden werden.");
            }

            // TODO: If relations are implemented, resolve conflict with the FOREIGN KEY constraint
            try
            {
                this.categoriesCrudRepository.DeleteCategory(categoryId);
            }
            catch (DbUpdateException)
            {
                this.logger.LogDebug($"Category ({categoryId}) konnte nicht gel??scht werden.");
                return LogicResult.Conflict($"Category ({categoryId}) konnte nicht gel??scht werden.");
            }

            this.logger.LogInformation($"Category ({categoryId}) gel??scht");
            return LogicResult.Ok();
        }

        public ILogicResult<ICategoryDetail> GetCategoryDetail(Guid categoryId)
        {
            IDbCategoryDetail dbCategoryDetail = this.categoriesCrudRepository.GetCategoryDetail(categoryId);
            if (dbCategoryDetail == null)
            {
                this.logger.LogDebug($"Category ({categoryId}) konnte nicht gefunden werden.");
                return LogicResult<ICategoryDetail>.NotFound($"Category ({categoryId}) konnte nicht gefunden werden.");
            }

            this.logger.LogDebug($"Details f??r Category ({categoryId}) wurde geladen");
            return LogicResult<ICategoryDetail>.Ok(CategoryDetail.FromDbCategoryDetail(dbCategoryDetail));
        }

        public ILogicResult<IPagedResult<ICategoryListItem>> GetPagedCategories()
        {
            IDbPagedResult<IDbCategoryListItem> dbCategoriesPagedResult =
                this.categoriesCrudRepository.GetPagedCategories();

            IPagedResult<ICategoryListItem> categoriesPagedResult =
                PagedResult.FromDbPagedResult(
                    dbCategoriesPagedResult,
                    (dbCategory) => CategoryListItem.FromDbCategoryListItem(dbCategory));

            this.logger.LogDebug("Categories wurden geladen");
            return LogicResult<IPagedResult<ICategoryListItem>>.Ok(categoriesPagedResult);
        }

        public ILogicResult UpdateCategory(ICategoryUpdate categoryUpdate)
        {
            IDbCategory dbCategoryToUpdate = this.categoriesCrudRepository.GetCategory(categoryUpdate.Id);
            if (dbCategoryToUpdate == null)
            {
                this.logger.LogDebug($"Category ({categoryUpdate.Id}) konnte nicht gefunden werden.");
                return LogicResult.NotFound($"Category ({categoryUpdate.Id}) konnte nicht gefunden werden.");
            }

            if (!this.categoriesCrudRepository.DoesCategoryExist(categoryUpdate.SuperCategoryId))
            {
                this.logger.LogDebug("SuperCategory konnte nicht gefunden werden.");
                return LogicResult.NotFound("SuperCategory konnte nicht gefunden werden.");
            }

            this.categoriesCrudRepository.UpdateCategory(DbCategoryUpdate
                .FromCategoryUpdate(categoryUpdate));

            this.logger.LogInformation($"Category ({categoryUpdate.Id}) aktualisiert");
            return LogicResult.Ok();
        }
    }
}