using Infraestructure.Entity.Models;
using N5Now.Domain.DTO.Permissions;

namespace N5Now.Domain.Services.ElasticSearchs.Interfaces
{
    public interface IElasticsearchService
    {
        //Task ChekIndex(string indexName);
        Task<PermissionDto> GetDocument(int id);
        Task<List<PermissionDto>> GetDocuments();
        Task InsertBulkDocuments(List<PermissionDto> permissions);
        Task InsertDocument(PermissionDto permission);
        Task UpdateDocument(PermissionDto permission);
        Task DeleteByIdDocument(int id);
        Task DeleteIndex();
    }
}
