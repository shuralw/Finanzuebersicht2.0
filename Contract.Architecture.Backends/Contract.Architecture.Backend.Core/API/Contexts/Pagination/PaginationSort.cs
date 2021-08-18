using Contract.Architecture.Backend.Core.Contract.Contexts;

namespace Contract.Architecture.Backend.Core.API.Contexts.Pagination
{
    internal class PaginationSort : IPaginationSortItem
    {
        public string PropertyName { get; set; }

        public SortOrder OrderBy { get; set; }
    }
}