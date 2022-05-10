using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface ISalesRepository
    {
        void Create(SalesEntity sales);
        List<SalesEntity> GetAll();
        SalesEntity GetById(Guid salesId);
        void Update(SalesEntity sales);
        void Delete(SalesEntity sales);
    }
   public class SalesRepository : ISalesRepository
    {
        private readonly LibraryContext _library;
        public SalesRepository(LibraryContext library)
        {
            _library = library;
        }
        public List<SalesEntity> GetAll()
        {
            return _library.Sales.ToList();
        }
        public SalesEntity GetById(Guid salesId)
        {
            return _library.Sales.FirstOrDefault(sales => sales.Id == salesId);
        }
        public void Create(SalesEntity sales)
        {
            _library.Sales.Add(sales);
            _library.SaveChanges();
        }
        public void Update(SalesEntity sales)
        {
            _library.Sales.Update(sales);
            _library.SaveChanges();
        }

        public void Delete(SalesEntity sales)
        {
            _library.Sales.Remove(sales);
            _library.SaveChanges();
        }
    }
}
