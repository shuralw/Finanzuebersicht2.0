using Contract.Architecture.Backend.Core.Contract.Contexts;

namespace Contract.Architecture.Backend.Core.API.Contexts.Pagination
{
    internal class PaginationFilterItem : IPaginationFilterItem
    {
        public string PropertyName { get; set; }

        public string PropertyValue { get; set; }

        public string PropertyQuery { get; set; }

        public FilterType FilterType { get; set; }
    }
}