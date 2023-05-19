using IK_Project.Application.Models.DTOs.AdminDTOs;
using IK_Project.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Application.Services.AdminSevice
{
    public interface IAdminService
    {
        Task Create(AdminCreateDTO adminCreateDTO);
        Task Edit(AdminUpdateDTO adminUpdateDTO);
        Task Remove(Guid id);

        Task<List<AdminListDTO>> GetDefaults(Expression<Func<Admin, bool>> expression);
        Task<List<AdminListDTO>> AllAdmins();

        Task<AdminUpdateDTO> GetById(Guid id);
        Task<bool> IsAdminExists(Guid adminId);
    }
}
