using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IK_Project.UI.Models;
using IK_Project.Application.Services.MenuService;
using IK_Project.Domain.Entities.Concrete;
using IK_Project.UI.Areas.Admin.Models.ViewModels.MenuVMs;
using AutoMapper;

namespace IK_Project.UI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMenuService _menuService;
    private readonly IMapper _mapper;


    public HomeController(ILogger<HomeController> logger, IMenuService menuService, IMapper mapper)
    {
        _logger = logger;
        _menuService = menuService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        List<MenuListVM> menuList = _mapper.Map<List<MenuListVM>>(await _menuService.GetDefaults(x => x.Status == Domain.Enums.Status.Active));
        return View(menuList);
    }

    public IActionResult GetOffer()
    {
        return View("GetOffer");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
