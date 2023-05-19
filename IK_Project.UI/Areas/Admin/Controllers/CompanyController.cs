using AutoMapper;
using IK_Project.Application.Models.DTOs.CompanyDTOs;
using IK_Project.Application.Models.DTOs.CompanyManagerDTOs;
using IK_Project.Application.Services.AppUserService;
using IK_Project.Application.Services.CompanyService;
using IK_Project.UI.Areas.Admin.Models.ViewModels.CompanyManagerVMs;
using IK_Project.UI.Areas.Admin.Models.ViewModels.CompanyVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data;

namespace IK_Project.UI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        //private readonly IAppUserService _appUserService;
        IWebHostEnvironment _webHostEnvironment;

        public CompanyController(ICompanyService companyService, IMapper mapper,IWebHostEnvironment webHostEnvironment)
        {
            _companyService = companyService;
            _mapper = mapper;
            _webHostEnvironment= webHostEnvironment;

        }

        public IActionResult Index()
        {
            return View();
        }


        //Aktif Company Listeleme
        public async Task<IActionResult> GetAllActiveCompanies()
        {
            var listDTO = await _companyService.GetDefaults(x => x.Status == Domain.Enums.Status.Active);
            var listVM = _mapper.Map<List<CompanyListVM>>(listDTO);

            return View(listVM);
        }
        public async Task<IActionResult> GetAllCompanies()
        {
            List<CompanyListVM> companyList = _mapper.Map<List<CompanyListVM>>(await _companyService.AllCompanies());
            return View(companyList);

        }

        //Company Ekleme
        [HttpGet]
        public IActionResult Create()
        {
            CompanyCreateVM createVm = new CompanyCreateVM();

            return View(createVm);

        }

        [HttpPost]
        public async Task<IActionResult> Create(CompanyCreateVM vm)
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

                    vm.Logo = tamDosyaAdi;
                }

                else
                {
                    vm.Logo = vm.Logo;
                }
                try
                {
                    var dto = _mapper.Map<CompanyCreateDTO>(vm);
                    await _companyService.Create(dto);
                    return RedirectToAction("GetAllCompanies");
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                }
            }
            return View(vm);
        }

        //Delete
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _companyService.Remove(id);
                return RedirectToAction("GetAllActiveCompanies");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("GetAllActiveCompanies");
            }
        }

        //  Category/Details/

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                CompanyUpdateVM company = _mapper.Map<CompanyUpdateVM>(await _companyService.GetById(id));

                return View(company);
                //var dto = _mapper.Map<CompanyListDTO>(id);
                //await _companyService.AllCompanies();
                //var company = await _context.Movie.SingleOrDefaultAsync(m => m.ID == id);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Details(CompanyUpdateVM vm)
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

                vm.Logo = tamDosyaAdi;
            }

            else
            {
                vm.Logo = vm.Logo;
            }

            var editDto = _mapper.Map<CompanyUpdateDTO>(vm);
            await _companyService.Edit(editDto);
            return RedirectToAction("GetAllCompanies");
        }


        

   
    }




}
