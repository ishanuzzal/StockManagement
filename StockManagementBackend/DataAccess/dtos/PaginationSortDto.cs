using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.dtos
{
    public class PaginationSortDto_DataAccess
    {
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;

        public string SortBy { get; set; } = "Id";

        public string SortOrder { get; set; }  = "asc";
    }
}
