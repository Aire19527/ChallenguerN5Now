using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infraestructure.Entity.Models
{
    [Table("PermissionTypes")]
    public class PermissionTypesEntity
    {
        public PermissionTypesEntity()
        {
            PermissionsEntities = new HashSet<PermissionsEntity>();
        }

        [Key]
        public int Id { get; set; }
        public string Description { get; set; } = null!;

        public virtual IEnumerable<PermissionsEntity> PermissionsEntities { get; set; }
    }
}
