using AutoMapper;
using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Models;
using N5Now.Common.Exceptions;
using N5Now.Common.Resources;
using N5Now.Domain.DTO.PermissionTypes;
using N5Now.Domain.Services.Permissions.Interfaces;

namespace N5Now.Domain.Services.Permissions
{
    public class PermissionTypeServices : IPermissionTypeServices
    {
        #region Attributes
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Builder
        public PermissionTypeServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public List<PermissionTypeDto> GetAll()
        {
            IEnumerable<PermissionTypesEntity> permissionTypes = _unitOfWork.PermissionTypesRepository.GetAll();
            
            return _mapper.Map<List<PermissionTypeDto>>(permissionTypes);
        }
        private PermissionTypesEntity Get(int id)
        {
            PermissionTypesEntity permissionType = _unitOfWork.PermissionTypesRepository.FirstOrDefault(x => x.Id == id);

            return permissionType;
        }

        public async Task<bool> Insert(AddPermissionTypeDto permissionType)
        {
            PermissionTypesEntity entity = new PermissionTypesEntity()
            {
                Description = permissionType.Description
            };
            _unitOfWork.PermissionTypesRepository.Insert(entity);

            return await _unitOfWork.Save() > 0;
        }

        public async Task<bool> Update(PermissionTypeDto data)
        {
            PermissionTypesEntity entity = Get(data.Id);
            if (entity == null)
                throw new BusinessException(GeneralMessages.ItemNoFound);

            entity.Description = data.Description;
            _unitOfWork.PermissionTypesRepository.Update(entity);

            return await _unitOfWork.Save() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            PermissionTypesEntity entity = Get(id);
            if (entity == null)
                throw new BusinessException(GeneralMessages.ItemNoFound);

            _unitOfWork.PermissionTypesRepository.Delete(entity);

            return await _unitOfWork.Save() > 0;
        }
        #endregion
    }
}
