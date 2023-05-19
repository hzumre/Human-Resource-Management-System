using IK_Project.Application.Models.DTOs.MenuDTOs;
using IK_Project.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Application.Services.MenuService
{
    public interface IMenuService
    {

        Task Create(MenuCreateDTO menuDTO);
        Task Edit(MenuUpdateDTO menuDTO);
        Task Remove(int id);

        Task<List<MenuListDTO>> GetDefaults(Expression<Func<Menu, bool>> expression);
        Task<List<MenuListDTO>> AllMenus();
        Task<MenuUpdateDTO> GetById(int id);
        Task<bool> IsMenuExsist(string menuName);
    }
}
