using Infraestructure.Core.Repository.Interface;
using Infraestructure.Entity.Models;

namespace Infraestructure.Core.UnitOfWork.Interface
{
    public interface IUnitOfWork
    {
        IRepository<PermissionsEntity> PermissionsRepository { get; }
        IRepository<PermissionTypesEntity> PermissionTypesRepository { get; }

        Task<int> Save();
    }
}
