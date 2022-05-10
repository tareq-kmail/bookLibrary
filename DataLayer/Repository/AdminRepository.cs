using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IAdminRepository
    {
        void Create(AdminEntity admin);
        AdminEntity GetAdminByUSerName(string userName);
        List<AdminEntity> GetAll();
        AdminEntity GetById(Guid adminId);
        void Update(AdminEntity admin);
        void Delete(AdminEntity admin);
    }
   public class AdminRepository : IAdminRepository
    {
        private readonly LibraryContext _library;
        public AdminRepository(LibraryContext library)
        {
            _library = library;
        }
        public AdminEntity GetAdminByUSerName(string userName)
        {
            return _library.Admins.FirstOrDefault(e => e.Username.Equals(userName));
        }
        public List<AdminEntity> GetAll()
        {
            return _library.Admins.ToList();
        }
        public AdminEntity GetById(Guid adminId)
        {
            return _library.Admins.FirstOrDefault(admin => admin.Id == adminId);
        }
        public void Create(AdminEntity admin)
        {
            _library.Admins.Add(admin);
            _library.SaveChanges();
        }
        public void Update(AdminEntity admin)
        {
            _library.Admins.Update(admin);
            _library.SaveChanges();
        }

        public void Delete(AdminEntity admin)
        {
            _library.Admins.Remove(admin);
            _library.SaveChanges();
        }

    }
}
