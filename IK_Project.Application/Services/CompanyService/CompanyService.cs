using AutoMapper;
using IK_Project.Application.Models.DTOs.CompanyDTOs;
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

namespace IK_Project.Application.Services.CompanyService
{
    public class CompanyService : ICompanyService
    {
        ICompanyRepository _companyRepository;
        IMapper _mapper;
        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            this._companyRepository = companyRepository;
            this._mapper = mapper;

        }
        public async Task<List<CompanyListDTO>> AllCompanies()
        {

            return _mapper.Map<List<CompanyListDTO>>(await _companyRepository.GetAll());
        }

        public async Task Create(CompanyCreateDTO companyCreateDTO)
        {
            var company = _mapper.Map<Company>(companyCreateDTO);
            await _companyRepository.Add(company);
        }

        public async Task Edit(CompanyUpdateDTO companyUpdateDTO)
        {
            var company = _mapper.Map<Company>(companyUpdateDTO);
            await _companyRepository.Update(company);
        }

        public async Task<CompanyUpdateDTO> GetById(int id)
        {
            return _mapper.Map<CompanyUpdateDTO>(await _companyRepository.GetBy(x => x.Id == id));
        }

        public async Task<List<CompanyListDTO>> GetDefaults(Expression<Func<Company, bool>> expression)
        {
            var result = await _companyRepository.GetDefault(expression);
            var listCompanyResult = _mapper.Map<List<CompanyListDTO>>(result);
            return listCompanyResult;
        }

        public async Task<bool> IsCompanyExists(int companyId)
        {
            return await _companyRepository.Any(x => x.Id == companyId);
        }

        public async Task Remove(int id)
        {
            Company company = await _companyRepository.GetBy(x => x.Id == id);
            await _companyRepository.Delete(company);

        }
    }
}
