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
    public class BussinessEntities:ITimeAuditable
    {
        [Key]
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

        [ForeignKey("UserId")]
        public string UsersId { get; set; }
        public Users Users { get; set; }

        [Required]
        public DateTime CreatedAtUtc { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }
        public ICollection<Transactions> Transactions { get; set; }
    }
}
