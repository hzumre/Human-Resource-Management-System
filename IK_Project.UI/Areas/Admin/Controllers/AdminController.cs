using AutoMapper;
using IK_Project.Application.Models.DTOs.AdminDTOs;
using IK_Project.Application.Services.AdminSevice;
using IK_Project.Application.Services.AppUserService;
using IK_Project.UI.Areas.Admin.Models.ViewModels.AdminVM;
using IK_Project.UI.Areas.Admin.Models.ViewModels.UserVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace IK_Project.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]

    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;
        private readonly IAppUserService _appUserService;
        IWebHostEnvironment _webHostEnvironment;

        public AdminController(IAdminService adminService, IMapper mapper, IAppUserService appUserService, IWebHostEnvironment webHostEnvironment)
        {
            _adminService = adminService;
            _mapper = mapper;
            _appUserService = appUserService;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {


            AdminCreateVM vm = new AdminCreateVM();
            vm.AppUserDTOs = await _appUserService.GetUsers();
            return View(vm);

        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminCreateVM vm)
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
                    var dto = _mapper.Map<AdminCreateDTO>(vm);
                   await _adminService.Create(dto);
                    return RedirectToAction("Index", "Home", new { area = "" });  
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
