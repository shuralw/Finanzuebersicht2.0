using Finanzuebersicht.Backend.Core.Contract.Contexts;

namespace Finanzuebersicht.Backend.Core.API.Contexts.Pagination
{
    internal class PaginationSortItem : IPaginationSortItem
    {
        public string PropertyName { get; set; }

        public SortOrder OrderBy { get; set; }
    }
}