using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.dtos
{
    public class AddBusinessEntitiesDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(300)]
        public string Address { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(20)]
        public string Type { get; set; }
    }

    public class ShowBusinessEntitiesDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(300)]
        public string Address { get; set; }

        [EmailAddress] 
        public string Email { get; set;}
        [Required]
        [MaxLength(20)]
        public string Type { get; set; }

        public DateTime CreatedAtUtc { get; set; }
    }

    public class UpdateBusinessEntitiesDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(300)]
        public string Address { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(20)]
        public string Type { get; set; }

    }


}
