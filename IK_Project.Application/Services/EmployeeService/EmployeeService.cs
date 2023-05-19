using AutoMapper;
using IK_Project.Application.Models.DTOs.CompanyManagerDTOs;
using IK_Project.Application.Models.DTOs.EmployeeDTOs;
using IK_Project.Domain.Entities.Concrete;
using IK_Project.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Application.Services.EmployeeService
{
    public class EmployeeService:IEmployeeService
    {
        IEmployeeRepository _employeeRepository;
        IMapper _mapper;
        public EmployeeService(IEmployeeRepository employeerRepository, IMapper mapper)
        {
            this._employeeRepository = employeerRepository;
            this._mapper = mapper;
        }
        public async Task<List<EmployeeListDTO>> AllManagers()
        {
            return _mapper.Map<List<EmployeeListDTO>>(await _employeeRepository.GetAll());
        }

        public async Task Create(EmployeeCreateDTO employeeCreateDTO)
        {
            var employee = _mapper.Map<Employee>(employeeCreateDTO);
            await _employeeRepository.Add(employee);
        }

        public async Task Edit(EmployeeUpdateDTO employeeUpdateDTO)
        {
            var employee = _mapper.Map<Employee>(employeeUpdateDTO);
            await _employeeRepository.Update(employee);
        }

        public async Task<EmployeeUpdateDTO> GetById(Guid id)
        {
            return _mapper.Map<EmployeeUpdateDTO>(await _employeeRepository.GetBy(x => x.Id == id));
        }

        public async Task<List<EmployeeListDTO>> GetDefaults(Expression<Func<Employee, bool>> expression)
        {
            var result = await _employeeRepository.GetDefault(expression);
            var listEmployeeResult = _mapper.Map<List<EmployeeListDTO>>(result);
            return listEmployeeResult;
        }

        public async Task<bool> IsEmployeeExists(Guid emplooyeeId)
        {
            return await _employeeRepository.Any(x => x.Id == emplooyeeId);
        }

     

        public async Task Remove(Guid id)
        {
            Employee employee = await _employeeRepository.GetBy(x => x.Id == id);
            await _employeeRepository.Delete(employee);
        }
    }
}
