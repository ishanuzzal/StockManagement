using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Transactions
    {
        [Key]
        public int Id { get; set; }

        [Range(0, double.MaxValue)] 
        public double Qty { get; set; }

        [Required]
        [MaxLength(20)]
        public string? UnitType { get; set; }

        [Range(0, double.MaxValue)] 
        public double TotalPrice { get; set; }

        [Required]
        [MaxLength(20)] 
        public string? TransactionTypes { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public string? UserId { get; set; }

        //[ForeignKey("UserId")]
        public Users Users { get; set; }

        public string? ProductsId { get; set; }

        //[ForeignKey("ProductsId")]
        public Products Products { get; set; }
    }
}
