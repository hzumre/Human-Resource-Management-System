using AutoMapper;
using IK_Project.Application.Models.DTOs.AdminDTOs;
using IK_Project.Application.Models.DTOs.AppUserDTOs;
using IK_Project.Application.Models.DTOs.CompanyDTOs;
using IK_Project.Application.Services.AdminSevice;
using IK_Project.Application.Services.AppUserService;
using IK_Project.Application.Services.CompanyService;
using IK_Project.Domain.Entities.Concrete;
using IK_Project.Infrastructure.IMailSender;
using IK_Project.UI.Areas.Admin.Models.ViewModels;
using IK_Project.UI.Areas.Admin.Models.ViewModels.AdminVM;
using IK_Project.UI.Areas.Admin.Models.ViewModels.UserVMs;
using IK_Project.UI.Areas.CompanyManager.Controllers;
using IK_Project.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Web;

namespace IK_Project.UI.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Area("Admin")]

    public class HomeController : Controller
    {

        readonly private IAppUserService _appUserService;
        readonly private IMapper _mapper;
        readonly private IAdminService _adminService;
        readonly private ICompanyService _companyService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailSender _emailSender;

        public HomeController(IAppUserService appUserService, IMapper mapper, IAdminService adminService, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IWebHostEnvironment webHostEnvironment, IEmailSender emailSender, ICompanyService companyService)

        {
            _appUserService = appUserService;
            _mapper = mapper;
            _adminService = adminService;
            _signInManager = signInManager;
            _userManager = userManager;
            this._webHostEnvironment = webHostEnvironment;
            _emailSender = emailSender;
            _companyService = companyService;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet, AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if (ModelState.IsValid)
            {
                var _loginDto = _mapper.Map<LoginDTO>(vm);
                var result = await _appUserService.Login(_loginDto);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(vm.UserName);
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });

                    }
                    else if (roles.Contains("CompanyManager"))
                    {
                        return RedirectToAction("Index", "Home", new { area = "CompanyManager" });
                        //return RedirectToRoute(new { controller = "Home", action = "Index", area="CompanyManager" });



                    }
                    else if (roles.Contains("Employee"))
                    {
                        return RedirectToAction("Index", "Home", new { area = "Employee" });

                    }
                    else
                    {
                        TempData["error"] = "Login failed.";
                        return View(vm);
                    }
                }
            }

            TempData["error"] = "Login failed.";
            return View(vm);
        }


        [AllowAnonymous]
        public async Task<IActionResult> LogOut()
        {
            await _appUserService.LogOut();

            return RedirectToAction("Login", "Home");
        }

        [AllowAnonymous]//Bu attribute sayesinde ilgili action methodun Authorize kapsamından çıkmasını istiyoruz. Neden? Çünkü kullanıcı herhangi bir kimlik doğrulamasından yani authentication işleminden geçmeden Register sayfasını görebilmeili ve sisteme register olabilmelidir.
        public IActionResult Register()
        {


            if (User.Identity.IsAuthenticated)//Kullanıcı giriş yapmışsa anasayfaya yönlendirilsin.
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(RegisterVM vm)
        {

            if (ModelState.IsValid)
            {
                var user = _mapper.Map<RegisterDTO>(vm);
                var result = await _appUserService.Register(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
                foreach (var item in result.Errors)
                {

                    ModelState.AddModelError(item.Code, item.Description);
                    TempData["error"] = "Kayıt Oluşturulurken Bir Hata Meydana Geldi";
                }
            }

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> ProfileDetails()
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {

                List<AdminUpdateVM> adminVm = _mapper.Map<List<AdminUpdateVM>>(await _adminService.GetDefaults(x => x.AppUser.UserName == HttpContext.User.Identity.Name));

                var admin = adminVm.FirstOrDefault();

                List<CompanyListDTO> company = await _companyService.GetDefaults(x => x.Id == admin.CompanyID);

                TempData["companyName"] = company.FirstOrDefault().CompanyName;

                //var userInfo = _mapper.Map<AdminListVM>(await _appUserService.GetByUserName(userName));

                //return View(userInfo);

                return View(admin);
            }
            else
            {
                return View();
            }


        }

        [HttpPost]
        public async Task<IActionResult> ProfileDetails(AdminUpdateVM vm)
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

            var adminDTO = _mapper.Map<AdminUpdateDTO>(vm);
            await _adminService.Edit(adminDTO);

            return RedirectToAction("Index", "Home");

        }

        [AllowAnonymous]
        public IActionResult PasswordReset()
        {
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> PasswordReset(ResetPasswordViewModel vm)
        {
            AppUser user = await _userManager.FindByEmailAsync(vm.Email);
            if (user != null)
            {
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                string mailBody = $"<a target=\"_blank\"href=\"http://localhost:5254{Url.Action("UpdatePassword", "Home", new { userId = user.Id, token = HttpUtility.UrlEncode(resetToken) })}\"> Click to update your password </a>";
                string subject = "Update Password";
                await _emailSender.SendEmailAsync(user.Email, subject, mailBody);
                ViewBag.State = true;
            }

            else
            {
                ViewBag.State = false;
            }

            return View();


        }

        [HttpGet("[action]/{userId}/{token}")]
        public IActionResult UpdatePassword(string userId, string token)
        {
            return View();
        }

        [HttpPost("[action]/{userId}/{token}")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordViewModel vm, string userId, string token)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            IdentityResult result = await _userManager.ResetPasswordAsync(user, HttpUtility.UrlDecode(token), vm.Password);
            if (result.Succeeded)
            {
                ViewBag.State = true;
                await _userManager.UpdateSecurityStampAsync(user);
            }
            else
            {
                ViewBag.State = false;
            }

            return View();
        }

    }
}
