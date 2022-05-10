using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class BookEntity
    {
        [Key]
        public Guid Id { set; get; }

        [Required]
        public string Isbn { set; get; }

        [Required]
        public string Name { set; get; }

        [Required]
        public double Price { set; get; }

        [Required]
        public int Quantity { set; get; }
    }
}
