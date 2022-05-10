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
    public interface IBookRepository  :IDisposable
    {
        void Create(new_book book);
        Task<new_book> GetById(Guid bookId);
        Task<new_book> GetByIsbn(string isbn);
        //Task<List<BookEntity>> GetAllWithFilter(string isbn, string name, double? price, double? quantity);
        Task<List<new_book>> GetAll();
        System.Threading.Tasks.Task Update(new_book book);
        void Delete(new_book book);
    }
    public class BookRepository : IBookRepository

    {
       
        private readonly ServiceClient _serviceClient;
        public BookRepository(ServiceClient serviceClient)
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
        public async Task<List<new_book>> GetAll()
        {
            using(var context = new CrmServiceContext(_serviceClient))
            {
                return context.new_bookSet.ToList();
               
            }
        }
        public async Task<new_book> GetById(Guid bookId)
        {
            
            using (var context = new CrmServiceContext(_serviceClient))
            {
                var x= context.new_bookSet.FirstOrDefault(book => book.new_bookId == bookId);
                context.SaveChanges();
                return x;
                
            }
        }
        public async Task<new_book> GetByIsbn(string isbn)
        {
            using (var context = new CrmServiceContext(_serviceClient))
            {
                return context.new_bookSet.FirstOrDefault(book => book.new_isbn == isbn);
               // return context.new_bookSet.Where(book => book.new_isbn == isbn).FirstOrDefault();
            }
        }

        public void Create(new_book book)
        {
            using (var context = new CrmServiceContext(_serviceClient))
            {
                context.AddObject(book);
                context.SaveChanges();
            }
            
            
        }
        public async System.Threading.Tasks.Task Update(new_book book)
        {
            using (var context = new CrmServiceContext(_serviceClient))
            {
                if (!context.IsAttached(book))
                {
                    context.Attach(book);
                }
                context.UpdateObject(book);
                context.SaveChanges();
            }
            //await _serviceClient.UpdateAsync(book);
        }

        public void Delete(new_book book)
        {
            using (var context = new CrmServiceContext(_serviceClient))
            {
                context.DeleteObject(book);
                context.SaveChanges();
            }
        }

       
    }
}
