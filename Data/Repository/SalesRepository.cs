using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrmEarlyBound;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
namespace Data.Repository
{
    public interface ISalesRepository : IDisposable
    {
        void Create(new_sales sales);
        Task<List<new_sales>> GetAll();
        Task<new_sales> GetById(Guid salesId);
        Task<List<new_sales>> GetByBookId(Guid bookId);
        Task<List<new_sales>> GetByCustomerId(Guid customerId);
        System.Threading.Tasks.Task Update(new_sales sales);
        void Delete(new_sales sales);
    }
   public class SalesRepository : ISalesRepository
    {
        
        private readonly ServiceClient _serviceClient;
        public SalesRepository( ServiceClient serviceClient)
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
        public async Task<List<new_sales>> GetAll()
        {
            using (var context = new CrmServiceContext(_serviceClient))
            {
                return context.new_salesSet.Include(e => e.new_new_book_new_sales_bookId).Include(c => c.new_new_customer_new_sales_customerId).ToList();
            }

        }
        public async Task<new_sales> GetById(Guid salesId)
        {
            using (var context = new CrmServiceContext(_serviceClient))
            {
                return context.new_salesSet.FirstOrDefault(sales => sales.new_salesId == salesId);
            }
        }
        public async Task<List<new_sales>> GetByBookId(Guid bookId)
        {
            using (var context = new CrmServiceContext(_serviceClient))
            {
                return context.new_salesSet.Where(sales => sales.new_bookId.Id == bookId).Include(e => e.new_new_book_new_sales_bookId).Include(c => c.new_new_customer_new_sales_customerId).ToList();
            }
        }
        public async Task<List<new_sales>> GetByCustomerId(Guid customerId)
        {
            using (var context = new CrmServiceContext(_serviceClient))
            {
                return context.new_salesSet.Where(sales => sales.new_customerId.Id == customerId).Include(e => e.new_new_book_new_sales_bookId).Include(c => c.new_new_customer_new_sales_customerId).ToList();
            }
        }
        public void Create(new_sales sales)
        {
            using (var context = new CrmServiceContext(_serviceClient))
            {
                context.AddObject(sales);
                context.SaveChanges();
            }
            
        }
        public async System.Threading.Tasks.Task Update(new_sales sales)
        {
            await _serviceClient.UpdateAsync(sales);
        }

        public void Delete(new_sales sales)
        {
            using (var context = new CrmServiceContext(_serviceClient))
            {
                context.DeleteObject(sales);
                context.SaveChanges();
            }
        }
    }
}
