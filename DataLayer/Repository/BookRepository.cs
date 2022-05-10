using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;

namespace Data.Repository 
{
    public interface IBookRepository
    {
        void Create(BookEntity book);
        BookEntity GetById(Guid bookId);
        BookEntity GetByIsbn(string isbn);
        BookEntity GetByName(string name);
        List<BookEntity> GetAll();
        void Update(BookModel book);
        void Delete(BookEntity book);
    }
    public class BookRepository : IBookRepository
    {
        private readonly LibraryContext _library; 
        public BookRepository(LibraryContext library)
        {
            _library = library;
        }
        public List<BookEntity> GetAll()
        {
            return _library.Books.ToList();
        }
        public BookEntity GetById(Guid bookId)
        {
            return _library.Books.FirstOrDefault(book => book.Id == bookId);
        }
        public BookEntity GetByIsbn(string isbn)
        {
            return _library.Books.FirstOrDefault(book => book.Isbn == isbn);
        }
        public BookEntity GetByName(string name)
        {
            return _library.Books.FirstOrDefault(book => book.Name == name);
        }
        public void Create(BookEntity book)
        {
             _library.Books.Add(book);
             _library.SaveChanges();
        }
        public void Update(BookModel book)
        {
            _library.Books.Update(book);
            _library.SaveChanges();
        }

        public void Delete(BookEntity book)
        {
            _library.Books.Remove(book);
            _library.SaveChanges();
        }

       
    }
}
