using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
       public interface IRentalRepository
    {
        void Create(RentalEntity rental);
        List<RentalEntity> GetAll();
        RentalEntity GetById(Guid rentalId);
        void Update(RentalEntity rental);
        void Delete(RentalEntity rental);
    }
    public class RentalRepository : IRentalRepository
    {
        private readonly LibraryContext _library;
        public RentalRepository(LibraryContext library)
        {
            _library = library;
        }
        public List<RentalEntity> GetAll()
        {
            return _library.Rental.ToList();
        }
        public RentalEntity GetById(Guid rentalId)
        {
            return _library.Rental.FirstOrDefault(rental => rental.Id == rentalId);
        }
        public void Create(RentalEntity rental)
        {
            _library.Rental.Add(rental);
            _library.SaveChanges();
        }
        public void Update(RentalEntity rental)
        {
            _library.Rental.Update(rental);
            _library.SaveChanges();
        }

        public void Delete(RentalEntity rental)
        {
            _library.Rental.Remove(rental);
            _library.SaveChanges();
        }
    }
}
