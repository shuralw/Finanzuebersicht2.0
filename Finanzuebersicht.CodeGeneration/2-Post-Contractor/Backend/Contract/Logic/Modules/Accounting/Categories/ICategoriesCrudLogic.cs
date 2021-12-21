using Finanzuebersicht.Backend.Generated.Contract.Logic.LogicResults;
using Finanzuebersicht.Backend.Generated.Contract.Logic.Tools.Pagination;
using System;

namespace Finanzuebersicht.Backend.Generated.Contract.Logic.Modules.Accounting.Categories
{
    public interface ICategoriesCrudLogic
    {
        ILogicResult<Guid> CreateCategory(ICategoryCreate categoryCreate);

        ILogicResult DeleteCategory(Guid categoryId);

        ILogicResult<ICategoryDetail> GetCategoryDetail(Guid categoryId);

        ILogicResult<IPagedResult<ICategoryListItem>> GetPagedCategories();

        ILogicResult UpdateCategory(ICategoryUpdate categoryUpdate);
    }
}