using AutoMapper;
using IK_Project.Application.Models.DTOs.AdvanceDTOs;
using IK_Project.Application.Services.AdvanceService;
using IK_Project.Application.Services.EmployeeService;
using IK_Project.Domain.Entities.Concrete;
using IK_Project.Domain.Enums;
using IK_Project.UI.Areas.CompanyManager.Models.Employee;
using IK_Project.UI.Areas.Employee.Models.AdvanceVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace IK_Project.UI.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = "Employee")]
    public class AdvanceController:Controller
    {      
        
            private readonly UserManager<AppUser> _userManager;
            private readonly IAdvanceService _advanceService;
            private readonly IEmployeeService _employeeService;
            private readonly IWebHostEnvironment _webHostEnvironment;

            IMapper _mapper;


            public AdvanceController(UserManager<AppUser> userManager, IAdvanceService advanceService, IEmployeeService employeeService, IMapper mapper, IWebHostEnvironment webHostEnvironment)
            {
                _userManager = userManager;
                _advanceService = advanceService;
                _employeeService = employeeService;
                _mapper = mapper;
                _webHostEnvironment = webHostEnvironment;
            }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetMyAllAdvances()
        {
            List<EmployeeListVM> employeeListVM = _mapper.Map<List<EmployeeListVM>>(await _employeeService.GetDefaults(x => x.AppUser.UserName == HttpContext.User.Identity.Name));
            var employee = employeeListVM.FirstOrDefault();

            List<AdvanceListVM> advanceList = _mapper.Map<List<AdvanceListVM>>(await _advanceService.GetDefaults(x => x.EmployeeId == employee.Id));
            
            return View(advanceList);
        }

        public async Task<IActionResult> Add()
        {
            List<EmployeeListVM> employeeListVM = _mapper.Map<List<EmployeeListVM>>(await _employeeService.GetDefaults(x => x.AppUser.UserName == HttpContext.User.Identity.Name));
            var employee =employeeListVM.FirstOrDefault();

            AdvanceCreateVM advanceCreateVM = new()
            {
                EmployeeId = employee.Id,
                AppUserId = employee.Id
            };

            return View(advanceCreateVM);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AdvanceCreateVM vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    List<EmployeeListVM> employeeList = _mapper.Map<List<EmployeeListVM>>(await _employeeService.GetDefaults(x => x.Id == vm.EmployeeId));
                    var employee = employeeList.FirstOrDefault();
                    if (vm.Amount>(employee.Salary*3))
                    {
                        TempData["SalaryError"] = "The amount demanded cannot be more than 3 times your salary.";
                        return View(vm);
                    }
                    else
                    {
                        vm.ConfirmationStatus = ConfirmationStatus.Pending;
                        var dto = _mapper.Map<AdvanceCreateDTO>(vm);
                        await _advanceService.Create(dto);
                        return RedirectToAction("GetMyAllAdvances");
                    }


                }
                catch(Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }

            }
            return View(vm);

        }

    }



}

