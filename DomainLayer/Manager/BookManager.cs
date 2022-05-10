using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repository;
using AutoMapper;
using Data.Entity;

namespace Domain.Manager
{
    public interface IBookManager
    {
        bool Create(BookModel book);
        BookModel GetById(Guid bookId);
        BookModel GetByIsbn(string isbn);
        BookModel GetByName(string name);
        List<BookModel> GetAll();       
        bool Update(BookModel book);  
        bool Delete(Guid bookId);  
    }
    public class BookManager : IBookManager
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper; 
        public BookManager(IBookRepository bookRepository , IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public BookModel GetById(Guid bookId)
        {
          return  _mapper.Map<BookModel>( _bookRepository.GetById(bookId));
        }
        public BookModel GetByIsbn(string isbn)
        {
            return _mapper.Map<BookModel>(_bookRepository.GetByIsbn(isbn));
        }
        public BookModel GetByName(string name)
        {
            return _mapper.Map<BookModel>(_bookRepository.GetByName(name));
        }
        public List<BookModel> GetAll()
        {
            return _mapper.Map<List<BookModel>>(_bookRepository.GetAll());   
        }
        public bool Create(BookModel book)
        {
            var bookEn = _mapper.Map<BookEntity>(book);
            _bookRepository.Create(bookEn);
            return true;
        }
        public bool Update(BookModel book)
        {
           // var bookEn = _mapper.Map<BookEntity>(book);
            _bookRepository.Update(book);
            return true; 
        }

        public bool Delete(Guid bookId)
        {
            var book = _bookRepository.GetById(bookId);
            if (book != null)
            {
                _bookRepository.Delete(book);
                return true; 
            }
            return false;
        }
    }
}
