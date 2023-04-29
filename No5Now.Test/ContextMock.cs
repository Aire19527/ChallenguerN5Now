using Infraestructure.Core.Context;
using Infraestructure.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No5Now.Test
{
    public class ContextMock
    {
        private DataContext context;

        public DataContext Seed()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
             .UseInMemoryDatabase(Guid.NewGuid().ToString())
             .Options;

            context = new DataContext(options);
            setDataPermissionTypes();
            setDataPermission();

            return context;
        }
        private void setDataPermission()
        {
            context.PermissionsEntity.AddRange(new List<PermissionsEntity>()
            {
                new PermissionsEntity()
                {
                    EmployeeForename="Juan",
                    EmployeeSurname="Rodriguez",
                    PermissionType=1,
                    PremissionDate=DateTime.Now,
                    
                },
                new PermissionsEntity()
                {
                    EmployeeForename="Pablo",
                    EmployeeSurname="Toro",
                    PermissionType=2,
                    PremissionDate=DateTime.Now
                }
            }.AsQueryable().AsNoTracking());
            context.SaveChanges();
        }


        private void setDataPermissionTypes()
        {
            context.PermissionTypesEntity.AddRange(new List<PermissionTypesEntity>()
            {
                new PermissionTypesEntity()
                {
                    Description="Usuarios"
                },
                new PermissionTypesEntity()
                {
                    Description="Estados"
                }
            }.AsQueryable().AsNoTracking());
            context.SaveChanges();
        }
    }
}
