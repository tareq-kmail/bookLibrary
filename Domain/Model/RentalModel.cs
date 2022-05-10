using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
   public class RentalModel
    {
        public Guid Id { set; get; }
        public Guid CustomerId { set; get; }
        public Guid BookId { set; get; }
        public double Price { set; get; }
        public string BookingDate { set; get; }
        public string BookingExpiryDate { set; get; }
        public CustomerModel Customer { set; get; }
        public BookModel Book { set; get; }
    }
}
