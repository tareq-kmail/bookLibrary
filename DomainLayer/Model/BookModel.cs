using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class BookModel
    {
        public Guid Id { set; get; }
        public string Isbn { set; get; }
        public string Name { set; get; }
        public double Price { set; get; }
        public int Quantity { set; get; }
    }
}
