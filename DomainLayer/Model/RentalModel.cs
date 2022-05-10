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
        public double Price { set; get; }
        public string Booking_date { set; get; }
        public string Booking_expiry_date { set; get; }
        public CustomerModel Customer { set; get; }
        public BookModel Book { set; get; }
    }
}
