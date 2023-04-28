using Microsoft.AspNetCore.Mvc;
using N5Now.Common.Resources;
using N5Now.Domain.DTO;
using N5Now.Domain.DTO.PermissionTypes;
using N5Now.Domain.Services.Permissions.Interfaces;
using N5Now.Handlers;

namespace N5Now.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(CustomExceptionFilterAttribute))]
    [ServiceFilter(typeof(CustomValidationFilterAttribute))]
    public class PermissionTypesController : ControllerBase
    {
        #region Attributes
        private readonly IPermissionTypeServices _permissionType;
        #endregion

        #region Builder
        public PermissionTypesController(IPermissionTypeServices permissionTypeServices)
        {
            this._permissionType = permissionTypeServices;
        }
        #endregion

        #region Services
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            List<PermissionTypeDto> list = _permissionType.GetAll();
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Message = string.Empty,
                Result = list
            };

            return Ok(response);
        }

        [HttpPost]
        [Route("Insert")]
        public async Task<IActionResult> Insert(AddPermissionTypeDto permissionType)
        {
            IActionResult action;
            bool result = await _permissionType.Insert(permissionType);
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

            return action;
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(PermissionTypeDto permissionType)
        {
            IActionResult action;
            bool result = await _permissionType.Update(permissionType);
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

            return action;
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            IActionResult action;
            bool result = await _permissionType.Delete(id);
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = result,
                Result = result,
                Message = result ? GeneralMessages.ItemDeleted : GeneralMessages.ItemNoDeleted
            };

            if (result)
                action = Ok(response);
            else
                action = BadRequest(response);

            return action;
        }
        #endregion
    }
}
