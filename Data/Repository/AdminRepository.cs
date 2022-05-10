using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.PowerPlatform.Dataverse.Client;
using CrmEarlyBound;
namespace Data.Repository
{
    public interface IAdminRepository : IDisposable
    {
        void Create(new_admin admin);
        new_admin GetAdminByUSerName(string userName);
        List<new_admin> GetAll();
        new_admin GetById(Guid adminId);
        void Update(new_admin admin);
        void Delete(new_admin admin);
    }
   public class AdminRepository : IAdminRepository
    {
        
        private readonly ServiceClient _serviceClient;
        public AdminRepository(ServiceClient serviceClient)
        {
            
            _serviceClient = serviceClient;
        }

        public void Dispose()
        {
            using (var context = new CrmServiceContext(_serviceClient))
            {
                context.Dispose();
            }


        }

        public new_admin GetAdminByUSerName(string userName)
        {
            using(var context = new CrmServiceContext(_serviceClient))
            {
                return context.new_adminSet.FirstOrDefault(e => e.new_username == userName );
            }
             
        }
        public List<new_admin> GetAll()
        {
            using (var context = new CrmServiceContext(_serviceClient))
            {
                return context.new_adminSet.ToList();
            }
        }
        public new_admin GetById(Guid adminId)
        {
            using (var context = new CrmServiceContext(_serviceClient))
            {
                return context.new_adminSet.FirstOrDefault(admin => admin.new_adminId == adminId);
            }
        }
        public void Create(new_admin admin)
        {
            using (var context = new CrmServiceContext(_serviceClient))
            {

               
               // context.new_adminSet.Append(admin);
                context.AddObject(admin);
                context.SaveChanges();
            }
        }
        public void Update(new_admin admin)
        {
            using (var context = new CrmServiceContext(_serviceClient))
            {
                context.UpdateObject(admin);
                context.SaveChanges();
            }

        }

        public void Delete(new_admin admin)
        {
            using (var context = new CrmServiceContext(_serviceClient))
            {

                context.DeleteObject(admin);
                context.SaveChanges();
            }
        }


    }
}
