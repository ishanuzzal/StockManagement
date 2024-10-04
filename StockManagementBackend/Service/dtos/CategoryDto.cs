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

    public class ShowCategoryDropdown
    {
        public int Id { get; set; }
        public string _name;
        public string Name { get => _name; set => _name = value.ToUpper(); }

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
