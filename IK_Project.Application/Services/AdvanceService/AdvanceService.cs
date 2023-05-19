using AutoMapper;
using IK_Project.Application.Models.DTOs.AdvanceDTOs;
using IK_Project.Application.Models.DTOs.ExpenseDTOs;
using IK_Project.Domain.Entities.Concrete;
using IK_Project.Domain.Repositories;
using IK_Project.Infrastructure.RepositoriesConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Application.Services.AdvanceService
{
    public class AdvanceService : IAdvanceService
    {
        IAdvanceRepository _advanceRepository;
        IMapper _mapper;
        public AdvanceService(IAdvanceRepository advanceRepository, IMapper mapper)
        {
            _advanceRepository = advanceRepository;
            _mapper = mapper;
        }

        public async Task<List<AdvanceListDTO>> AllAdvances()
        {
            return _mapper.Map<List<AdvanceListDTO>>(await _advanceRepository.GetAll());
        }

        public async Task Create(AdvanceCreateDTO advanceCreateDTO)
        {
            var advance = _mapper.Map<Advance>(advanceCreateDTO);
            await _advanceRepository.Add(advance);
        }

        public async Task Edit(AdvanceUpdateDTO advanceUpdateDTO)
        {
            var advance = _mapper.Map<Advance>(advanceUpdateDTO);
            await _advanceRepository.Update(advance);
        }

        public async Task<AdvanceUpdateDTO> GetById(int id)
        {
            return _mapper.Map<AdvanceUpdateDTO>(await _advanceRepository.GetBy(x => x.Id == id));
        }

        public async Task<List<AdvanceListDTO>> GetDefaults(Expression<Func<Advance, bool>> expression)
        {
            var result = await _advanceRepository.GetDefault(expression);
            var listAdvanceResult = _mapper.Map<List<Advance>, List<AdvanceListDTO>>(result);
            return listAdvanceResult;
        }

        public async Task Remove(int id)
        {
            Advance advance = await _advanceRepository.GetBy(x => x.Id == id);
              await _advanceRepository.Delete(advance);
        }
        //public async Task<List<AdvanceListDTO>> AllAdvances()
        //{
        //    return _mapper.Map<List<AdvanceListDTO>>(await _advanceRepository.GetAll());
        //}

        //public async Task Create(AdvanceCreateDTO advanceCreateDTO)
        //{
        //    var advance = _mapper.Map<Advance>(advanceCreateDTO);
        //    await _advanceRepository.Add(advance);
        //}

        //public async Task Edit(AdvanceUpdateDTO advanceUpdateDTO)
        //{
        //    var advance = _mapper.Map<Advance>(advanceUpdateDTO);
        //    await _advanceRepository.Update(advance);
        //}

        //public async Task<AdvanceUpdateDTO> GetById(int id)
        //{
        //    return _mapper.Map<AdvanceUpdateDTO>(await _advanceRepository.GetBy(x => x.Id == id));
        //}

        //public async Task<List<AdvanceListDTO>> GetDefaults(Expression<Func<Advance, bool>> expression)
        //{
        //    var result = await _advanceRepository.GetDefault(expression);
        //    var listAdvanceResult = _mapper.Map<List<Advance>, List<AdvanceListDTO>>(result);
        //    return listAdvanceResult;

        //}



        //public async Task Remove(int id)
        //{
        //    Advance advance = await _advanceRepository.GetBy(x => x.Id == id);
        //    await _advanceRepository.Delete(advance);
        //}
    }
}
