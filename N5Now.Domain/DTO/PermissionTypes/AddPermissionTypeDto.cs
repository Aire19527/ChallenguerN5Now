using System.ComponentModel.DataAnnotations;

namespace N5Now.Domain.DTO.PermissionTypes
{
    public class AddPermissionTypeDto
    {
        [Required(ErrorMessage = "the description of PermissionType is mandatory")]
        public string Description { get; set; }
    }
}
