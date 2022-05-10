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
    public interface ISalesManager
    {
        bool Create(SalesModel sales);
        List<SalesModel> GetAll();
        SalesModel GetById(Guid salesId);
        bool Update(SalesModel sales);
        bool Delete(Guid salesId);
    }
    public class SalesManager : ISalesManager
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IMapper _mapper;
        public SalesManager(ISalesRepository salesRepository, IMapper mapper)
        {
            _salesRepository = salesRepository;
            _mapper = mapper;
        }
        public List<SalesModel> GetAll()
        {
            return _mapper.Map<List<SalesModel>>(_salesRepository.GetAll());
        }
        public SalesModel GetById(Guid salesId)
        {
            return _mapper.Map<SalesModel>(_salesRepository.GetById(salesId));
        }
        public bool Create(SalesModel sales)
        {
            var salesEn = _mapper.Map<SalesEntity>(sales);
            _salesRepository.Create(salesEn);
            return true;
        }
        public bool Update(SalesModel sales)
        {
            var salesEn = _mapper.Map<SalesEntity>(sales);
            _salesRepository.Update(salesEn);
            return true;
        }

        public bool Delete(Guid salesId)
        {
            var sales = _salesRepository.GetById(salesId);
            if (sales != null)
            {
                _salesRepository.Delete(sales);
                return true;
            }
            return false;
        }
    }
}
