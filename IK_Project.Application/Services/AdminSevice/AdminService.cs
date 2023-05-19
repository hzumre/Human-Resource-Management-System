using AutoMapper;
using IK_Project.Application.Models.DTOs.AdminDTOs;
using IK_Project.Application.Models.DTOs.EmployeeDTOs;
using IK_Project.Domain.Entities.Concrete;
using IK_Project.Domain.Repositories;
using IK_Project.Infrastructure.RepositoriesConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Application.Services.AdminSevice
{
    public class AdminService : IAdminService
    {
        IAdminRepository _adminRepository;
        IMapper _mapper;

        public AdminService(IAdminRepository adminRepository, IMapper mapper)
        {
            this._adminRepository = adminRepository;
            this._mapper = mapper;
        }

        public async Task<List<AdminListDTO>> AllAdmins()
        {
            return _mapper.Map<List<AdminListDTO>>(await _adminRepository.GetAll());
        }

        public async Task Create(AdminCreateDTO adminCreateDTO)
        {
            var admin =_mapper.Map<Admin>(adminCreateDTO);
            await _adminRepository.Add(admin);

        }

        public async Task Edit(AdminUpdateDTO adminUpdateDTO)
        {
            var admin = _mapper.Map<Admin>(adminUpdateDTO);
            await _adminRepository.Update(admin);
        }

        public async  Task<AdminUpdateDTO> GetById(Guid id)
        {
            return _mapper.Map<AdminUpdateDTO>(await _adminRepository.GetBy(x=>x.Id==id));
        }

        public async Task<List<AdminListDTO>> GetDefaults(Expression<Func<Admin, bool>> expression)
        {
            var result = await _adminRepository.GetDefault(expression);
            var listAdminResult = _mapper.Map<List<AdminListDTO>>(result);
            return listAdminResult;
        }

        public async Task<bool> IsAdminExists(Guid adminId)
        {
            return await _adminRepository.Any(x => x.Id == adminId);
        }

        public async Task Remove(Guid id)
        {
            Admin admin = await _adminRepository.GetBy(x => x.Id == id);
            await _adminRepository.Delete(admin);
        }
    }

      

        

        
}
