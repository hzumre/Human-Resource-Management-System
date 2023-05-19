using AutoMapper;
using IK_Project.Application.Services.AdminSevice;
using IK_Project.Domain.Entities.Concrete;
using IK_Project.UI.Areas.Admin.Models.ViewModels.AdminVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IK_Project.UI.ViewComponents
{
	public class AdminViewComponent : ViewComponent
	{
		IAdminService _adminService;
		IMapper _mapper;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly UserManager<AppUser> _userManager;




		public AdminViewComponent(IAdminService adminService, IMapper mapper, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
		{
			_adminService = adminService;
			_mapper = mapper;
			_signInManager = signInManager;
			_userManager = userManager;

		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			//List<AdminListVM> adminVm = _mapper.Map<List<AdminListVM>>(await _adminService.GetDefaults(x => x.AppUser.UserName == userName));



			//var admin = adminVm.FirstOrDefault();
			////eklenecek unutma!!

			if (_signInManager.IsSignedIn(HttpContext.User))
			{

				List<AdminListVM> adminVm = _mapper.Map<List<AdminListVM>>(await _adminService.GetDefaults(x => x.AppUser.UserName == HttpContext.User.Identity.Name));

				var admin = adminVm.FirstOrDefault();


				return View(admin);
			}
			else
			{
				return View();






			}







		}
	}
}
