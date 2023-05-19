using AutoMapper;
using IK_Project.Application.Models.DTOs.AppRoleDTOs;
using IK_Project.Application.Models.DTOs.MenuDTOs;
using IK_Project.Application.Services.MenuService;
using IK_Project.UI.Areas.Admin.Models.ViewModels.MenuVMs;
using IK_Project.UI.Areas.Admin.Models.ViewModels.RoleVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace IK_Project.UI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class MenuController : Controller
    {

        private readonly IMenuService _menuService;
        private readonly IMapper _mapper;

        public MenuController(IMenuService menuService, IMapper mapper)
        {
            _menuService = menuService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllMenus()
        {
            var listDTO = await _menuService.AllMenus();
            var listVM = _mapper.Map<List<MenuListVM>>(listDTO);

            return View(listVM);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MenuCreateVM vm)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    var dto = _mapper.Map<MenuCreateDTO>(vm);
                    await _menuService.Create(dto);
                    return RedirectToAction("GetAllMenus");
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                }

            }
            return View(vm);
        }


        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _menuService.Remove(id);
                return RedirectToAction("GetAllMenus");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("GetAllMenus");

            }

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var updateDto = await _menuService.GetById(id);
            var updateVm = _mapper.Map<MenuUpdateVM>(updateDto);

            return View(updateVm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MenuUpdateVM vm)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var updateDto = _mapper.Map<MenuUpdateDTO>(vm);
                    await _menuService.Edit(updateDto);
                    return RedirectToAction("GetAllMenus");
                }
                catch (Exception ex)
                {

                    TempData["error"] = ex.Message;
                }
            }

            return View(vm);
        }


    }
}
