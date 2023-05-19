using AutoMapper;
using IK_Project.Application.Models.DTOs.AdminDTOs;
using IK_Project.Application.Models.DTOs.AppUserDTOs;
using IK_Project.Application.Models.DTOs.CompanyDTOs;
using IK_Project.Application.Models.DTOs.EmployeeDTOs;
using IK_Project.Application.Services.AppUserService;
using IK_Project.Application.Services.CompanyManagerService;
using IK_Project.Application.Services.CompanyService;
using IK_Project.Application.Services.EmployeeService;
using IK_Project.Domain.Entities.Concrete;
using IK_Project.Infrastructure.IMailSender;
using IK_Project.UI.Areas.Admin.Models.ViewModels.AdminVM;
using IK_Project.UI.Areas.CompanyManager.Models;
using IK_Project.UI.Areas.CompanyManager.Models.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace IK_Project.UI.Areas.CompanyManager.Controllers
{
    [Area("CompanyManager")]
    [Authorize(Roles = "CompanyManager")]
    public class EmployeeController : Controller
    {

        readonly private IAppUserService _appUserService;
        readonly private IMapper _mapper;
        readonly private ICompanyManagerService _cmService;
        readonly private ICompanyService _companyService;
        readonly private IEmployeeService _employeeService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailSender _emailSender;

        public EmployeeController(IAppUserService appUserService, IMapper mapper, ICompanyManagerService cmService, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IWebHostEnvironment webHostEnvironment, IEmployeeService employeeService, IEmailSender emailSender, ICompanyService companyService)
        {
            _appUserService = appUserService;
            _mapper = mapper;
            _cmService = cmService;
            _signInManager = signInManager;
            _userManager = userManager;
            this._webHostEnvironment = webHostEnvironment;
            _employeeService = employeeService;
            _emailSender = emailSender;
            _companyService= companyService;
        }
    

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {

                List<CompanyManagerVM> cmVm = _mapper.Map<List<CompanyManagerVM>>(await _cmService.GetDefaults(x => x.AppUser.UserName == HttpContext.User.Identity.Name));

                var cm = cmVm.FirstOrDefault();


                List<EmployeeListVM> listEmployee = _mapper.Map<List<EmployeeListVM>>(await _employeeService.GetDefaults(x => x.CompanyID == cm.CompanyID));

                //var userInfo = _mapper.Map<AdminListVM>(await _appUserService.GetByUserName(userName));

                //return View(userInfo);

                return View(listEmployee);
            }
            else
            {
                return View();
            }


        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                List<CompanyManagerVM> cmVm = _mapper.Map<List<CompanyManagerVM>>(await _cmService.GetDefaults(x => x.AppUser.UserName == HttpContext.User.Identity.Name));

                var cm = cmVm.FirstOrDefault();

                //EmployeeCreateVM vm = new EmployeeCreateVM();

                TempData["companyID"]= cm.CompanyID;

                List<CompanyListDTO> company =await _companyService.GetDefaults(x => x.Id == cm.CompanyID);

                TempData["companyName"]=company.FirstOrDefault().CompanyName;

                return View();
            }

            else
            {
                return View();
            }


        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreateVM vm)
        {
            if (ModelState.IsValid)
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

                try
                {

                    //var dtoAppUser = _mapper.Map<RegisterDTO>(vm.AppUser);


                    //var result = await _appUserService.Register(dtoAppUser);
                    //    vm.AppUser.UserName = vm.userName;

                    //vm.AppUser.Email = vm.Email;
                    var result = await _userManager.CreateAsync(vm.AppUser, vm.userPassword);

                    if (result.Succeeded)
                    {
                        AppUser userEmployee = await _appUserService.GetById(vm.AppUser.Id);
                        await _userManager.AddToRoleAsync(userEmployee, "Employee");
                        vm.AppUser = userEmployee;
                        
                        var dto = _mapper.Map<EmployeeCreateDTO>(vm);

                        string recepientEmail = userEmployee.Email;
                        string subject = "Welcome to TeamThreeHR Application";
                        string message = $"Your system registration has been made by your Administrator.<br> Your UserName:{userEmployee.UserName}<br> Your Temporary Password:123<br> You need to change it before using.";
                        await _employeeService.Create(dto);
                        //await _emailSender.SendEmailAsync(recepientEmail, subject, message);

                        return RedirectToAction("GetAllEmployees", "Employee");
                    }

                    else
                    {
                        TempData["error"] = "Error occured.";
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
