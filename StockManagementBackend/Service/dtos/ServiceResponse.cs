using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.dtos
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
    }

    public class PaginatedServiceResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public int? TotalItemDataBase { get; set; }

        public int? TotalItemInList { get; set; }
    }
}
