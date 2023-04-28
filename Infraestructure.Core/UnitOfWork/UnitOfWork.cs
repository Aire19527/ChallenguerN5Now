using Infraestructure.Core.Context;
using Infraestructure.Core.Repository;
using Infraestructure.Core.Repository.Interface;
using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Models;

namespace Infraestructure.Core.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        #region Attributes
        private readonly DataContext _context;
        private bool disposed = false;
        #endregion Attributes

        #region builder
        public UnitOfWork(DataContext context)
        {
            this._context = context;
        }
        #endregion

        #region Properties
        private IRepository<PermissionsEntity> permissionsRepository;
        private IRepository<PermissionTypesEntity> permissionTypesRepository;
        #endregion

        #region Members
        public IRepository<PermissionsEntity> PermissionsRepository
        {
            get
            {
                if (this.permissionsRepository == null)
                    this.permissionsRepository = new Repository<PermissionsEntity>(_context);

                return permissionsRepository;
            }
        }

        public IRepository<PermissionTypesEntity> PermissionTypesRepository
        {
            get
            {
                if (this.permissionTypesRepository == null)
                    this.permissionTypesRepository = new Repository<PermissionTypesEntity>(_context);

                return permissionTypesRepository;
            }
        }

        #endregion
        protected virtual void Dispose(bool disposing)
        {

            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<int> Save() => await _context.SaveChangesAsync();
    }
}
