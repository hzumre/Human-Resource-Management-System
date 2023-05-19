using IK_Project.Application.Models.DTOs.ExpenseDTOs;
using IK_Project.Application.Models.DTOs.PermissionDTOs;
using IK_Project.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Application.Services.PermissionService
{
    public interface IPermissionService
    {
        Task Create(PermissionCreateDTO permissionCreateDTO);
        Task Edit(PermissionUpdateDTO permissionUpdateDTO);
        Task Remove(int id);

        Task<List<PermissionListDTO>> GetDefaults(Expression<Func<Permission, bool>> expression);
        Task<List<PermissionListDTO>> AllPermissions();
        Task<PermissionUpdateDTO> GetById(int id);
    }
}
