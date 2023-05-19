using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Application.Services
{
    public interface IBaseService<T, T1> where T : class
    {
        Task Create(T itemDTO);
        Task Edit(T itemDTO);
        Task Remove(T itemDTO);

        Task<List<T>> GetDefaults(Expression<Func<T, bool>> expression);
        Task<List<T>> All();
        Task<T> GetById(T1 id);






    }
}
