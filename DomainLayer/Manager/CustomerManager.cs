using AutoMapper;
using Data.Entity;
using Data.Repository;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Manager
{
    public interface ICustomerManager
    {
        bool Create(CustomerModel customer);
        List<CustomerModel> GetAll();
        CustomerModel GetById(Guid customerId);
        bool Update(CustomerModel customer);
        bool Delete(Guid customerId);
    }
    public class CustomerManager : ICustomerManager
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        public CustomerManager(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }
        public List<CustomerModel> GetAll()
        {
            return _mapper.Map<List<CustomerModel>>(_customerRepository.GetAll());
        }

        public CustomerModel GetById(Guid customerId)
        {
            return _mapper.Map<CustomerModel>(_customerRepository.GetById(customerId));
        }

        public bool Create(CustomerModel customer)
        {
            var customerEn = _mapper.Map<CustomerEntity>(customer);
            _customerRepository.Create(customerEn);
            return true;
        }
        public bool Update(CustomerModel customer)
        {
            var customerEn = _mapper.Map<CustomerEntity>(customer);
            _customerRepository.Update(customerEn);
            return true;
        }

        public bool Delete(Guid customerId)
        {
            var customer = _customerRepository.GetById(customerId);
            if (customer != null)
            {
                _customerRepository.Delete(customer);
                return true;
            }
            return false;
        }
    }
}
