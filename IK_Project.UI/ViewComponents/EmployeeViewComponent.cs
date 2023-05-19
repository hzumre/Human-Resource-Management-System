using AutoMapper;
using IK_Project.Application.Services.CompanyManagerService;
using IK_Project.Application.Services.EmployeeService;
using IK_Project.Domain.Entities.Concrete;
using IK_Project.UI.Areas.Admin.Models.ViewModels.CompanyManagerVMs;
using IK_Project.UI.Areas.CompanyManager.Models.Employee;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IK_Project.UI.ViewComponents
{
    public class EmployeeViewComponent : ViewComponent
    {
        IEmployeeService _employeeService;
        IMapper _mapper;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        public EmployeeViewComponent(IEmployeeService employeeService,IMapper mapper, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _employeeService = employeeService;
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

                List<EmployeeListVM> employeeVm = _mapper.Map<List<EmployeeListVM>>(await _employeeService.GetDefaults(x => x.AppUser.UserName == HttpContext.User.Identity.Name));

                var employee = employeeVm.FirstOrDefault();


                return View(employee);
            }
            else
            {
                return View();


            }

        }
    }
}
