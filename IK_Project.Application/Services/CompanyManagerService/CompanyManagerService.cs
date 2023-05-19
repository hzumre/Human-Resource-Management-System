using AutoMapper;
using IK_Project.Application.Models.DTOs.CompanyManagerDTOs;
using IK_Project.Domain.Entities.Concrete;
using IK_Project.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Application.Services.CompanyManagerService
{
    public class CompanyManagerService : ICompanyManagerService
    {
        ICompanyManagerRepository _companyManagerRepository;
        IMapper _mapper;
        public CompanyManagerService(ICompanyManagerRepository companyManagerRepository, IMapper mapper)
        {
            this._companyManagerRepository = companyManagerRepository;
            this._mapper = mapper;
        }
        public async Task<List<CompanyManagerListDTO>> AllManagers()
        {
            return _mapper.Map<List<CompanyManagerListDTO>>(await _companyManagerRepository.GetAll());
        }

        public async Task Create(CompanyManagerCreateDTO companyManagerCreateDTO)
        {
            var manager = _mapper.Map<CompanyManager>(companyManagerCreateDTO);
            await _companyManagerRepository.Add(manager);
        }

        public async Task Edit(CompanyManagerUpdateDTO companyManagerUpdateDTO)
        {
            var manager = _mapper.Map<CompanyManager>(companyManagerUpdateDTO);
            await _companyManagerRepository.Update(manager);
        }

        public async Task<CompanyManagerUpdateDTO> GetById(Guid id)
        {
            return _mapper.Map<CompanyManagerUpdateDTO>(await _companyManagerRepository.GetBy(x => x.Id == id));
        }

        public async Task<List<CompanyManagerListDTO>> GetDefaults(Expression<Func<CompanyManager, bool>> expression)
        {
            var result = await _companyManagerRepository.GetDefault(expression);
            var listManagerResult = _mapper.Map<List<CompanyManagerListDTO>>(result);
            return listManagerResult;
        }

        public async Task<bool> IsManagerExists(Guid managerId)
        {
            return await _companyManagerRepository.Any(x => x.Id==managerId);
        }

        public async Task Remove(Guid id)
        {
            CompanyManager manager = await _companyManagerRepository.GetBy(x => x.Id == id);
            await _companyManagerRepository.Delete(manager);
        }
    }
}
