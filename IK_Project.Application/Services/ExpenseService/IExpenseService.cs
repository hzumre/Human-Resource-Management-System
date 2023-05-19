using IK_Project.Application.Models.DTOs.ExpenseDTOs;
using IK_Project.Application.Models.DTOs.MenuDTOs;
using IK_Project.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Application.Services.ExpenseService
{
    public interface IExpenseService
    {
        Task Create(ExpenseCreateDTO expenseCreateDTO);
        Task Edit(ExpenseUpdateDTO expenseUpdateDTO);
        Task Remove(int id);

        Task<List<ExpenseListDTO>> GetDefaults(Expression<Func<Expense, bool>> expression);
        Task<List<ExpenseListDTO>> AllExpenses();
        Task<ExpenseUpdateDTO> GetById(int id);
    }
}
