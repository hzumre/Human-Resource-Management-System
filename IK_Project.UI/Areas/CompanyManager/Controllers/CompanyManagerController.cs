using AutoMapper;
using IK_Project.Application.Models.DTOs.CompanyDTOs;
using IK_Project.Application.Models.DTOs.CompanyManagerDTOs;
using IK_Project.Application.Services.AppUserService;
using IK_Project.Application.Services.CompanyManagerService;
using IK_Project.Application.Services.CompanyService;
using IK_Project.Domain.Entities.Concrete;
using IK_Project.Infrastructure.IMailSender;
using IK_Project.UI.Areas.Admin.Models.ViewModels.CompanyManagerVMs;
using IK_Project.UI.Areas.CompanyManager.Models;
using IK_Project.UI.Areas.CompanyManager.Models.Employee;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IK_Project.UI.Areas.CompanyManager.Controllers
{
    [Area("CompanyManager")]
    public class CompanyManagerController : Controller
    {
        

        private readonly ICompanyManagerService _companyManagerService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IAppUserService _appUserService;
        private readonly ICompanyService _companyService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<AppUser> _signInManager;
        public CompanyManagerController(ICompanyManagerService companyManagerService, IMapper mapper, IWebHostEnvironment webHostEnvironment, IAppUserService appUserService, ICompanyService companyService, UserManager<AppUser> userManager, IEmailSender emailSender, SignInManager<AppUser> signInManager)
        {
            _companyManagerService = companyManagerService;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _appUserService = appUserService;
            _companyService = companyService;
            _userManager = userManager;
            _emailSender = emailSender;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllActiveCompanyManagers()
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {

                List<CompanyManagerVM> cmVm = _mapper.Map<List<CompanyManagerVM>>(await _companyManagerService.GetDefaults(x => x.AppUser.UserName == HttpContext.User.Identity.Name));

                var cm = cmVm.FirstOrDefault();


                List<CompanyManagerListVM> listCM = _mapper.Map<List<CompanyManagerListVM>>(await _companyManagerService.GetDefaults(x => x.CompanyID == cm.CompanyID && x.Status==Domain.Enums.Status.Active));

                //var userInfo = _mapper.Map<AdminListVM>(await _appUserService.GetByUserName(userName));

                //return View(userInfo);

                return View(listCM);
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> GetAllCompanyManagers()
        {

            if (_signInManager.IsSignedIn(HttpContext.User))
            {

                List<CompanyManagerVM> cmVm = _mapper.Map<List<CompanyManagerVM>>(await _companyManagerService.GetDefaults(x => x.AppUser.UserName == HttpContext.User.Identity.Name));

                var cm = cmVm.FirstOrDefault();


                List<CompanyManagerListVM> listCM = _mapper.Map<List<CompanyManagerListVM>>(await _companyManagerService.GetDefaults(x => x.CompanyID == cm.CompanyID));

                //var userInfo = _mapper.Map<AdminListVM>(await _appUserService.GetByUserName(userName));

                //return View(userInfo);

                return View(listCM);
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
                List<CompanyManagerVM> cmVM = _mapper.Map<List<CompanyManagerVM>>(await _companyManagerService.GetDefaults(x => x.AppUser.UserName == HttpContext.User.Identity.Name));
                var cm = cmVM.FirstOrDefault();
 
                TempData["CompanyID"] = cm.CompanyID;

                List<CompanyListDTO> company = await _companyService.GetDefaults(x => x.Id == cm.CompanyID);

                TempData["companyName"] = company.FirstOrDefault().CompanyName;

                return View();
            }
            else
            {
                return View();
            }

        }

        [HttpPost]
        public async Task<IActionResult> Create(CompanyManagerCreateVM vm)
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

              
                    var result = await _userManager.CreateAsync(vm.AppUser, vm.userPassword);

                    if (result.Succeeded)
                    {

                        AppUser userCM = await _appUserService.GetById(vm.AppUser.Id);
                        await _userManager.AddToRoleAsync(userCM, "CompanyManager");
                        vm.AppUser = userCM;

                        var dto = _mapper.Map<CompanyManagerCreateDTO>(vm);

                        string recepientEmail = userCM.Email;
                        string subject = "Welcome to TeamThreeHR Application";
                        string message = $"Your system registration has been made by your Administrator.<br> Your UserName:{userCM.UserName}<br> Your Temporary Password:123<br> You need to change it before using.";
                        await _companyManagerService.Create(dto);
                        //await _emailSender.SendEmailAsync(recepientEmail, subject, message);

                        return RedirectToAction("GetAllCompanyManagers", "CompanyManager");

                    }
                    else
                    {
                        TempData["error"] = "Error occured";
                    }



                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                }

            }
            return View(vm);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _companyManagerService.Remove(id);
                return RedirectToAction("GetAllActiveCompanyManagers");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("GetAllActiveCompanyManagers");
            }
        }

        //[HttpGet]
        //public async Task<IActionResult> Edit(Guid id)
        //{
        //    var updateDTO = await _companyManagerService.GetById(id);
        //    var updateVm = _mapper.Map<CompanyManagerEditVM>(updateDTO);

        //    return View(updateVm);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Edit(CompanyManagerEditVM vm)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var updateDto = _mapper.Map<CompanyManagerUpdateDTO>(vm);
        //            await _companyManagerService.Edit(updateDto);
        //            return RedirectToAction("GetAllCompanyManagers");
        //        }
        //        catch (Exception ex)
        //        {

        //            TempData["error"] = ex.Message;
        //        }
        //    }
        //    return View(vm);
        //}


        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                CompanyManagerEditVM companyManager = _mapper.Map<CompanyManagerEditVM>(await _companyManagerService.GetById(id));

                List<CompanyListDTO> company = await _companyService.GetDefaults(x => x.Id == companyManager.CompanyID);

                TempData["companyName"] = company.FirstOrDefault().CompanyName;

                return View(companyManager);

            }


        }

        [HttpPost]
        public async Task<IActionResult> Details(CompanyManagerEditVM vm)
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

            var editDto = _mapper.Map<CompanyManagerUpdateDTO>(vm);
            await _companyManagerService.Edit(editDto);
            return RedirectToAction("GetAllCompanyManagers");

        }
    }
}
