using AutoMapper;
using IK_Project.Application.Models.DTOs.ExpenseDTOs;
using IK_Project.Application.Services.AdvanceService;
using IK_Project.Application.Services.CompanyManagerService;
using IK_Project.Application.Services.EmployeeService;
using IK_Project.Domain.Entities.Concrete;
using IK_Project.UI.Areas.CompanyManager.Models.Employee;
using IK_Project.UI.Areas.CompanyManager.Models;
using IK_Project.UI.Areas.Employee.Models.ExpenseVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using IK_Project.UI.Areas.Employee.Models.AdvanceVMs;
using IK_Project.Application.Models.DTOs.AdvanceDTOs;

namespace IK_Project.UI.Areas.CompanyManager.Controllers
{
    [Area("CompanyManager")]
    [Authorize(Roles = "CompanyManager")]
    public class AdvanceController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAdvanceService _advanceService;
        private readonly IEmployeeService _employeeService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        readonly private ICompanyManagerService _cmService;

        IMapper _mapper;


        public AdvanceController(UserManager<AppUser> userManager, IAdvanceService advanceService, IEmployeeService employeeService, IMapper mapper, IWebHostEnvironment webHostEnvironment, ICompanyManagerService cmService)
        {
            _userManager = userManager;
            _advanceService = advanceService;
            _employeeService = employeeService;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _cmService = cmService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CheckAdvanceDemands()

        {


            List<CompanyManagerVM> cmVm = _mapper.Map<List<CompanyManagerVM>>(await _cmService.GetDefaults(x => x.AppUser.UserName == HttpContext.User.Identity.Name));

            var cm = cmVm.FirstOrDefault();



            List<AdvanceListVM> advanceList = _mapper.Map<List<AdvanceListVM>>(await _advanceService.GetDefaults(x => x.Employee.CompanyID == cm.CompanyID));
            List<EmployeeListVM> listEmployee = _mapper.Map<List<EmployeeListVM>>(await _employeeService.GetDefaults(x => x.CompanyID == cm.CompanyID));



            foreach (var advance in advanceList)
            {
                var demandOwner = _mapper.Map<IK_Project.Domain.Entities.Concrete.Employee>(listEmployee.FirstOrDefault(x => x.Id == advance.EmployeeId));
                advance.Employee = demandOwner;
            }


            return View(advanceList);
        }


        [HttpGet]
        public async Task<IActionResult> AdvanceDetails(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                AdvanceUpdateVM advance = _mapper.Map<AdvanceUpdateVM>(await _advanceService.GetById(id));

                return View(advance);


                //var dto = _mapper.Map<CompanyListDTO>(id);
                //await _companyService.AllCompanies();
                //var company = await _context.Movie.SingleOrDefaultAsync(m => m.ID == id);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AdvanceDetails(AdvanceUpdateVM vm)
        {

            var expenseDto = _mapper.Map<AdvanceUpdateDTO>(vm);
            await _advanceService.Edit(expenseDto);
            return RedirectToAction("CheckAdvanceDemands");
        }

    }
}
