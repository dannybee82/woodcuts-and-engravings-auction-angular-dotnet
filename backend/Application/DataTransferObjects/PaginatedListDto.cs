using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransferObjects
{
    public class PaginatedListDto<T>
    {
        public int CurrentPage { get; set; }
        public int From { get; set; }
        public List<T> Items { get; set; }
        public int PageSize { get; set; }
        public int To { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
