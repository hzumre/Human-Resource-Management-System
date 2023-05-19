using AutoMapper;
using IK_Project.Application.Models.DTOs.ExpenseDTOs;
using IK_Project.Application.Models.DTOs.PermissionDTOs;
using IK_Project.Application.Services.EmployeeService;
using IK_Project.Application.Services.ExpenseService;
using IK_Project.Application.Services.PermissionService;
using IK_Project.Domain.Entities.Concrete;
using IK_Project.Domain.Enums;
using IK_Project.UI.Areas.CompanyManager.Models.Employee;
using IK_Project.UI.Areas.Employee.Models.ExpenseVMs;
using IK_Project.UI.Areas.Employee.Models.PermissionVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace IK_Project.UI.Areas.Employee.Controllers
{  
        [Area("Employee")]
        [Authorize(Roles = "Employee")]
        public class PermissionController : Controller
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly IPermissionService _permissionService;
            private readonly IEmployeeService _employeeService;
            private readonly IWebHostEnvironment _webHostEnvironment;

            IMapper _mapper;


            public PermissionController(UserManager<AppUser> userManager, IPermissionService permissionService, IEmployeeService employeeService, IMapper mapper, IWebHostEnvironment webHostEnvironment)
            {
                _userManager = userManager;
               _permissionService = permissionService;
                _employeeService = employeeService;
                _mapper = mapper;
                _webHostEnvironment = webHostEnvironment;
            }

            public async Task<IActionResult> GetMyAllPermission()
            {
                List<EmployeeListVM> employeeListVM = _mapper.Map<List<EmployeeListVM>>(await _employeeService.GetDefaults(x => x.AppUser.UserName == HttpContext.User.Identity.Name));

                var employee = employeeListVM.FirstOrDefault();


                List<PermissionListVM> permissionList = _mapper.Map<List<PermissionListVM>>(await _permissionService.GetDefaults(x => x.EmployeeId == employee.Id));


                return View(permissionList);
            }


            public async Task<IActionResult> Add()
            {


                List<EmployeeListVM> employeeListVM = _mapper.Map<List<EmployeeListVM>>(await _employeeService.GetDefaults(x => x.AppUser.UserName == HttpContext.User.Identity.Name));

                var employee = employeeListVM.FirstOrDefault();

                PermissionCreateVM permissionCreateVM = new()
                {
                    EmployeeId = employee.Id,
                    AppUserId = employee.AppUserID

                };
                return View(permissionCreateVM);
            }


            [HttpPost]
            public async Task<IActionResult> Add(PermissionCreateVM vm)
            {
                if (ModelState.IsValid)
                {
                    if (Request.Form.Files.Count > 0)
                    {


                        string wwwrootDosyaYolu = _webHostEnvironment.WebRootPath;
                        string dosyaAdi = Path.GetFileNameWithoutExtension(Request.Form.Files[0].FileName);
                        string dosyaUzantisi = Path.GetExtension(Request.Form.Files[0].FileName);
                        string tamDosyaAdi = $"{dosyaAdi}_{Guid.NewGuid()}{dosyaUzantisi}";
                        string yeniDosyaYolu = Path.Combine($"{wwwrootDosyaYolu}/permissionFiles/{tamDosyaAdi}");

                        using (var fileStream = new FileStream(yeniDosyaYolu, FileMode.Create))
                        {
                            Request.Form.Files[0].CopyTo(fileStream);

                        }

                        vm.PermissionFilePath = tamDosyaAdi;
                    }

                    else
                    {
                        vm.PermissionFilePath = vm.PermissionFilePath;
                    }

                    try
                    {
                        vm.ConfirmationStatus = ConfirmationStatus.Pending;
                        var dto = _mapper.Map<PermissionCreateDTO>(vm);
                        await _permissionService.Create(dto);
                        return RedirectToAction("GetMyAllPermission");
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
