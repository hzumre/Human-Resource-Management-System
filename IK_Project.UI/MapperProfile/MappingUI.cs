using AutoMapper;
using IK_Project.Application.Models.DTOs.AdminDTOs;
using IK_Project.Application.Models.DTOs.AppRoleDTOs;
using IK_Project.Application.Models.DTOs.AppUserDTOs;
using IK_Project.Domain.Entities.Concrete;
using IK_Project.Application.Models.DTOs.CompanyDTOs;
using IK_Project.Application.Models.DTOs.CompanyManagerDTOs;
using IK_Project.UI.Areas.Admin.Models.ViewModels;
using IK_Project.UI.Areas.Admin.Models.ViewModels.AdminVM;
using IK_Project.UI.Areas.Admin.Models.ViewModels.CompanyManagerVMs;
using IK_Project.UI.Areas.Admin.Models.ViewModels.RoleVMs;
using IK_Project.UI.Areas.Admin.Models.ViewModels.UserVMs;
using IK_Project.UI.Areas.Admin.Models.ViewModels.CompanyVM;
using IK_Project.UI.Areas.CompanyManager.Models;
using IK_Project.UI.Areas.Admin.Models.ViewModels.MenuVMs;
using IK_Project.Application.Models.DTOs.MenuDTOs;
using IK_Project.Application.Models.DTOs.EmployeeDTOs;
using IK_Project.UI.Areas.CompanyManager.Models.Employee;
using IK_Project.UI.Areas.Employee.Models;
using IK_Project.UI.Areas.Employee.Models.ExpenseVMs;
using IK_Project.Application.Models.DTOs.ExpenseDTOs;
using IK_Project.UI.Areas.Employee.Models.AdvanceVMs;
using IK_Project.Application.Models.DTOs.AdvanceDTOs;
using IK_Project.UI.Areas.Employee.Models.PermissionVMs;
using IK_Project.Application.Models.DTOs.PermissionDTOs;

namespace IK_Project.UI.MapperProfile
{
    public class MappingUI:Profile
    {
        public MappingUI()
        {
            CreateMap<RegisterDTO, RegisterVM>().ReverseMap();
            CreateMap<LoginDTO, LoginVM>().ReverseMap();


            CreateMap<AdminListDTO, AdminListVM>().ReverseMap();
            CreateMap<AdminCreateDTO, AdminCreateVM>().ReverseMap();
            CreateMap<AdminUpdateDTO, AdminUpdateVM>().ReverseMap();

            //CreateMap<AdminListVM, AdminUpdateDTO>().ReverseMap();
            CreateMap<AdminListDTO, AdminUpdateVM>().ReverseMap();


            CreateMap<AppRoleCreateDTO, RoleCreateVM>().ReverseMap();
            CreateMap<AppRoleListDTO, RoleListVM>().ReverseMap();
            CreateMap<AppRoleUpdateDTO, RoleEditVM>().ReverseMap();

            CreateMap<CompanyManagerCreateDTO, CompanyManagerCreateVM>().ReverseMap(); 
            CreateMap<CompanyManagerUpdateDTO, CompanyManagerEditVM>().ReverseMap();
            CreateMap<CompanyManagerListDTO, CompanyManagerListVM>().ReverseMap();
            CreateMap<CompanyManagerVM, CompanyManagerUpdateDTO>().ReverseMap();
            CreateMap<CompanyManagerVM, CompanyManagerListDTO>().ReverseMap();


            CreateMap<CompanyCreateDTO, CompanyCreateVM>().ReverseMap();
            CreateMap<CompanyListDTO, CompanyListVM>().ReverseMap();
            CreateMap<CompanyUpdateDTO, CompanyUpdateVM>().ReverseMap();
            


            CreateMap<UserListDTO, UserListsVM>().ReverseMap();
            CreateMap<RoleAssignDTO, UserRoleAssignVM>().ReverseMap();
            CreateMap<AppUser, UserListsVM>().ReverseMap();


            CreateMap<MenuCreateVM, MenuCreateDTO>().ReverseMap();
            CreateMap<MenuListVM, MenuListDTO>().ReverseMap();
            CreateMap<MenuUpdateVM, MenuUpdateDTO>().ReverseMap();


            CreateMap<EmployeeListDTO, EmployeeListVM>().ReverseMap();
            CreateMap<EmployeeCreateDTO, EmployeeCreateVM>().ReverseMap();
            CreateMap<EmployeeListDTO, EmployeeVM>().ReverseMap();
            CreateMap<EmployeeUpdateDTO, EmployeeUpdateVM>().ReverseMap();
            CreateMap<EmployeeVM, EmployeeUpdateDTO>().ReverseMap();
            CreateMap<EmployeeVM, EmployeeListDTO>().ReverseMap();
            CreateMap<Employee, EmployeeListVM>().ReverseMap();




            CreateMap<ExpenseCreateVM, ExpenseCreateDTO>().ReverseMap();
            CreateMap<ExpenseListDTO, ExpenseListVM>().ReverseMap();
            CreateMap<ExpenseUpdateVM, ExpenseUpdateDTO>().ReverseMap();


           
            CreateMap<PermissionCreateVM, PermissionCreateDTO>().ReverseMap();
            CreateMap<PermissionListDTO, PermissionListVM>().ReverseMap();
            CreateMap<PermissionUpdateVM, PermissionUpdateDTO>().ReverseMap();

            //Tuğçe
            CreateMap<AdvanceCreateVM, AdvanceCreateDTO>().ReverseMap();
            CreateMap<AdvanceListVM, AdvanceListDTO>().ReverseMap(); 
            CreateMap<AdvanceUpdateVM, AdvanceUpdateDTO>().ReverseMap();


      
        }
    }
}
