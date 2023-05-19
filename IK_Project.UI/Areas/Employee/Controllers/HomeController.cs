using AutoMapper;
using IK_Project.Application.Services.AppUserService;
using IK_Project.Application.Services.EmployeeService;
using IK_Project.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using IK_Project.Application.Models.DTOs.CompanyManagerDTOs;
using IK_Project.UI.Areas.CompanyManager.Models;
using IK_Project.UI.Areas.Employee.Models;
using IK_Project.Application.Models.DTOs.EmployeeDTOs;
using IK_Project.Application.Services.CompanyService;
using IK_Project.Application.Models.DTOs.CompanyDTOs;

namespace IK_Project.UI.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = "Employee")]
    public class HomeController : Controller
    {
        readonly private IAppUserService _appUserService;
        readonly private IMapper _mapper;
        readonly private IEmployeeService _employeeService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        IWebHostEnvironment _webHostEnvironment;
        readonly private ICompanyService _companyService;

        public HomeController(IAppUserService appUserService, IMapper mapper, IEmployeeService employeeService, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IWebHostEnvironment webHostEnvironment, ICompanyService companyService)

        {
            _appUserService = appUserService;
            _mapper = mapper;
            _employeeService = employeeService;
            _signInManager = signInManager;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _companyService = companyService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ProfileDetails()
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {

                List<EmployeeVM> employeeVm = _mapper.Map<List<EmployeeVM>>(await _employeeService.GetDefaults(x => x.AppUser.UserName == HttpContext.User.Identity.Name));

                var employee = employeeVm.FirstOrDefault();

                List<CompanyListDTO> company = await _companyService.GetDefaults(x => x.Id == employee.CompanyID);

                TempData["companyName"] = company.FirstOrDefault().CompanyName;

                //var userInfo = _mapper.Map<AdminListVM>(await _appUserService.GetByUserName(userName));

                //return View(userInfo);

                return View(employee);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ProfileDetails(EmployeeVM vm)
        {

            if (Request.Form.Files.Count > 0)
            {


                string wwwrootDosyaYolu = _webHostEnvironment.WebRootPath;
                string dosyaAdi = Path.GetFileNameWithoutExtension(Request.Form.Files[0].FileName);
                string dosyaUzantisi = Path.GetExtension(Request.Form.Files[0].FileName);
                string tamDosyaAdi = $"{dosyaAdi}_{Guid.NewGuid()}{dosyaUzantisi}";
                string yeniDosyaYolu = Path.Combine($"{wwwrootDosyaYolu}/images/{tamDosyaAdi}");

                using (var fileStream = new FileStream(yeniDosyaYolu, FileMode.Create))
                {
                    Request.Form.Files[0].CopyTo(fileStream);

                }

                vm.ProfilePhoto = tamDosyaAdi;
            }

            else
            {
                vm.ProfilePhoto = vm.ProfilePhoto;
            }

            var eDTO = _mapper.Map<EmployeeUpdateDTO>(vm);
            await _employeeService.Edit(eDTO);

            return RedirectToAction("Index", "Home");




        }
    }
}
