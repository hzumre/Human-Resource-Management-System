using AutoMapper;
using IK_Project.Application.Models.DTOs.PermissionDTOs;
using IK_Project.Domain.Entities.Concrete;
using IK_Project.Domain.Repositories;
using System.Linq.Expressions;


namespace IK_Project.Application.Services.PermissionService
{
    public class PermissionService : IPermissionService
    {
        IPermissionRepository _permissionRepository;
        IMapper _mapper;
        public PermissionService(IPermissionRepository permissionRepository,IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
        }
        public async Task<List<PermissionListDTO>> AllPermissions()
        {
            return _mapper.Map<List<PermissionListDTO>>(await _permissionRepository.GetAll());
        }

        public async Task Create(PermissionCreateDTO permissionCreateDTO)
        {
            var permission = _mapper.Map<Permission>(permissionCreateDTO);

            await _permissionRepository.Add(permission);
        }

        public async Task Edit(PermissionUpdateDTO permissionUpdateDTO)
        {
            Permission permission = _mapper.Map<Permission>(permissionUpdateDTO);
            await _permissionRepository.Update(permission);
        }

        public async Task<PermissionUpdateDTO> GetById(int id)
        {
            return _mapper.Map<PermissionUpdateDTO>(await _permissionRepository.GetBy(x => x.Id == id));
        }

        public async Task<List<PermissionListDTO>> GetDefaults(Expression<Func<Permission, bool>> expression)
        {
            var result = await _permissionRepository.GetDefault(expression);
            var listPermissionResult = _mapper.Map<List<Permission>, List<PermissionListDTO>>(result);
            return listPermissionResult;
        }

        public async Task Remove(int id)
        {
            Permission permission = await _permissionRepository.GetBy(x => x.Id == id);
            await _permissionRepository.Delete(permission);
        }
    }
}
