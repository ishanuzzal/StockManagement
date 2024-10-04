using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shared.dtos
{
    public class PaginationSortDto
    {
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;

        public string SortBy { get; set; } = string.Empty;

        public string SortOrder { get; set; }  = string.Empty;

    }
}
