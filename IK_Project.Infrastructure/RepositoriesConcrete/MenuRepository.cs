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
    public class MenuRepository : BaseRepository<Menu>, IMenuRepository
    {
        public MenuRepository(IKProjectDBContext dBContext) : base(dBContext)
        {
            _dBContext = dBContext;
            _menuTable = _dBContext.Menus;
        }

        IKProjectDBContext _dBContext;

        DbSet<Menu> _menuTable;
        public async Task<List<Menu>> DeactiveMenus()
        {
            return await this.GetDefault(x=>x.Status == Domain.Enums.Status.DeActive);
        }
    }
}
