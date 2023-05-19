using AutoMapper;
using IK_Project.Application.Models.DTOs.CompanyManagerDTOs;
using IK_Project.Application.Models.DTOs.ExpenseDTOs;
using IK_Project.Application.Services.EmployeeService;
using IK_Project.Application.Services.ExpenseService;
using IK_Project.Domain.Entities.Concrete;
using IK_Project.Domain.Enums;
using IK_Project.UI.Areas.Admin.Models.ViewModels.AdminVM;
using IK_Project.UI.Areas.Admin.Models.ViewModels.CompanyVM;
using IK_Project.UI.Areas.CompanyManager.Models.Employee;
using IK_Project.UI.Areas.Employee.Models.ExpenseVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IK_Project.UI.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = "Employee")]
    public class ExpenseController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IExpenseService _expenseService;
        private readonly IEmployeeService _employeeService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        IMapper _mapper;


        public ExpenseController(UserManager<AppUser> userManager, IExpenseService expenseService, IEmployeeService employeeService, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _expenseService = expenseService;
            _employeeService=employeeService;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> GetMyAllExpenses()
        {
            List<EmployeeListVM> employeeListVM = _mapper.Map<List<EmployeeListVM>>(await _employeeService.GetDefaults(x => x.AppUser.UserName == HttpContext.User.Identity.Name));

            var employee = employeeListVM.FirstOrDefault();


            List<ExpenseListVM> expenseList = _mapper.Map<List<ExpenseListVM>>(await _expenseService.GetDefaults(x=>x.EmployeeId==employee.Id));


            return View(expenseList);
        }


        public async Task<IActionResult> Add()
        {


            List<EmployeeListVM> employeeListVM = _mapper.Map<List<EmployeeListVM>>(await _employeeService.GetDefaults(x => x.AppUser.UserName == HttpContext.User.Identity.Name));

            var employee = employeeListVM.FirstOrDefault();

            ExpenseCreateVM expenseCreateVM = new()
            {
                EmployeeId = employee.Id,
                AppUserId = employee.AppUserID

            };
            return View(expenseCreateVM);
        }


        [HttpPost]
        public async Task<IActionResult> Add(ExpenseCreateVM vm)
        {
            if (ModelState.IsValid)
            {
                if (Request.Form.Files.Count > 0)
                {


                    string wwwrootDosyaYolu = _webHostEnvironment.WebRootPath;
                    string dosyaAdi = Path.GetFileNameWithoutExtension(Request.Form.Files[0].FileName);
                    string dosyaUzantisi = Path.GetExtension(Request.Form.Files[0].FileName);
                    string tamDosyaAdi = $"{dosyaAdi}_{Guid.NewGuid()}{dosyaUzantisi}";
                    string yeniDosyaYolu = Path.Combine($"{wwwrootDosyaYolu}/expenseFiles/{tamDosyaAdi}");

                    using (var fileStream = new FileStream(yeniDosyaYolu, FileMode.Create))
                    {
                        Request.Form.Files[0].CopyTo(fileStream);

                    }

                    vm.ExpenseFilePath = tamDosyaAdi;
                }

                else
                {
                    vm.ExpenseFilePath = vm.ExpenseFilePath;
                }

                try
                {
                    List<EmployeeListVM> employeeList = _mapper.Map<List<EmployeeListVM>>(await _employeeService.GetDefaults(x => x.Id == vm.EmployeeId));
                    var employee = employeeList.FirstOrDefault();

                    List<ExpenseListVM> expenses = _mapper.Map<List<ExpenseListVM>>(await _expenseService.GetDefaults(x => x.EmployeeId == vm.EmployeeId));


                    var toplamExpenseConfirm = expenses.Where(x => x.ModifiedDate <= DateTime.Now && x.ModifiedDate >= DateTime.Now.AddYears(-1)  && (x.ConfirmationStatus == ConfirmationStatus.Confirm)).Sum(x => x.Amount);

                    var toplamExpensePending= expenses.Where(x => x.CreatedDate <= DateTime.Now && x.CreatedDate >= DateTime.Now.AddYears(-1) && (x.ConfirmationStatus == ConfirmationStatus.Pending)).Sum(x => x.Amount);


                    //var toplam = db.AddedFoods.Where(u => u.UserID == _gelenUser.ID && u.CreatedDate == DateTime.Today).Sum(x => x.CalculatedKcal);

                    if (toplamExpenseConfirm+toplamExpensePending + vm.Amount > (employee.Salary * 3))
                    {
                        TempData["SalaryError"] = "The amount demanded cannot be more than 3 times your salary in the last one year.";
                        return View(vm);
                    }
                    else
                    {
                        vm.ConfirmationStatus = ConfirmationStatus.Pending;
                        var dto = _mapper.Map<ExpenseCreateDTO>(vm);
                        await _expenseService.Create(dto);
                        return RedirectToAction("GetMyAllExpenses");
                    }


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
