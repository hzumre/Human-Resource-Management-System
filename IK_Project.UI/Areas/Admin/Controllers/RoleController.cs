using AutoMapper;
using IK_Project.Application.Models.DTOs.AppRoleDTOs;
using IK_Project.Application.Services.AppRoleService;
using IK_Project.Application.Services.AppUserService;
using IK_Project.Domain.Entities.Concrete;
using IK_Project.UI.Areas.Admin.Models.ViewModels.RoleVMs;
using IK_Project.UI.Areas.Admin.Models.ViewModels.UserVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace IK_Project.UI.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class RoleController : Controller
    {

		private readonly IAppRoleService _appRoleService;
		private readonly IAppUserService _appUserService;
		private readonly IMapper _mapper;
		public RoleController(IAppRoleService appRoleService, IMapper mapper, IAppUserService appUserService)
		{
			_appRoleService = appRoleService;
			_mapper = mapper;
			_appUserService = appUserService;
		}
		public IActionResult Index()
		{
			return View();
		}
		public async Task<IActionResult> GetAllRoles()
		{
			List<AppRoleListDTO> list1 = await _appRoleService.AllRoles();
			List<RoleListVM> list2 = _mapper.Map<List<RoleListVM>>(list1);


			return View(list2);
		}

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            RoleCreateVM vm = new RoleCreateVM();
            Guid id = Guid.NewGuid();
            vm.Id = id;
            return View(vm);
        }  
        [HttpPost]
        public async Task<IActionResult> Create(RoleCreateVM vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dto = _mapper.Map<AppRoleCreateDTO>(vm);
                    await _appRoleService.Create(dto);
                    return RedirectToAction("GetAllRoles");

                }
                catch (Exception ex)
                {

                    TempData["error"] = ex.Message;
                }
            }

            return View(vm);
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _appRoleService.Remove(id);
                return RedirectToAction("GetAllRoles");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("GetAllRoles");

            }

        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var updateDto = await _appRoleService.GetById(id);
            var updateVm = _mapper.Map<RoleEditVM>(updateDto);

            return View(updateVm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleEditVM vm)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var updateDto = _mapper.Map<AppRoleUpdateDTO>(vm);
                    await _appRoleService.Edit(updateDto);
                    return RedirectToAction("GetAllRoles");
                }
                catch (Exception ex)
                {

                    TempData["error"] = ex.Message;
                }
            }

            return View(vm);
        }


        public async Task<IActionResult> ListUsers()
        {

            List<UserListsVM> list = _mapper.Map<List<UserListsVM>>(await _appUserService.GetUsers());
            return View(list);
        }


        public async Task<IActionResult> RoleAssign(Guid id)
        {
            AppUser user = await _appUserService.GetById(id);
            List<RoleListVM> allRoles = _mapper.Map<List<RoleListVM>>(await _appRoleService.AllRoles());

            List<string> userRoles = await _appUserService.GetUserAssignedRoles(user) as List<string>;

            List<UserRoleAssignVM> assignRoles = new List<UserRoleAssignVM>();

            allRoles.ForEach(role => assignRoles.Add(new UserRoleAssignVM

            {
                HasAssign = userRoles.Contains(role.Name),
                Id = role.Id,
                Name = role.Name
            }));

            return View(assignRoles);
        }
        [HttpPost]
        public async Task<ActionResult> RoleAssign(List<UserRoleAssignVM> modelList, Guid id)
        {
            AppUser user = await _appUserService.GetById(id);
            foreach (UserRoleAssignVM role in modelList)
            {
                if (role.HasAssign)
                    await _appUserService.AddRole(user, role.Name);
                else
                    await _appUserService.RemoveRole(user, role.Name);
            }
            return RedirectToAction("ListUsers", "Role");
        }

    }
}
