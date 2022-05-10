using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class AdminEntity
    {
        [Key]
        public Guid Id { set; get; }

        [Required]
        public string NationalId { set; get; }

        [Required]
        public string Username { set; get; }
        public string Name { set; get; }
        public string Phone { set; get; }
        [Required]
        public string Password { set; get; }
        public string Address { set; get; }
    }
}
