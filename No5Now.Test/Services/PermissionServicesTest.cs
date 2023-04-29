using AutoMapper;
using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Models;
using Moq;
using N5Now.Domain.DTO.Permissions;
using N5Now.Domain.Mappins;
using N5Now.Domain.Services.ElasticSearchs.Interfaces;
using N5Now.Domain.Services.Kafka.Interface;
using N5Now.Domain.Services.Permissions;
using N5Now.Domain.Services.Permissions.Interfaces;
using Xunit;

namespace No5Now.Test.Services
{
    public class PermissionServicesTest
    {
        #region Attributes
        private readonly IMapper _mapper;
        private readonly Mock<IElasticsearchService> _elasticsearchService;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IKafkaServices> _kafkaServices;

        private readonly IPermissionServices _permissionServices;
        #endregion

        #region Builder
        public PermissionServicesTest()
        {
            //arranges
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new SettingAutomapper());
            });
            _mapper = mappingConfig.CreateMapper();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _elasticsearchService = new Mock<IElasticsearchService>();
            _kafkaServices = new Mock<IKafkaServices>();

            _permissionServices = new PermissionServices(_unitOfWorkMock.Object,
                                                         _mapper,
                                                         _elasticsearchService.Object,
                                                         _kafkaServices.Object);
        }
        #endregion

        #region Test
        [Fact]
        public async Task GetPermissionsTest()
        {
            // Arrange
            _elasticsearchService.Setup(x => x.GetDocuments())
                                .ReturnsAsync(new List<PermissionDto>()
                                {
                                    new PermissionDto()
                                    {
                                        Id = 1,
                                        EmployeeForename="Juan",
                                        EmployeeSurname="Rodriguez",
                                        PermissionType=1,
                                        PremissionDate=DateTime.Now,
                                    },
                                    new PermissionDto()
                                    {
                                        Id = 2,
                                        EmployeeForename="Pablo",
                                        EmployeeSurname="Toro",
                                        PermissionType=2,
                                        PremissionDate=DateTime.Now,
                                    }
                                }.ToList());

            // Act
            var result = await _permissionServices.GetPermissions();

            // Assert
            var model = Assert.IsAssignableFrom<IEnumerable<PermissionDto>>(result);
            Assert.True(model.Any());
            Assert.Equal(2, model.Count());

        }


        [Fact]
        public async Task ModifyPermissionTrueUpdate_Test()
        {
            // Arrange
            _elasticsearchService.Setup(x => x.GetDocument(It.IsAny<int>()))
                                .ReturnsAsync(new PermissionDto()
                                {
                                    Id = 2,
                                    EmployeeForename = "Juan",
                                    EmployeeSurname = "Rodriguez",
                                    PermissionType = 1,
                                    PremissionDate = DateTime.Now,
                                });
            _unitOfWorkMock.Setup(x => x.PermissionsRepository.Update(It.IsAny<PermissionsEntity>()));
            _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(1);

            var permission = new UpdatePermissionDto()
            {
                Id = 2,
                EmployeeForename = "Juan",
                EmployeeSurname = "Rodriguez",
                PermissionType = 1,
            };

            // Act
            var result = await _permissionServices.ModifyPermission(permission);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ModifyPermission_Error_Test()
        {
            // Arrange
            _elasticsearchService.Setup(x => x.GetDocument(It.IsAny<int>()));


            // act & assert
            Assert.Throws<AggregateException>(() => _permissionServices.ModifyPermission(It.IsAny<UpdatePermissionDto>()).Result);
        }


        [Fact]
        public async Task RequestPermission_Test()
        {
            // Arrange
            _unitOfWorkMock.Setup(x => x.PermissionsRepository.Insert(It.IsAny<PermissionsEntity>()));
            _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(1);

            var permission = new AddPermissionDto()
            {
                EmployeeForename = "Juan",
                EmployeeSurname = "Rodriguez",
                PermissionType = 1,
            };

            // Act
            var result = await _permissionServices.RequestPermission(permission);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void RequestPermission_Error_Test()
        {
            // Arrange
            // Arrange
            _unitOfWorkMock.Setup(x => x.PermissionsRepository.Insert(It.IsAny<PermissionsEntity>()));
            _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(0);

            // act & assert
            Assert.Throws<AggregateException>(() => _permissionServices.RequestPermission(It.IsAny<AddPermissionDto>()).Result);
        }
        #endregion
    }
}
