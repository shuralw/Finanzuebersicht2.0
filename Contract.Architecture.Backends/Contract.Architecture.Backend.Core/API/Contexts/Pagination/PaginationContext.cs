﻿using Contract.Architecture.Backend.Core.Contract.Contexts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Contract.Architecture.Backend.Core.API.Contexts.Pagination
{
    public class PaginationContext : IPaginationContext
    {
        private const int DefaultLimit = 10;

        private readonly IHttpContextAccessor httpContextAccessor;

        public PaginationContext(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public int Limit
        {
            get
            {
                try
                {
                    if (this.httpContextAccessor.HttpContext.Request.Query.ContainsKey("limit"))
                    {
                        string limitString = this.httpContextAccessor.HttpContext.Request.Query["limit"].First();
                        int limit = Convert.ToInt32(limitString);

                        if (limit <= 0)
                        {
                            return DefaultLimit;
                        }

                        return limit;
                    }
                }
                catch
                {
                }

                return DefaultLimit;
            }
        }

        public int Offset
        {
            get
            {
                try
                {
                    if (this.httpContextAccessor.HttpContext.Request.Query.ContainsKey("offset"))
                    {
                        string offsetString = this.httpContextAccessor.HttpContext.Request.Query["offset"].First();
                        int offset = Convert.ToInt32(offsetString);

                        if (offset < 0)
                        {
                            return 0;
                        }

                        return Convert.ToInt32(offsetString);
                    }
                }
                catch
                {
                }

                return 0;
            }
        }

        public IEnumerable<IPaginationFilterItem> Filter
        {
            get
            {
                PaginationAttribute paginationAttribut =
                    this.httpContextAccessor.HttpContext
                        .GetEndpoint().Metadata
                        .GetMetadata<PaginationAttribute>();

                return this.GetFilterItemsFromQuery()
                    .Where(paginationFilterItem => paginationAttribut.FilterFields
                        .Select(filterField => filterField.ToLower())
                        .Contains(paginationFilterItem.PropertyName.ToLower()));
            }
        }

        public IEnumerable<IPaginationFilterItem> CustomFilter
        {
            get
            {
                PaginationAttribute paginationAttribut =
                    this.httpContextAccessor.HttpContext
                        .GetEndpoint().Metadata
                        .GetMetadata<PaginationAttribute>();

                return this.GetFilterItemsFromQuery()
                    .Where(paginationFilterItem => paginationAttribut.CustomFilterFields
                        .Select(customFilterFields => customFilterFields.ToLower())
                        .Contains(paginationFilterItem.PropertyName.ToLower()));
            }
        }

        public IEnumerable<IPaginationSortItem> Sort
        {
            get
            {
                PaginationAttribute paginationAttribut =
                    this.httpContextAccessor.HttpContext
                        .GetEndpoint().Metadata
                        .GetMetadata<PaginationAttribute>();

                return this.httpContextAccessor.HttpContext.Request.Query
                    .Where((keyValue) =>
                    {
                        bool isEqualFilter = keyValue.Key == "sort";
                        bool isAdvancedEqualFilter = Regex.IsMatch(keyValue.Key, @"^sort\.(asc|desc)$", RegexOptions.IgnoreCase);
                        return isEqualFilter || isAdvancedEqualFilter;
                    })
                    .Select(keyValue =>
                    {
                        string[] sortSplit = keyValue.Key.Split(".");

                        PaginationSort paginationSort = new PaginationSort()
                        {
                            OrderBy = this.ExtractOrderBy(sortSplit),
                            PropertyName = keyValue.Value,
                        };

                        return paginationSort;
                    })
                    .Where(paginationSortItem => paginationAttribut.SortFields
                        .Select(sortField => sortField.ToLower())
                        .Contains(paginationSortItem.PropertyName.ToLower()));
            }
        }

        private IEnumerable<IPaginationFilterItem> GetFilterItemsFromQuery()
        {
            return this.httpContextAccessor.HttpContext.Request.Query
                .Where((keyValue) =>
                {
                    bool isEqualFilter = Regex.IsMatch(keyValue.Key, @"^filter\.[a-zA-Z0-9]+$", RegexOptions.IgnoreCase);
                    bool isAdvancedEqualFilter = Regex.IsMatch(keyValue.Key, @"^filter\.[a-zA-Z0-9]+\.(eq|gt|gte|lt|lte)$", RegexOptions.IgnoreCase);
                    return isEqualFilter || isAdvancedEqualFilter;
                })
                .Select(keyValue =>
                {
                    string[] filterSplit = keyValue.Key.Split(".");

                    PaginationFilterItem paginationFilterItem = new PaginationFilterItem()
                    {
                        FilterType = this.ExtractFilterType(filterSplit),
                        PropertyName = filterSplit[1],
                        PropertyValue = keyValue.Value,
                    };

                    return paginationFilterItem;
                });
        }

        private FilterType ExtractFilterType(string[] filterSplit)
        {
            if (filterSplit.Length == 3)
            {
                switch (filterSplit[2].ToLower())
                {
                    case "lt":
                        return FilterType.LessThan;

                    case "lte":
                        return FilterType.LessThanOrEqual;

                    case "gt":
                        return FilterType.GreaterThan;

                    case "gte":
                        return FilterType.GreaterThanOrEqual;

                    case "eq":
                    default:
                        return FilterType.Equal;
                }
            }
            else
            {
                return FilterType.Equal;
            }
        }

        private SortOrder ExtractOrderBy(string[] filterSplit)
        {
            if (filterSplit.Length == 2)
            {
                switch (filterSplit[1].ToLower())
                {
                    case "desc":
                        return SortOrder.DESC;

                    case "asc":
                    default:
                        return SortOrder.ASC;
                }
            }
            else
            {
                return SortOrder.ASC;
            }
        }
    }
}