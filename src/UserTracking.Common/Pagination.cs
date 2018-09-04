using System;

namespace UserTracking.Common
{
    public class Pagination
    {
        private const int MaxPageSize = 100000;

        public Pagination(int pageNumber) : this(pageNumber, 10)
        {

        }
        public Pagination(int pageNumber, int pageSize)
        {
            if(pageNumber < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be a positive number.");
            }
            if(pageSize < 1 || pageSize > 100000)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), $"Page size must be a number between 1 and {MaxPageSize}.");
            }
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public int PageNumber { get; }

        public int PageSize { get; }
    }
}
