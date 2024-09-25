using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.dtos
{
    public class ShowTransactionDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double Qty { get; set; }
        public string? UnitType { get; set; }
        public double TotalPrice { get; set; }
        public string? TransactionTypes { get; set; }
        public DateTime DateTime { get; set; }

    }

    public class AddTransactionDto
    {
        public double Qty { get; set; }
        public string? UnitType { get; set; }
        public double TotalPrice { get; set; }
        public string? TransactionTypes { get; set; }
        public DateTime DateTime { get; set; }
    }
}
