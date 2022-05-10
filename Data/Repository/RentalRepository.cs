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
    public interface IRentalRepository : IDisposable
    {
        void Create(new_rental rental);
        Task<List<new_rental>> GetAll();
        Task<new_rental> GetById(Guid rentalId);
        Task<List<new_rental>> GetByBookId(Guid bookId);
        Task<List<new_rental>> GetByCustomerId(Guid customerId);
        System.Threading.Tasks.Task Update(new_rental rental);
        void Delete(new_rental rental);
    }
   
    public class RentalRepository : IRentalRepository
    {
        
        private readonly ServiceClient _serviceClient;
        public RentalRepository( ServiceClient serviceClient)
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
        public async Task<List<new_rental>> GetAll()
        {
            using (var context = new CrmServiceContext(_serviceClient))
            {
                return context.new_rentalSet.Include(e => e.new_new_book_new_rental_bookId).Include(c => c.new_new_customer_new_rental_customerId).ToList();
            }
           

        }
        public async Task<new_rental> GetById(Guid rentalId)
        {
            using (var context = new CrmServiceContext(_serviceClient))
            {
                return context.new_rentalSet.FirstOrDefault(rental =>rental.new_rentalId == rentalId);
            }
        }
        public async Task<List<new_rental>> GetByBookId(Guid bookId)
        {
            using (var context = new CrmServiceContext(_serviceClient))
            {
                return context.new_rentalSet.Where(rental => rental.new_bookId.Id == bookId).Include(e => e.new_new_book_new_rental_bookId).Include(c => c.new_new_customer_new_rental_customerId).ToList();
            }
            
        }
        public async Task<List<new_rental>> GetByCustomerId(Guid customerId)
        {
            using (var context = new CrmServiceContext(_serviceClient))
            {
                return context.new_rentalSet.Where(rental => rental.new_customerId.Id == customerId).Include(e => e.new_new_book_new_rental_bookId).Include(c => c.new_new_customer_new_rental_customerId).ToList();
            }
        }
        public void Create(new_rental rental)
        {
            using (var context = new CrmServiceContext(_serviceClient))
            {
                 context.AddObject(rental);
                context.SaveChanges();
            }
            
        }
        
        public async System.Threading.Tasks.Task Update(new_rental rental)
        {
            await _serviceClient.UpdateAsync(rental);
        }

        public void Delete(new_rental rental)
        {
            using (var context = new CrmServiceContext(_serviceClient))
            {
                context.DeleteObject(rental);
                context.SaveChanges();
            }
        }
    }
}
