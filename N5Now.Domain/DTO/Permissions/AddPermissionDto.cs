using System.ComponentModel.DataAnnotations;

namespace N5Now.Domain.DTO.Permissions
{
    public class AddPermissionDto
    {
        [Required(ErrorMessage = "the EmployeeForename of Permissions is mandatory")]
        public string EmployeeForename { get; set; } = null!;
        [Required(ErrorMessage = "the EmployeeSurname of Permissions is mandatory")]
        public string EmployeeSurname { get; set; } = null!;
        [Required(ErrorMessage = "the PermissionType of Permissions is mandatory")]
        public int PermissionType { get; set; }
    }
}
