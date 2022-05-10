using AutoMapper;
using Data.Repository;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrmEarlyBound;
using Microsoft.PowerPlatform.Dataverse.Client;

namespace Domain.Manager
{
    public interface ISalesManager
    {
        Task<SalesModel> Create(SalesModel sales);
       Task<List<SalesModel>> GetAll();
        Task<SalesModel> GetById(Guid salesId);
        Task<List<SalesModel>> GetByBookId(Guid bookId);
        Task<List<SalesModel>> GetByCustomerId(Guid customerId);
        Task<SalesModel> Update(SalesModel sale);
        bool Delete(Guid saleId);
    }
    public class SalesManager : ISalesManager
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IBookManager _bookManager;
        private readonly IMapper _mapper;
        private readonly ServiceClient _serviceClient;
        public SalesManager(ISalesRepository salesRepository, IBookManager bookManager, IMapper mapper, ServiceClient serviceClient)
        {
            _salesRepository = salesRepository;
            _bookManager = bookManager;
            _mapper = mapper;
            _serviceClient = serviceClient;
        }
        public async Task<List<SalesModel>> GetAll()
        {
            return _mapper.Map<List<SalesModel>>(await _salesRepository.GetAll());
        }
        public async Task<SalesModel> GetById(Guid id)
        {
            return _mapper.Map<SalesModel>(await _salesRepository.GetById(id));//await same as Result
        }
        public async Task<List<SalesModel>> GetByBookId(Guid id)
        {
            return _mapper.Map<List<SalesModel>>( _salesRepository.GetByBookId(id).Result);
        }
        public async Task<List<SalesModel>> GetByCustomerId(Guid id)
        {
            return _mapper.Map<List<SalesModel>>(_salesRepository.GetByCustomerId(id).Result);
        }
        public async Task<SalesModel> Create(SalesModel sales)
        {
            var salesEn = _mapper.Map<new_sales>(sales);
             _salesRepository.Create(salesEn);
            var sale = _salesRepository.GetById(salesEn.Id).Result;
            var book = _bookManager.GetById(sales.BookId).Result;
            book.Quantity = book.Quantity - sales.Quantity;
            var bookUp = _bookManager.Update(book).Result;
            return _mapper.Map<SalesModel>(sale);
        }
        public async Task<SalesModel> Update (SalesModel sales)
        {
            var toUPdate = _salesRepository.GetById(sales.Id).Result;
            toUPdate.new_price = sales.Price;
            toUPdate.new_quantity = sales.Quantity;
           await _salesRepository.Update(toUPdate);
            return _mapper.Map<SalesModel>(_salesRepository.GetById(toUPdate.Id).Result);
            
            
        }

        public bool Delete(Guid salesId)
        {
            var sales = _salesRepository.GetById(salesId).Result;
            if (sales != null)
            {
                _salesRepository.Delete(sales);
                return true;
            }
            return false;
        }
    }
}
