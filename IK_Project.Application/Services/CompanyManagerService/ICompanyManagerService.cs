using IK_Project.Application.Models.DTOs.CompanyManagerDTOs;
using IK_Project.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Application.Services.CompanyManagerService
{
    public  interface ICompanyManagerService
    {
        Task Create(CompanyManagerCreateDTO companyManagerCreateDTO);
        Task Edit(CompanyManagerUpdateDTO companyManagerUpdateDTO);
        Task Remove(Guid id);

        Task<List<CompanyManagerListDTO>> GetDefaults(Expression<Func<CompanyManager, bool>> expression);
        Task<List<CompanyManagerListDTO>> AllManagers();
 
        Task<CompanyManagerUpdateDTO> GetById(Guid id);
        Task<bool> IsManagerExists(Guid managerId);

    }
}
