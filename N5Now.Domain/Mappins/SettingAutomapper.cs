using AutoMapper;
using Infraestructure.Entity.Models;
using N5Now.Domain.DTO.Permissions;
using N5Now.Domain.DTO.PermissionTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5Now.Domain.Mappins
{
    public class SettingAutomapper: Profile
    {
        public SettingAutomapper()
        {
            CreateMap<PermissionsEntity, PermissionDto>();
            CreateMap<PermissionDto, PermissionsEntity>();

            CreateMap<PermissionTypeDto, PermissionTypesEntity>();
            CreateMap<PermissionTypesEntity, PermissionTypeDto>();
        }
    }
}
