using AutoMapper;
using IK_Project.Application.Services.CompanyManagerService;
using IK_Project.Application.Services.EmployeeService;
using IK_Project.Application.Services.ExpenseService;
using IK_Project.Application.Services.PermissionService;
using IK_Project.Domain.Entities.Concrete;
using IK_Project.UI.Areas.CompanyManager.Models.Employee;
using IK_Project.UI.Areas.CompanyManager.Models;
using IK_Project.UI.Areas.Employee.Models.ExpenseVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using IK_Project.UI.Areas.Employee.Models.PermissionVMs;
using IK_Project.Application.Models.DTOs.ExpenseDTOs;
using IK_Project.Application.Models.DTOs.PermissionDTOs;

namespace IK_Project.UI.Areas.CompanyManager.Controllers
{
    [Area("CompanyManager")]
    [Authorize(Roles = "CompanyManager")]
    public class PermissionController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IPermissionService _permissionService;
        private readonly IEmployeeService _employeeService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        readonly private ICompanyManagerService _cmService;

        IMapper _mapper;

        public PermissionController(UserManager<AppUser> userManager, IPermissionService permissionService, IEmployeeService employeeService, IMapper mapper, IWebHostEnvironment webHostEnvironment, ICompanyManagerService cmService)
        {
            _userManager = userManager;
            _permissionService = permissionService;
            _employeeService = employeeService;
            _webHostEnvironment = webHostEnvironment;
            _cmService = cmService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CheckPermissionDemands()
        {
            List<CompanyManagerVM> cmVm = _mapper.Map<List<CompanyManagerVM>>(await _cmService.GetDefaults(x => x.AppUser.UserName == HttpContext.User.Identity.Name));

            var cm = cmVm.FirstOrDefault();



            List<PermissionListVM> permissionList = _mapper.Map<List<PermissionListVM>>(await _permissionService.GetDefaults(x => x.Employee.CompanyID == cm.CompanyID));
            List<EmployeeListVM> listEmployee = _mapper.Map<List<EmployeeListVM>>(await _employeeService.GetDefaults(x => x.CompanyID == cm.CompanyID)); ;



            foreach (var permission in permissionList)
            {
                var demandOwner = _mapper.Map<IK_Project.Domain.Entities.Concrete.Employee>(listEmployee.FirstOrDefault(x => x.Id == permission.EmployeeId));
                permission.Employee = demandOwner;
            }


            return View(permissionList);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                PermissionUpdateVM permission = _mapper.Map<PermissionUpdateVM>(await _permissionService.GetById(id));

                return View(permission);

            }
        }

        [HttpPost]
        public async Task<IActionResult> Details(PermissionUpdateVM vm)
        {

            var permissionDto = _mapper.Map<PermissionUpdateDTO>(vm);
            await _permissionService.Edit(permissionDto);
            return RedirectToAction("CheckPermissionDemands");
        }

    }
}
