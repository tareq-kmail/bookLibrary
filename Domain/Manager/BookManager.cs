using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repository;
using AutoMapper;
using CrmEarlyBound;
using Microsoft.PowerPlatform.Dataverse.Client;

namespace Domain.Manager
{
    public interface IBookManager
    {
        Task<BookModel> Create(BookModel book);
        Task<BookModel> GetById(Guid bookId);
        Task<BookModel> GetByIsbn(string isbn);
        //Task<List<BookModel>> GetAll(BookFilterModel filter);
        Task<List<BookModel>> GetAll();
        Task<BookModel> Update(BookModel book);  
        bool Delete(Guid bookId);  
    }
    public class BookManager : IBookManager
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly ServiceClient _serviceClient;
        public BookManager(IBookRepository bookRepository , IMapper mapper, ServiceClient serviceClient)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _serviceClient = serviceClient;
        }
        public async Task<BookModel> GetById(Guid bookId)
        {
          return  _mapper.Map<BookModel>(await _bookRepository.GetById(bookId));
        }
        public async Task<BookModel> GetByIsbn(string isbn)
        {
            return _mapper.Map<BookModel>(await _bookRepository.GetByIsbn(isbn));
        }

        //public async Task<List<BookModel>> GetAll(BookFilterModel filter)
        //{
        //    return   _mapper.Map<List<BookModel>>(await _bookRepository.GetAllWithFilter(filter.Isbn,filter.Name,filter.Price,filter.Quantity));   
        //}
        public async Task<List<BookModel>> GetAll()
        {
            return _mapper.Map<List<BookModel>>(await _bookRepository.GetAll());
        }
        public async Task<BookModel> Create(BookModel book)
        {
            var bookEn = _mapper.Map<new_book>(book);
            _bookRepository.Create(bookEn);
            var books = _bookRepository.GetById(bookEn.Id).Result;
            return   _mapper.Map<BookModel>(books); 
        }
        public async Task<BookModel> Update(BookModel book)
        {
            //var toUPdate =  _bookRepository.GetById(book.Id).Result;
            
           // toUPdate.new_name = book.Name;
           // toUPdate.new_price = book.Price;
           // toUPdate.new_quantity = book.Quantity;
            
            await _bookRepository.Update(_mapper.Map <new_book> (book));

           return _mapper.Map<BookModel>(_bookRepository.GetById(book.Id).Result);
        }

        public bool Delete(Guid bookId)
        {
            var book = _bookRepository.GetById(bookId).Result;
            if (book != null)
            {
                book.new_quantity = 0;
                _bookRepository.Update(book);
                return true; 
            }
            return false;
        }
    }
}
