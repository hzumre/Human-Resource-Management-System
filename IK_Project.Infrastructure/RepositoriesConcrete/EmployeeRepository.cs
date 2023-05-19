using IK_Project.Domain.Entities.Concrete;
using IK_Project.Domain.Repositories;
using IK_Project.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Infrastructure.RepositoriesConcrete
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {

        private readonly IKProjectDBContext _dbContext;
        protected DbSet<Employee> _employeeTable;

        public EmployeeRepository(IKProjectDBContext DbContext) : base(DbContext)
        {
            this._dbContext = DbContext;
            this._employeeTable = _dbContext.Employees;

        }
    }
}
