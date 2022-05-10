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
    public interface IAdminManager
    {
        bool Create(AdminModel admin);
        List<AdminModel> GetAll();
        bool Update(AdminModel admin);
        bool Delete(Guid adminId);
        AdminModel login(LoginModel loginModel);
        AdminModel GetById(Guid adminId);
    }
    public class AdminManager : IAdminManager
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;
        private readonly ServiceClient _serviceClient;
        public AdminManager(IAdminRepository adminRepository , IMapper mapper, ServiceClient serviceClient)
        {
             _adminRepository = adminRepository;
            _mapper = mapper;
            _serviceClient = serviceClient;
        }

        public AdminModel login(LoginModel loginModel)
        {
            var admin = _adminRepository.GetAdminByUSerName(loginModel.UserName);
            if (admin != null &&  admin.new_password.Equals(loginModel.Password))
            {
               var adminToReturn =  _mapper.Map<AdminModel>(admin);
                adminToReturn.Password = "";
                return adminToReturn;
            }
            return null;
        }
        //Comment
        public List<AdminModel> GetAll()
        {
            return _mapper.Map<List<AdminModel>>(_adminRepository.GetAll());
        }
        public AdminModel GetById(Guid adminId)
        {
            return _mapper.Map<AdminModel>(_adminRepository.GetById(adminId));
        }

        public bool Create(AdminModel admin)
        {
            var adminEn = _mapper.Map<new_admin>(admin);
            _adminRepository.Create(adminEn);
            return true;
        }
        public bool Update(AdminModel admin)
        {
            var adminEn = _mapper.Map<new_admin>(admin);
            _adminRepository.Update(adminEn);
            return true;
        }

        public bool Delete(Guid adminId)
        {
            var admin = _adminRepository.GetById(adminId);
            if (admin != null)
            {
                _adminRepository.Delete(admin);
                return true;
            }
            return false;
        }
    }
}
