using Microsoft.AspNetCore.Mvc;
using N5Now.Common.Resources;
using N5Now.Domain.DTO;
using N5Now.Domain.DTO.Permissions;
using N5Now.Domain.Services.Permissions.Interfaces;
using N5Now.Handlers;
using Serilog;
using System.Reflection;
using System.Text.Json;

namespace N5Now.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(CustomExceptionFilterAttribute))]
    [ServiceFilter(typeof(CustomValidationFilterAttribute))]
    public class PermissionsController : ControllerBase
    {
        #region Attributes
        private readonly IPermissionServices _permission;
        #endregion

        #region Builder
        public PermissionsController(IPermissionServices permissionServices)
        {
            _permission = permissionServices;
        }
        #endregion

        #region Services
        [HttpGet]
        [Route("GetPermissions")]
        public async Task<IActionResult> GetPermissions()
        {

            List<PermissionDto> list = await _permission.GetPermissions();
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Message = string.Empty,
                Result = list
            };

            string logMessage = $"[Service]: Api/Permissions/GetPermissions [LogMessage]: Consult";
            Log.Warning(logMessage);

            return Ok(response);
        }

        [HttpPost]
        [Route("RequestPermission")]
        public async Task<IActionResult> RequestPermission(AddPermissionDto permission)
        {
            IActionResult action;
            bool result = await _permission.RequestPermission(permission);
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = result,
                Result = result,
                Message = result ? GeneralMessages.ItemInserted : GeneralMessages.ItemNoInserted
            };

            if (result)
                action = Ok(response);
            else
                action = BadRequest(response);

            string logMessage = $"[Service]: Api/Permissions/ModifyPermission [LogMessage]: Request - [DTO]: {JsonSerializer.Serialize(permission)}";
            Log.Warning(logMessage);

            return action;
        }

        [HttpPut]
        [Route("ModifyPermission")]
        public async Task<IActionResult> ModifyPermission(UpdatePermissionDto permission)
        {
            IActionResult action;
            bool result = await _permission.ModifyPermission(permission);
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = result,
                Result = result,
                Message = result ? GeneralMessages.ItemUpdated : GeneralMessages.ItemNoUpdated
            };

            if (result)
                action = Ok(response);
            else
                action = BadRequest(response);

            string logMessage = $"[Service]: Api/Permissions/ModifyPermission [LogMessage]: Modify - [DTO]: {JsonSerializer.Serialize(permission)}";
            Log.Warning(logMessage);

            return action;
        }

        #endregion
    }
}
