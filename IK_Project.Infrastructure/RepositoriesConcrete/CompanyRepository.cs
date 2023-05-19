using IK_Project.Domain.Entities.Concrete;
using IK_Project.Domain.Repositories;
using IK_Project.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Infrastructure.RepositoriesConcrete
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {

        private readonly IKProjectDBContext _dbContext;
        protected DbSet<Company> _companyTable;

        public CompanyRepository(IKProjectDBContext DbContext) : base(DbContext)
        {
            this._dbContext = DbContext;
            this._companyTable = _dbContext.Companies;
        }


    }
}
