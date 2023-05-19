using AutoMapper;
using IK_Project.Application.Models.DTOs.AdminDTOs;
using IK_Project.Application.Models.DTOs.CompanyDTOs;
using IK_Project.Application.Models.DTOs.CompanyManagerDTOs;
using IK_Project.Application.Services.AdminSevice;
using IK_Project.Application.Services.AdvanceService;
using IK_Project.Application.Services.AppUserService;
using IK_Project.Application.Services.CompanyManagerService;
using IK_Project.Application.Services.CompanyService;
using IK_Project.Application.Services.ExpenseService;
using IK_Project.Application.Services.PermissionService;
using IK_Project.Domain.Entities.Concrete;
using IK_Project.Domain.Repositories;
using IK_Project.UI.Areas.Admin.Models.ViewModels.AdminVM;
using IK_Project.UI.Areas.Admin.Models.ViewModels.CompanyManagerVMs;
using IK_Project.UI.Areas.CompanyManager.Models;
using IK_Project.UI.Areas.CompanyManager.Models.Employee;
using IK_Project.UI.Areas.Employee.Models.AdvanceVMs;
using IK_Project.UI.Areas.Employee.Models.ExpenseVMs;
using IK_Project.UI.Areas.Employee.Models.PermissionVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IK_Project.UI.Areas.CompanyManager.Controllers
{
    [Area("CompanyManager")]
    [Authorize(Roles ="CompanyManager")]
    public class HomeController : Controller
    {
        readonly private IAppUserService _appUserService;
        readonly private IMapper _mapper;
        readonly private ICompanyManagerService _cmService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        IWebHostEnvironment _webHostEnvironment;
        IExpenseService _expenseService;
        IAdvanceService _advanceService;
        IPermissionService _permissionService;
        private readonly ICompanyService _companyService;

        public HomeController(IAppUserService appUserService, IMapper mapper, ICompanyManagerService cmService, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IWebHostEnvironment webHostEnvironment, IExpenseService expenseService,
        IAdvanceService advanceService,
        IPermissionService permissionService, ICompanyService companyService)

        {
            _appUserService = appUserService;
            _mapper = mapper;
            _cmService = cmService;
            _signInManager = signInManager;
            _userManager = userManager;
            this._webHostEnvironment = webHostEnvironment;
            _advanceService = advanceService;
            _expenseService = expenseService;
            _permissionService = permissionService;
            _companyService = companyService;
        }

        public async Task<IActionResult> Index()
        {


            List<CompanyManagerVM> cmVm = _mapper.Map<List<CompanyManagerVM>>(await _cmService.GetDefaults(x => x.AppUser.UserName == HttpContext.User.Identity.Name));

            var cm = cmVm.FirstOrDefault();


            NotificationVM notificationVM = new NotificationVM();
           List<AdvanceListVM> listAdvance = _mapper.Map<List<AdvanceListVM>>( await _advanceService.GetDefaults(x => x.ConfirmationStatus == Domain.Enums.ConfirmationStatus.Pending && x.Employee.CompanyID == cm.CompanyID));

            List<PermissionListVM> permissionList = _mapper.Map<List<PermissionListVM>>( await _permissionService.GetDefaults(x => x.ConfirmationStatus == Domain.Enums.ConfirmationStatus.Pending && x.Employee.CompanyID == cm.CompanyID));


            List<ExpenseListVM> expenseList = _mapper.Map<List<ExpenseListVM>>(await _expenseService.GetDefaults(x => x.ConfirmationStatus == Domain.Enums.ConfirmationStatus.Pending && x.Employee.CompanyID == cm.CompanyID));

            notificationVM.Advances = listAdvance;
            notificationVM.Permissions = permissionList;
            notificationVM.Expenses = expenseList;
            
            return View(notificationVM);
        }


        [HttpGet]
        public async Task<IActionResult> ProfileDetails()
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {

                List<CompanyManagerVM> cmVm = _mapper.Map<List<CompanyManagerVM>>(await _cmService.GetDefaults(x => x.AppUser.UserName == HttpContext.User.Identity.Name));

                var cm = cmVm.FirstOrDefault();

                List<CompanyListDTO> company = await _companyService.GetDefaults(x => x.Id == cm.CompanyID);

                TempData["companyName"] = company.FirstOrDefault().CompanyName;

                //var userInfo = _mapper.Map<AdminListVM>(await _appUserService.GetByUserName(userName));

                //return View(userInfo);

                return View(cm);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ProfileDetails(CompanyManagerVM vm)
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

            var cmDTO = _mapper.Map<CompanyManagerUpdateDTO>(vm);
            await _cmService.Edit(cmDTO);

            return RedirectToAction("Index", "Home");




        }
    }
}
