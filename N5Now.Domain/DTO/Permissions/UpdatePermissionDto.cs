using System.ComponentModel.DataAnnotations;

namespace N5Now.Domain.DTO.Permissions
{
    public class UpdatePermissionDto : AddPermissionDto
    {
        [Required(ErrorMessage = "the Id of Permissions is mandatory")]
        public int Id { get; set; }
    }
}
