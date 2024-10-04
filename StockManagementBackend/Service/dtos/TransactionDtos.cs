using DataAccess.Enums;
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

    public class SellTransactionDto
    {
        [Required]
        public double Qty { get; set; }

        [Required]
        public double TotalPrice { get; set; }
        [Required]
        public TransactionType TransactionTypes { get; set; } = TransactionType.Sell;
        [Required]
        public string UserId { get; set; }

        [Required]
        public int ProductsId { get; set; }

        [Required]
        public int BussinessEntitiesId { get; set; }
    }

    public class BuyTransactionDto
    {
        [Required]
        public double Qty { get; set; }

        [Required]
        public double TotalPrice { get; set; }
        [Required]
        public TransactionType TransactionTypes { get; set; } = TransactionType.Buy;
        [Required]
        public string UserId { get; set; }
        [Required]
        public int CategoriesId { get; set; }

        [Required]
        public int BussinessEntitiesId { get; set; }
    }

    public class PrintBuyTransaction
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public double Qty { get; set; }

        [Required]
        public double TotalPrice { get; set; }
        [Required]
        public TransactionType TransactionTypes { get; set; } = TransactionType.Buy;
        [Required]
        public string Buyer {  get; set; }
      
    }

    public class PrintSellTransaction
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public double Qty { get; set; }

        [Required]
        public double TotalPrice { get; set; }
        [Required]
        public TransactionType TransactionTypes { get; set; } = TransactionType.Sell;
        [Required]
        public string Seller { get; set; }

    }
}
