using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.dtos
{
    public class ShowCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ProductCount { get; set; } = 0;
    }

    public class AddCategoryDto
    {
        [Required]
        public string Name { get; set; }
    }

    public class UpdateCategoryDto {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
