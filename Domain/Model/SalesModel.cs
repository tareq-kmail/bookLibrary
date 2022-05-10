using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class SalesModel
    {
        public Guid Id { set; get; }
        public Guid CustomerId { set; get; }
        public Guid BookId { set; get; }
        public int Quantity { set; get; }
        public double Price { set; get; }
        public string Date { set; get; }
        public CustomerModel Customer { set; get; }
        public BookModel Book  { set; get; }
}
}
