using AutoMapper;
using IK_Project.Application.Models.DTOs.CompanyDTOs;
using IK_Project.Application.Models.DTOs.ExpenseDTOs;
using IK_Project.Application.Services.CompanyManagerService;
using IK_Project.Application.Services.EmployeeService;
using IK_Project.Application.Services.ExpenseService;
using IK_Project.Domain.Entities.Concrete;
using IK_Project.Domain.Enums;
using IK_Project.Infrastructure.Migrations;
using IK_Project.UI.Areas.Admin.Models.ViewModels.CompanyVM;
using IK_Project.UI.Areas.CompanyManager.Models;
using IK_Project.UI.Areas.CompanyManager.Models.Employee;
using IK_Project.UI.Areas.Employee.Models.ExpenseVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace IK_Project.UI.Areas.CompanyManager.Controllers
{
    [Area("CompanyManager")]
    [Authorize(Roles = "CompanyManager")]
    public class ExpenseController : Controller

    {

        private readonly UserManager<AppUser> _userManager;
        private readonly IExpenseService _expenseService;
        private readonly IEmployeeService _employeeService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        readonly private ICompanyManagerService _cmService;

        IMapper _mapper;


        public ExpenseController(UserManager<AppUser> userManager, IExpenseService expenseService, IEmployeeService employeeService, IMapper mapper, IWebHostEnvironment webHostEnvironment, ICompanyManagerService cmService)
        {
            _userManager = userManager;
            _expenseService = expenseService;
            _employeeService = employeeService;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _cmService = cmService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CheckExpenseDemands()

        {


            List<CompanyManagerVM> cmVm = _mapper.Map<List<CompanyManagerVM>>(await _cmService.GetDefaults(x => x.AppUser.UserName == HttpContext.User.Identity.Name));

            var cm = cmVm.FirstOrDefault();

            

            List<ExpenseListVM> expenseList = _mapper.Map<List<ExpenseListVM>>(await _expenseService.GetDefaults(x => x.Employee.CompanyID == cm.CompanyID));
            List<EmployeeListVM> listEmployee = _mapper.Map<List<EmployeeListVM>>(await _employeeService.GetDefaults(x => x.CompanyID == cm.CompanyID));

            

            foreach (var expense in expenseList)
            {
                var demandOwner = _mapper.Map<IK_Project.Domain.Entities.Concrete.Employee>(listEmployee.FirstOrDefault(x => x.Id == expense.EmployeeId));
                expense.Employee = demandOwner;
            }


            return View(expenseList);
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
                ExpenseUpdateVM expense = _mapper.Map<ExpenseUpdateVM>(await _expenseService.GetById(id));

                return View(expense);
                //var dto = _mapper.Map<CompanyListDTO>(id);
                //await _companyService.AllCompanies();
                //var company = await _context.Movie.SingleOrDefaultAsync(m => m.ID == id);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Details(ExpenseUpdateVM vm)
        {

            var expenseDto = _mapper.Map<ExpenseUpdateDTO>(vm);
            await _expenseService.Edit(expenseDto);
            return RedirectToAction("CheckExpenseDemands");
        }




    }
}
