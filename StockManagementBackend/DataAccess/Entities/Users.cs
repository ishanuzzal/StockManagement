using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Entities
{
    public class Users : IdentityUser, ITimeAuditable
    {
        [Required]
        public DateTime CreatedAtUtc { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }

        public ICollection<Products> Products { get; set; } = new List<Products>();
        public ICollection<Transactions> Transactions { get; set; }
    }
}
