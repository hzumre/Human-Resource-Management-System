using AutoMapper;
using IK_Project.Application.Models.DTOs.AdminDTOs;
using IK_Project.Application.Models.DTOs.AdvanceDTOs;
using IK_Project.Application.Models.DTOs.AppRoleDTOs;
using IK_Project.Application.Models.DTOs.AppUserDTOs;
using IK_Project.Application.Models.DTOs.CompanyDTOs;
using IK_Project.Application.Models.DTOs.CompanyManagerDTOs;
using IK_Project.Application.Models.DTOs.EmployeeDTOs;
using IK_Project.Application.Models.DTOs.ExpenseDTOs;
using IK_Project.Application.Models.DTOs.MenuDTOs;
using IK_Project.Application.Models.DTOs.PermissionDTOs;
using IK_Project.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Application.AutoMapper
{
    public class Mapping:Profile
    {
        public Mapping()
        {
            CreateMap<AppUser, RegisterDTO>().ReverseMap();
            CreateMap<AppRole, RoleAssignDTO>().ReverseMap();
            CreateMap<AppRole, AppRoleCreateDTO>().ReverseMap();
            CreateMap<AppRole, AppRoleUpdateDTO>().ReverseMap();
            CreateMap<AppRole, AppRoleListDTO>().ReverseMap();
            //AppUser
            CreateMap<AppUser, LoginDTO>().ReverseMap();
            CreateMap<AppUser, UserListDTO>().ReverseMap();
            CreateMap<AppUser, UserRoleAssignDTO>().ReverseMap();

            //AppRole

            CreateMap<AppRole, AppRoleCreateDTO>().ReverseMap();
            CreateMap<AppRole, AppRoleListDTO>().ReverseMap();
            CreateMap<AppRole, AppRoleUpdateDTO>().ReverseMap();
            CreateMap<AppRole, UserRoleAssignDTO>().ReverseMap();


            //Employee
            CreateMap<Employee, EmployeeCreateDTO>().ReverseMap();
            CreateMap<Employee, Models.DTOs.EmployeeDTOs.EmployeeListDTO>().ReverseMap();
            CreateMap<Employee, EmployeeUpdateDTO>().ReverseMap();

            //CompanyManager
            CreateMap<CompanyManager, CompanyManagerCreateDTO>().ReverseMap();
            CreateMap<CompanyManager, CompanyManagerListDTO>().ReverseMap();
            CreateMap<CompanyManager, CompanyManagerUpdateDTO>().ReverseMap();

            //Admin
            CreateMap<Admin, AdminCreateDTO>().ReverseMap();
            CreateMap<Admin, Models.DTOs.AdminDTOs.AdminListDTO>().ReverseMap();
            CreateMap<Admin, AdminUpdateDTO>().ReverseMap();


            //Company
            CreateMap<Company, CompanyUpdateDTO>().ReverseMap();
            CreateMap<Company, CompanyListDTO>().ReverseMap();
            CreateMap<Company, CompanyCreateDTO>().ReverseMap();


            //Menu
            CreateMap<Menu, MenuCreateDTO>().ReverseMap();
            CreateMap<Menu, MenuListDTO>().ReverseMap();
            CreateMap<Menu, MenuUpdateDTO>().ReverseMap();


            //Expense
            CreateMap<Expense, ExpenseCreateDTO>().ReverseMap();
            CreateMap<Expense, ExpenseListDTO>().ReverseMap();
            CreateMap<Expense, ExpenseUpdateDTO>().ReverseMap();

            //Hatice
            CreateMap<Permission, PermissionCreateDTO>().ReverseMap();
            CreateMap<Permission, PermissionListDTO>().ReverseMap();
            CreateMap<Permission, PermissionUpdateDTO>().ReverseMap();

            //Tuğçe
            CreateMap<Advance, AdvanceCreateDTO>().ReverseMap();
            CreateMap<Advance, AdvanceListDTO>().ReverseMap();
            CreateMap<Advance, AdvanceUpdateDTO>().ReverseMap();


        }
    }
}
