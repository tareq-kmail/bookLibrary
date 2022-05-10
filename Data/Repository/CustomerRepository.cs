using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrmEarlyBound;
using Microsoft.PowerPlatform.Dataverse.Client;
namespace Data.Repository
{
    public interface ICustomerRepository : IDisposable
    {
        void Create(new_customer customer);
        Task<new_customer> GetById(Guid customerId);
        Task<new_customer> GetByNationalId(string nationalId);
        //Task<List<BookEntity>> GetAllWithFilter(string isbn, string name, double? price, double? quantity);
        Task<List<new_customer>> GetAll();
        System.Threading.Tasks.Task Update(new_customer customer);
        void Delete(new_customer customer);
    }
    public class CustomerRepository : ICustomerRepository
    {
        
        private readonly ServiceClient _serviceClient;
        public CustomerRepository( ServiceClient serviceClient)
        {
            
            _serviceClient = serviceClient;
        }

        public void Dispose()
        {
            using (var context = new CrmServiceContext(_serviceClient))
            {
                context.Dispose();
            }


        }
        //public async Task<List<BookEntity>> GetAllWithFilter(string? isbn, string? name, double? price, double? quantity)
        //{
        //    var books =  _library.Books.Where(e =>
        //                                          (string.IsNullOrEmpty(isbn) || e.Isbn.Equals(isbn))
        //                                        ||( string.IsNullOrEmpty(name) || e.Name.Contains(name))
        //                                        || ( price == null || e.Price == price)
        //                                        || ( quantity == null || e.Quantity == quantity)
        //                                     ).ToList();
        //    return  books;

        //}
        public async Task<List<new_customer>> GetAll()
        {
            using (var context = new CrmServiceContext(_serviceClient))
            {
                return context.new_customerSet.ToList();
            }
           
        }
        public async Task<new_customer> GetById(Guid customerId)
        {
            using (var context = new CrmServiceContext(_serviceClient))
            {
                return context.new_customerSet.FirstOrDefault(customer => customer.new_customerId == customerId);
            }
            
        }
        public async Task<new_customer> GetByNationalId(string nationalId)
        {
            using (var context = new CrmServiceContext(_serviceClient))
            {
                return context.new_customerSet.FirstOrDefault(customer => customer.new_nationalId == nationalId);
            }
        }

        public void Create(new_customer customer)
        {
            using (var context = new CrmServiceContext(_serviceClient))
            {
                context.AddObject(customer);
                context.SaveChanges();
            }

        }
        
        public async System.Threading.Tasks.Task Update(new_customer customer)
        {
            await _serviceClient.UpdateAsync(customer);
        }

        public void Delete(new_customer customer)
        {
            using (var context = new CrmServiceContext(_serviceClient))
            {
                context.DeleteObject(customer);
                context.SaveChanges();
            }
        }


    }
}
