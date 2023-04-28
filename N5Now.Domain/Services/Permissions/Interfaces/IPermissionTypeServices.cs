using N5Now.Domain.DTO.PermissionTypes;

namespace N5Now.Domain.Services.Permissions.Interfaces
{
    public interface IPermissionTypeServices
    {
        List<PermissionTypeDto> GetAll();
        Task<bool> Insert(AddPermissionTypeDto permissionType);
        Task<bool> Update(PermissionTypeDto data);
        Task<bool> Delete(int id);
    }
}
