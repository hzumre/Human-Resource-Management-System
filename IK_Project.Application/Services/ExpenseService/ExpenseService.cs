using AutoMapper;
using IK_Project.Application.Models.DTOs.ExpenseDTOs;
using IK_Project.Application.Models.DTOs.MenuDTOs;
using IK_Project.Domain.Entities.Concrete;
using IK_Project.Domain.Repositories;
using IK_Project.Infrastructure.RepositoriesConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Application.Services.ExpenseService
{
    public class ExpenseService : IExpenseService
    {
        IExpenseRepository _expenseRepository;
        IMapper _mapper;
        public ExpenseService(IExpenseRepository expenseRepository, IMapper mapper)
        {
            _expenseRepository = expenseRepository;
            _mapper = mapper;
        }
        public async Task<List<ExpenseListDTO>> AllExpenses()
        {
            return _mapper.Map<List<ExpenseListDTO>>(await _expenseRepository.GetAll());
        }

        public async Task Create(ExpenseCreateDTO expenseCreateDTO)
        {
            var expense = _mapper.Map<Expense>(expenseCreateDTO);

            await _expenseRepository.Add(expense);
        }

        public async Task Edit(ExpenseUpdateDTO expenseUpdateDTO)
        {
            Expense expense = _mapper.Map<Expense>(expenseUpdateDTO);
            await _expenseRepository.Update(expense);
        }

        public async Task<ExpenseUpdateDTO> GetById(int id)
        {
            return _mapper.Map<ExpenseUpdateDTO>(await _expenseRepository.GetBy(x => x.Id == id));
        }

        public async Task<List<ExpenseListDTO>> GetDefaults(Expression<Func<Expense, bool>> expression)
        {
            var result = await _expenseRepository.GetDefault(expression);
            var listExpenseResult = _mapper.Map<List<Expense>, List<ExpenseListDTO>>(result);
            return listExpenseResult;
        }

        public async Task Remove(int id)
        {
            Expense expense = await _expenseRepository.GetBy(x => x.Id == id);
            await _expenseRepository.Delete(expense);
        }
    }
}
