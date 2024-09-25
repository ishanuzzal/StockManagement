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

        [Range(0, double.MaxValue)]
        public double StockAmount { get; set; }

        [Range(0, double.MaxValue)]
        public double UnitPrice { get; set; }

        [Required]
        [MaxLength(20)]
        public string UnitType { get; set; }

        [Required]
        public int categoryId { get; set; }
        public List<ShowCategoryDto> categories { get; set; }
    }

    public class ShowProdcutDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double StockAmount { get; set; }
        public double UnitPrice { get; set; }
        public string CategoryName { get; set; }

    }
}
