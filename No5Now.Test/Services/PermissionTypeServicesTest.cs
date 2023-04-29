using AutoMapper;
using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Models;
using Moq;
using N5Now.Domain.DTO.PermissionTypes;
using N5Now.Domain.Mappins;
using N5Now.Domain.Services.Permissions;
using N5Now.Domain.Services.Permissions.Interfaces;
using Xunit;

namespace No5Now.Test.Services
{
    public class PermissionTypeServicesTest
    {
        #region Attributes
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMapper _mapper;


        private readonly IPermissionTypeServices _permissionTypeServices;
        #endregion

        #region Builder
        public PermissionTypeServicesTest()
        {
            //arranges
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new SettingAutomapper());
            });
            _mapper = mappingConfig.CreateMapper();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _permissionTypeServices = new PermissionTypeServices(_unitOfWorkMock.Object, _mapper);
        }
        #endregion

        #region Test
        [Fact]
        public void GetAll_Test()
        {
            //arranges
            _unitOfWorkMock.Setup(x => x.PermissionTypesRepository.GetAll()).Returns(new List<PermissionTypesEntity>()
            {
                new PermissionTypesEntity()
                {
                    Description="Usuarios"
                },
                new PermissionTypesEntity()
                {
                    Description="Estados"
                }
            });

            // Act
            var result = _permissionTypeServices.GetAll();

            // Assert
            var model = Assert.IsAssignableFrom<IEnumerable<PermissionTypeDto>>(result);
            Assert.True(model.Any());
            Assert.Equal(2, model.Count());
        }



        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Insert_Test(bool test)
        {
            //arranges
            _unitOfWorkMock.Setup(x => x.PermissionTypesRepository.Insert(It.IsAny<PermissionTypesEntity>()));
            if (test)
                _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(1);
            else
                _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(0);


            // Act
            var result = await _permissionTypeServices.Insert(new AddPermissionTypeDto()
            {
                Description = "MockDescription"
            });

            // Assert
            if (test)
                Assert.True(result);
            else
                Assert.False(result);

        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Update_Test(bool test)
        {
            //arranges
            _unitOfWorkMock.Setup(x => x.PermissionTypesRepository.FirstOrDefault(x => x.Id == 1)).Returns(new PermissionTypesEntity()
            {
                Description = "Usuarios"
            });
            _unitOfWorkMock.Setup(x => x.PermissionTypesRepository.Update(It.IsAny<PermissionTypesEntity>()));
            if (test)
                _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(1);
            else
                _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(0);


            // Act
            var result = await _permissionTypeServices.Update(new PermissionTypeDto()
            {
                Id = 1,
                Description = "MockRoles"
            });


            // Assert
            if (test)
                Assert.True(result);
            else
                Assert.False(result);
        }


        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Delete_Test(bool test)
        {
            _unitOfWorkMock.Setup(x => x.PermissionTypesRepository.FirstOrDefault(x => x.Id == 1)).Returns(new PermissionTypesEntity()
            {
                Description = "Usuarios"
            });
            _unitOfWorkMock.Setup(x => x.PermissionTypesRepository.Delete(It.IsAny<PermissionTypesEntity>()));
            if (test)
                _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(1);
            else
                _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(0);

            // Act
            var result = await _permissionTypeServices.Delete(id: 1);

            // Assert
            if (test)
                Assert.True(result);
            else
                Assert.False(result);
        }

        #endregion
    }
}
