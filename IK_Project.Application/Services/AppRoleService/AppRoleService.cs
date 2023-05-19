using AutoMapper;
using IK_Project.Application.Models.DTOs.AppRoleDTOs;
using IK_Project.Application.Models.DTOs.AppUserDTOs;
using IK_Project.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Application.Services.AppRoleService
{
    public class AppRoleService:IAppRoleService
    {
        IMapper _mapper;
        readonly RoleManager<AppRole> _roleManager;
        readonly UserManager<AppUser> _userManager;


        public AppRoleService(IMapper mapper, RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            this._mapper = mapper;
            this._roleManager = roleManager;
            this._userManager = userManager;
        }
        public async Task<List<AppRoleListDTO>> AllRoles()
        {
            var roleList = _roleManager.Roles.ToList();
            return _mapper.Map<List<AppRoleListDTO>>(roleList);
        }


        public async Task Create(AppRoleCreateDTO appRoleCreateDTO)

        {
            AppRole appRole = _mapper.Map<AppRole>(appRoleCreateDTO);
            await _roleManager.CreateAsync(appRole);



        }

        public async Task Edit(AppRoleUpdateDTO appRoleUpdateDTO)
        {
            var result = await _roleManager.UpdateAsync(_mapper.Map<AppRole>(appRoleUpdateDTO));
        }

        public async Task<AppRoleUpdateDTO> GetById(Guid id)
        {
            AppRole role = await _roleManager.FindByIdAsync(id.ToString());
            return _mapper.Map<AppRoleUpdateDTO>(role);
        }



        public async Task<bool> IsRoleExists(string approleName)
        {
            var result = await _roleManager.FindByNameAsync(approleName);
            if (result != null)
            {
                return true;
            }

            return false;
        }

        public async Task Remove(Guid id)
        {
            IdentityResult result = await _roleManager.DeleteAsync(await _roleManager.FindByIdAsync(id.ToString()));
        }

        public async Task RoleAssign(Guid id, List<UserRoleAssignDTO> roleDTOlist)
        {
            AppUser appUser = await _userManager.FindByIdAsync(id.ToString());

            foreach (UserRoleAssignDTO item in roleDTOlist)
            {
                await _userManager.AddToRoleAsync(appUser, item.Name);

            }

        }
    }
}
