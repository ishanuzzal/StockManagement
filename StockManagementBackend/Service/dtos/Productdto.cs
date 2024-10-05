using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.dtos
{
    public class AddProductDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        private string _sku;

        [Required]
        public string SKU { get=>_sku; set=>_sku=value.ToLower(); }

        [MaxLength(200)]
        public string? Description { get; set; }

        [Range(0, double.MaxValue)]
        public double StockAmount { get; set; }

        [Range(0, double.MaxValue)]
        public double UnitPrice { get; set; }

        [Required]
        [MaxLength(20)]
        public string UnitType { get; set; }
        [Required]
        public int CategoriesId { get; set; }
        [Required]
        public int BusinessEntitiesId {  get; set; }
    }

    public class SellProductDto {

        [Required]
        public int Id {  get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0, double.MaxValue)]
        public double Qty { get; set; }

        [Range(0, double.MaxValue)]
        public double UnitPrice { get; set; }
        public int BussinessEntitiesId { get; set; }

    }


    public class UpdateProductDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string SKU { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        [Range(0, double.MaxValue)]
        public double StockAmount { get; set; }

        [Range(0, double.MaxValue)]
        public double UnitPrice { get; set; }

        [Required]
        [MaxLength(20)]
        public string UnitType { get; set; }

        [Required]
        public int CategoriesId { get; set; }
    }

    public class ShowProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }

        public string Description { get; set; }
        public double StockAmount { get; set; }
        public double UnitPrice { get; set; }
        public string CategoryName { get; set; }
        
        public string StockLevel { get; set; }

    }
}
