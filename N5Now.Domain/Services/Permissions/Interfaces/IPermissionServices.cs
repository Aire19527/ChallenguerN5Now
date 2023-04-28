using N5Now.Domain.DTO.Permissions;

namespace N5Now.Domain.Services.Permissions.Interfaces
{
    public interface IPermissionServices
    {
        Task<List<PermissionDto>> GetPermissions();
        Task<bool> ModifyPermission(UpdatePermissionDto permission);
        Task<bool> RequestPermission(AddPermissionDto permission);
    }
}
