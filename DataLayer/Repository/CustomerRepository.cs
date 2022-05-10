using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface ICustomerRepository
    {
        void Create(CustomerEntity customer);
        List<CustomerEntity> GetAll();
        CustomerEntity GetById(Guid customerId);
        void Update(CustomerEntity customer);
        void Delete(CustomerEntity customer);
    }
   public class CustomerRepository : ICustomerRepository
    {
        private readonly LibraryContext _library;
        public CustomerRepository(LibraryContext library)
        {
            _library = library;
        }
        public List<CustomerEntity> GetAll()
        {
            return _library.Customers.ToList();
        }
        public CustomerEntity GetById(Guid customerId)
        {
            return _library.Customers.FirstOrDefault(customer => customer.Id == customerId);
        }
        public void Create(CustomerEntity customer)
        {
            _library.Customers.Add(customer);
            _library.SaveChanges();
        }
        public void Update(CustomerEntity customer)
        {
            _library.Customers.Update(customer);
            _library.SaveChanges();
        }

        public void Delete(CustomerEntity customer)
        {
            _library.Customers.Remove(customer);
            _library.SaveChanges();
        }
    }
}
