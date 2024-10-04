using DataAccess.Enums;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Transactions : ITimeAuditable
    {
        [Key]
        public int Id { get; set; }

        [Range(0, double.MaxValue)] 
        public double Qty { get; set; }

        [Range(0, double.MaxValue)] 
        public double TotalPrice { get; set; }

        [Required]
        public TransactionType TransactionTypes { get; set; }

        public string? UserId { get; set; }

        public Users Users { get; set; }

        public int ProductsId { get; set; }

        public Product Products { get; set; }

        public int BussinessEntitiesId { get; set; }
        public BussinessEntities BussinessEntities { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }
    }
}
