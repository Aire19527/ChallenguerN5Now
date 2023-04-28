using AutoMapper;
using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Models;
using N5Now.Common.Exceptions;
using N5Now.Common.Resources;
using N5Now.Domain.DTO;
using N5Now.Domain.DTO.Permissions;
using N5Now.Domain.Services.ElasticSearchs.Interfaces;
using N5Now.Domain.Services.Kafka.Interface;
using N5Now.Domain.Services.Permissions.Interfaces;

namespace N5Now.Domain.Services.Permissions
{
    public class PermissionServices : IPermissionServices
    {
        #region Attributes
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IElasticsearchService _elasticsearch;
        private readonly IKafkaServices _kafkaServices;
        #endregion

        #region Builder
        public PermissionServices(IUnitOfWork unitOfWork, 
                                  IMapper mapper,
                                  IElasticsearchService elasticsearchService,
                                  IKafkaServices kafkaServices)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _elasticsearch = elasticsearchService;
            _kafkaServices = kafkaServices;
        }
        #endregion

        #region Methods
        public async Task<List<PermissionDto>> GetPermissions()
        {
            //IEnumerable<PermissionsEntity> permissions = _unitOfWork.PermissionsRepository.GetAll();
            //var result=_mapper.Map<List<PermissionDto>>(permissions);

            List<PermissionDto> list = await _elasticsearch.GetDocuments();
            await _kafkaServices.SendServices(new ServiceKafkaDto()
            {
                Id = Guid.NewGuid(),
                NameOperation = "get"
            });

            return list;
        }

        public async Task<bool> ModifyPermission(UpdatePermissionDto permission)
        {
            PermissionsEntity entity = await Get(permission.Id);
            if (entity == null)
                throw new BusinessException(GeneralMessages.ItemNoFound);

            entity.PermissionType = permission.PermissionType;
            entity.EmployeeForename = permission.EmployeeForename;
            entity.EmployeeSurname = permission.EmployeeSurname;
            _unitOfWork.PermissionsRepository.Update(entity);

            bool result = await _unitOfWork.Save() > 0;
            if (result)
            {
                await _elasticsearch.UpdateDocument(_mapper.Map<PermissionDto>(entity));
                await _kafkaServices.SendServices(new ServiceKafkaDto()
                {
                    Id=Guid.NewGuid(),
                    NameOperation= "modify"
                });
            }
            else
                throw new BusinessException(GeneralMessages.ItemNoUpdated);

            return result;
        }

        public async Task<bool> RequestPermission(AddPermissionDto permission)
        {
            PermissionsEntity entity = new PermissionsEntity()
            {
                EmployeeForename = permission.EmployeeForename,
                EmployeeSurname = permission.EmployeeSurname,
                PermissionType = permission.PermissionType,
                PremissionDate = DateTime.Now
            };
            _unitOfWork.PermissionsRepository.Insert(entity);
            bool result = await _unitOfWork.Save() > 0;
            if (result)
            {
                await _elasticsearch.InsertDocument(_mapper.Map<PermissionDto>(entity));
                await _kafkaServices.SendServices(new ServiceKafkaDto()
                {
                    Id = Guid.NewGuid(),
                    NameOperation = "request"
                });
            }
            else
                throw new BusinessException(GeneralMessages.ItemNoInserted);

            return result;
        }

        private async Task<PermissionsEntity> Get(int id)
        {
            //return _unitOfWork.PermissionsRepository.FirstOrDefault(x => x.Id == id);

            var result = await _elasticsearch.GetDocument(id);

            return _mapper.Map<PermissionsEntity>(result);
        }

        #endregion
    }
}
