using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infraestructure.Entity.Models
{
    [Table("Permissions")]
    public class PermissionsEntity
    {
        [Key]
        public int Id { get; set; }
        public string EmployeeForename { get; set; } = null!;
        public string EmployeeSurname { get; set; } = null!;

        [ForeignKey("PermissionTypesEntity")]
        public int PermissionType { get; set; }
        public DateTime PremissionDate { get; set; }

        public virtual PermissionTypesEntity PermissionTypesEntity { get; set; }

    }
}
