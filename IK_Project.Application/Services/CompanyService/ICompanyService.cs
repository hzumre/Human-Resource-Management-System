using IK_Project.Application.Models.DTOs.CompanyDTOs;
using IK_Project.Domain.Entities.Concrete;
using System.Linq.Expressions;

namespace IK_Project.Application.Services.CompanyService
{
    public interface ICompanyService
    {
        Task Create(CompanyCreateDTO companyCreateDTO);
        Task Edit(CompanyUpdateDTO companyUpdateDTO);
        Task Remove(int id);

        Task<List<CompanyListDTO>> GetDefaults(Expression<Func<Company, bool>> expression);
        Task<List<CompanyListDTO>> AllCompanies();

        Task<CompanyUpdateDTO> GetById(int id);
        Task<bool> IsCompanyExists(int companyId);
    }
}
