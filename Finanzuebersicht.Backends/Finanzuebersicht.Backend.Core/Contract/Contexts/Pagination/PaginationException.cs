using System;

namespace Finanzuebersicht.Backend.Core.Contract.Contexts.Pagination
{
    public class PaginationException : ApplicationException
    {
        public PaginationException(string message, Exception ex)
            : base(message, ex)
        {
        }
    }
}