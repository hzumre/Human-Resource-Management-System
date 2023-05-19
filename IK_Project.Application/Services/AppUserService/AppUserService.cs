using AutoMapper;
using IK_Project.Application.Models.DTOs.AppUserDTOs;
using IK_Project.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Application.Services.AppUserService
{
    public class AppUserService : IAppUserService
    {
        UserManager<AppUser> _userManager;
        SignInManager<AppUser> _signInManager;
        IMapper _mapper;

        public AppUserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;

        }

        public async Task AddRole(AppUser appUser, string appRoleName)
        {
            await _userManager.AddToRoleAsync(appUser, appRoleName);
        }

        public async Task<AppUser> GetById(Guid id)
        {

            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<AppUser> GetByUserName(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<List<string>> GetUserAssignedRoles(AppUser appUser)
        {
            return await _userManager.GetRolesAsync(appUser) as List<string>;

        }

        public async Task<List<UserListDTO>> GetUsers()
        {
            return _mapper.Map<List<UserListDTO>>(await _userManager.Users.ToListAsync());
        }

        public async Task<SignInResult> Login(LoginDTO loginDTO)
        {
            var result = await _signInManager.PasswordSignInAsync(loginDTO.UserName, loginDTO.Password, false, false);
            return result;
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();

        }

        public async Task<IdentityResult> Register(RegisterDTO registerDTO)
        {
            AppUser appUser = _mapper.Map<AppUser>(registerDTO);
            var result = await _userManager.CreateAsync(appUser, registerDTO.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(appUser, isPersistent: false);
            }

            return result;
        }

        public async Task RemoveRole(AppUser appUser, string appRoleName)
        {
            await _userManager.RemoveFromRoleAsync(appUser, appRoleName);
        }
    }
}
