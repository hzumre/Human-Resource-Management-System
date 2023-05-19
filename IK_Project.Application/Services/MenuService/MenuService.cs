using AutoMapper;
using IK_Project.Application.Models.DTOs.MenuDTOs;
using IK_Project.Domain.Entities.Concrete;
using IK_Project.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Application.Services.MenuService
{
    public class MenuService : IMenuService
    {
        public MenuService(IMenuRepository menuRepository,IMapper mapper)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        IMenuRepository _menuRepository;
        IMapper _mapper;
        public async Task<List<MenuListDTO>> AllMenus()
        {
            return _mapper.Map<List<MenuListDTO>>(await _menuRepository.GetAll());
        }

        public async Task Create(MenuCreateDTO menuDTO)
        {
            var menu = _mapper.Map<Menu>(menuDTO);

            await _menuRepository.Add(menu);
        }

        public async Task Edit(MenuUpdateDTO menuDTO)
        {
            Menu menu = _mapper.Map<Menu>(menuDTO);
            await _menuRepository.Update(menu);
        }

        public async Task<MenuUpdateDTO> GetById(int id)
        {
            return _mapper.Map<MenuUpdateDTO>(await _menuRepository.GetBy(x => x.Id == id));
        }

        public async Task<List<MenuListDTO>> GetDefaults(Expression<Func<Menu, bool>> expression)
        {
            var result = await _menuRepository.GetDefault(expression);
            var listCategoryResult = _mapper.Map<List<Menu>, List<MenuListDTO>>(result);
            return listCategoryResult;
        }

        public async Task<bool> IsMenuExsist(string menuName)
        {
            return await _menuRepository.Any(x => x.Name.Contains(menuName));
        }

        public async Task Remove(int id)
        {
            Menu menu = await _menuRepository.GetBy(x => x.Id == id);
            await _menuRepository.Delete(menu);
        }
    }
}
