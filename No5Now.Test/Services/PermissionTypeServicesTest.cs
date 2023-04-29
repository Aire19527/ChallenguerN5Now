using AutoMapper;
using Infraestructure.Core.UnitOfWork.Interface;
using Moq;
using N5Now.Domain.Services.ElasticSearchs.Interfaces;
using N5Now.Domain.Services.Kafka.Interface;
using N5Now.Domain.Services.Permissions.Interfaces;
using N5Now.Domain.Services.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using N5Now.Domain.Mappins;
using Infraestructure.Core.UnitOfWork;

namespace No5Now.Test.Services
{
    public class PermissionTypeServicesTest
    {
        #region Attributes
        //private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IUnitOfWork _unitOfWorkMock;

        private readonly IPermissionTypeServices _permissionTypeServices;
        #endregion

        #region Builder
        public PermissionTypeServicesTest()
        {
            //arranges
            //_unitOfWorkMock = new Mock<IUnitOfWork>();
            var context = new ContextMock();
            _unitOfWorkMock = new UnitOfWork(context.Seed());
            _permissionTypeServices = new PermissionTypeServices(_unitOfWorkMock);
        }
        #endregion
    }
}
