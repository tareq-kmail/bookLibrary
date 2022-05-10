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
    public interface ICustomerManager
    {
        Task<CustomerModel> Create(CustomerModel customer);
        Task<CustomerModel> GetById(Guid customerId);
        Task<CustomerModel> GetByNationalId(string nationalId);
        //Task<List<BookModel>> GetAll(BookFilterModel filter);
        Task<List<CustomerModel>> GetAll();
        Task<CustomerModel> Update(CustomerModel book);
        bool Delete(Guid bookId);
    }
    public class CustomerManager : ICustomerManager
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly ServiceClient _serviceClient;
        public CustomerManager(ICustomerRepository customerRepository, IMapper mapper, ServiceClient serviceClient)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _serviceClient = serviceClient;
        }
        public async Task<CustomerModel> GetById(Guid customerId)
        {
            return _mapper.Map<CustomerModel>(await _customerRepository.GetById(customerId));
        }
        public async Task<CustomerModel> GetByNationalId(string nationalId)
        {
            return _mapper.Map<CustomerModel>(await _customerRepository.GetByNationalId(nationalId));
        }

        //public async Task<List<BookModel>> GetAll(BookFilterModel filter)
        //{
        //    return   _mapper.Map<List<BookModel>>(await _bookRepository.GetAllWithFilter(filter.Isbn,filter.Name,filter.Price,filter.Quantity));   
        //}
        public async Task<List<CustomerModel>> GetAll()
        {
            return _mapper.Map<List<CustomerModel>>(await _customerRepository.GetAll());
        }
        public async Task<CustomerModel> Create(CustomerModel customer)
        {
            var customerEn = _mapper.Map<new_customer>(customer);
            _customerRepository.Create(customerEn);
            var customers = _customerRepository.GetById(customerEn.Id).Result;
            return _mapper.Map<CustomerModel>(customers);
        }
        public async Task<CustomerModel> Update(CustomerModel customer)
        {
            var toUPdate = _customerRepository.GetById(customer.Id).Result;
            toUPdate.new_name = customer.Name;
            toUPdate.new_phone = customer.Phone;
            toUPdate.new_city = customer.City;
            toUPdate.new_nationalId = customer.Nationalid;
           await _customerRepository.Update(toUPdate);
            return _mapper.Map<CustomerModel>(_customerRepository.GetById(toUPdate.Id).Result);
        }

        public bool Delete(Guid customerId)
        {
            var customer = _customerRepository.GetById(customerId).Result;
            if (customer != null)
            {
               
                _customerRepository.Delete(customer);
                return true;
            }
            return false;
        }
    }
}
