using N5Now.Domain.DTO.Permissions;
using Nest;

namespace N5Now.Domain.Mappins
{
    public static class Mapping
    {
        public static CreateIndexDescriptor PermissionMapping(this CreateIndexDescriptor descriptor)
        {
            return descriptor.Map<PermissionDto>(m => m.Properties(p => p
                .Keyword(k => k.Name(n => n.Id))
                .Text(t => t.Name(n => n.EmployeeForename))
                .Text(t => t.Name(n => n.EmployeeSurname))
                .Number(t => t.Name(n => n.PermissionType))
                .Date(t => t.Name(n => n.PremissionDate)))
            );
        }
    }
}
