using IK_Project.Application.Models.DTOs.AppUserDTOs;
using IK_Project.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Application.Services.AppUserService
{
    public interface IAppUserService
    {
        Task<IdentityResult> Register(RegisterDTO registerDTO);
        Task<SignInResult> Login(LoginDTO loginDTO);
        Task LogOut();
        Task<List<UserListDTO>> GetUsers();
        Task<AppUser> GetById(Guid id);
        Task<AppUser> GetByUserName(string userName);
        Task<List<string>> GetUserAssignedRoles(AppUser appUser);
        Task AddRole(AppUser appUser, string appRoleName);
        Task RemoveRole(AppUser appUser, string appRoleName);
    }
}
