using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.dtos
{
    public class PaginationDataReturnDto<T>
    {
        public ICollection<T> Data { get; set; }
        public int? TotalItemDataBase { get; set; }

        public int? TotalItemInList { get; set; }
    }
}
