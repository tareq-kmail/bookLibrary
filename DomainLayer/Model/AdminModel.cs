using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class AdminModel
    {
        public Guid Id { set; get; }
        public string Nationalid { set; get; }
        public string Username { set; get; }
        public string Name { set; get; }
        public string Phone { set; get; }
        public string Password { set; get; }
        public string Address { set; get; }
    }
}
