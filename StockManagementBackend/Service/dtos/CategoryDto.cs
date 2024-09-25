using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.dtos
{
    public class ShowCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class AddCategoryDto
    {
        public string Name { get; set; }
    }
}
