using IK_Project.Application.Services.AdminSevice;
using IK_Project.Application.Services.AdvanceService;
using IK_Project.Application.Services.AppRoleService;
using IK_Project.Application.Services.AppUserService;
using IK_Project.Application.Services.CompanyManagerService;
using IK_Project.Application.Services.CompanyService;
using IK_Project.Application.Services.EmailSenderService;
using IK_Project.Application.Services.EmployeeService;
using IK_Project.Application.Services.ExpenseService;
using IK_Project.Application.Services.MenuService;
using IK_Project.Application.Services.PermissionService;
using IK_Project.Domain.Entities.Concrete;
using IK_Project.Domain.Repositories;
using IK_Project.Infrastructure.DataAccess;
using IK_Project.Infrastructure.IMailSender;
using IK_Project.Infrastructure.RepositoriesConcrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<IKProjectDBContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("dbConnection")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddIdentity<AppUser, AppRole>(x =>
{
    x.SignIn.RequireConfirmedEmail = false;
    x.SignIn.RequireConfirmedPhoneNumber = false;
    x.SignIn.RequireConfirmedAccount = false;
    x.Password.RequiredLength = 3;
    x.Password.RequireNonAlphanumeric = false;
    x.Password.RequireUppercase = false;
    x.Password.RequireLowercase = false;
    x.Password.RequireDigit = false;
    x.Password.RequiredUniqueChars = 0;
    

}).AddEntityFrameworkStores<IKProjectDBContext>().AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromSeconds(12000);

    options.LoginPath = "/Account/Login";
    //options.AccessDeniedPath = "//Account/AccessDenied";
    options.SlidingExpiration = true;
});

//.Net Core çekirdeði IoC prensibi ile çalýþýr. Bu yüzden AspNet Core Mvc için hazýrlanan IoC containerlarýna hangi talepte hangi nesnenin resolve edileceðini veriyoruz.
builder.Services.AddTransient<IAppUserService, AppUserService>();
builder.Services.AddTransient<ICompanyService, CompanyService>();
builder.Services.AddTransient<ICompanyRepository, CompanyRepository>();
builder.Services.AddTransient<IAdminRepository, AdminRepository>();
builder.Services.AddTransient<IAdminService, AdminService>();
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddTransient<ICompanyManagerRepository, CompanyManagerRepository>();
builder.Services.AddTransient<ICompanyManagerService, CompanyManagerService>();
builder.Services.AddTransient<IMenuRepository, MenuRepository>();
builder.Services.AddTransient<IMenuService, MenuService>();
builder.Services.AddTransient<IAppRoleService, AppRoleService>();
builder.Services.AddTransient<IEmailSender, EmailSenderService>();
builder.Services.AddTransient<IExpenseService, ExpenseService>();
builder.Services.AddTransient<IExpenseRepository, ExpenseRepository>();
builder.Services.AddTransient<IPermissionService, PermissionService>();
builder.Services.AddTransient<IPermissionRepository, PermissionRepository>();
builder.Services.AddTransient<IAdvanceService, AdvanceService>();
builder.Services.AddTransient<IAdvanceRepository,AdvanceRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseCookiePolicy();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
