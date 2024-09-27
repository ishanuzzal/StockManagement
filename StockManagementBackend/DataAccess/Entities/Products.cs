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
    public class Products : ITimeAuditable
    {
        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        [Range(0, double.MaxValue)] 
        public double StockAmount { get; set; }

        [Range(0, double.MaxValue)]
        public double MinStockAmount { get; set; } = 0;

        [Range(0, double.MaxValue)] 
        public double UnitPrice { get; set; }

        [Required]
        [MaxLength(20)] 
        public string UnitType { get; set; }

        [Required]
        public DateTime CreatedAtUtc { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }
        public string? UserId { get; set; }

        //[ForeignKey("UserId")]
        public Users User { get; set; }

        public int? CategoriesId { get; set; }

        //[ForeignKey("CategoriesId")]
        public Categories Categories { get; set; }

        public ICollection<Transactions> Transactions { get; set; }

    }
}
