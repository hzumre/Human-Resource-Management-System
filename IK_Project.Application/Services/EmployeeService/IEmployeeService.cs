using IK_Project.Application.Models.DTOs.CompanyManagerDTOs;
using IK_Project.Application.Models.DTOs.EmployeeDTOs;
using IK_Project.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Application.Services.EmployeeService
{
    public interface IEmployeeService
    {
        Task Create(EmployeeCreateDTO companyManagerCreateDTO);
        Task Edit(EmployeeUpdateDTO companyManagerUpdateDTO);
        Task Remove(Guid id);

        Task<List<EmployeeListDTO>> GetDefaults(Expression<Func<Employee, bool>> expression);
        Task<List<EmployeeListDTO>> AllManagers();

        Task<EmployeeUpdateDTO> GetById(Guid id);
        Task<bool> IsEmployeeExists(Guid employeeId);
    }
}
