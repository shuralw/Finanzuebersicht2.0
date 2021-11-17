using Contract.Architecture.Backend.Core.Contract.Contexts;
using Moq;
using System.Collections.Generic;

namespace Contract.Architecture.Backend.Core.Persistence.Tests.Tools.Pagination
{
    internal class PaginationContextTest
    {
        public static IPaginationContext SetupPaginationContextDefault()
        {
            Mock<IPaginationContext> paginationContext = new Mock<IPaginationContext>();
            paginationContext.Setup(context => context.Limit).Returns(10);
            paginationContext.Setup(context => context.Offset).Returns(0);
            paginationContext.Setup(context => context.Sort).Returns(new Dictionary<string, IPaginationSortItem>());
            paginationContext.Setup(context => context.Filter).Returns(new Dictionary<string, IPaginationFilterItem>());
            paginationContext.Setup(context => context.CustomSort).Returns(new Dictionary<string, IPaginationSortItem>());
            paginationContext.Setup(context => context.CustomFilter).Returns(new Dictionary<string, IPaginationFilterItem>());
            return paginationContext.Object;
        }
    }
}