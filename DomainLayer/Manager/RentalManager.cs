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
    public interface IRentalManager
    {
        bool Create(RentalModel rental);
        List<RentalModel> GetAll();
        RentalModel GetById(Guid rentalId);
        bool Update(RentalModel rental);
        bool Delete(Guid rentalId);
    }
    public class RentalManager : IRentalManager
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IMapper _mapper;
        public RentalManager(IRentalRepository rentalRepository, IMapper mapper)
        {
            _rentalRepository = rentalRepository;
            _mapper = mapper;
        }
        public List<RentalModel> GetAll()
        {
            return _mapper.Map<List<RentalModel>>(_rentalRepository.GetAll());
        }
        public RentalModel GetById(Guid rentalId)
        {
            return _mapper.Map<RentalModel>(_rentalRepository.GetById(rentalId));
        }
        public bool Create(RentalModel rental)
        {
            var rentalEn = _mapper.Map<RentalEntity>(rental);
            _rentalRepository.Create(rentalEn);
            return true;
        }
        public bool Update(RentalModel rental)
        {
            var rentalEn = _mapper.Map<RentalEntity>(rental);
            _rentalRepository.Update(rentalEn);
            return true;
        }

        public bool Delete(Guid rentalId)
        {
            var rental = _rentalRepository.GetById(rentalId);
            if (rental != null)
            {
                _rentalRepository.Delete(rental);
                return true;
            }
            return false;
        }
    }
}
