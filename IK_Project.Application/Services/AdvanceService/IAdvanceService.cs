using IK_Project.Application.Models.DTOs.AdvanceDTOs;
using IK_Project.Application.Models.DTOs.ExpenseDTOs;
using IK_Project.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Application.Services.AdvanceService
{
    public interface IAdvanceService
    {
        Task Create(AdvanceCreateDTO advanceCreateDTO);
        Task Edit(AdvanceUpdateDTO advanceUpdateDTO);
        Task Remove(int id);

        Task<List<AdvanceListDTO>> GetDefaults(Expression<Func<Advance, bool>> expression);
        Task<List<AdvanceListDTO>> AllAdvances();
        Task<AdvanceUpdateDTO> GetById(int id);
    }
}
