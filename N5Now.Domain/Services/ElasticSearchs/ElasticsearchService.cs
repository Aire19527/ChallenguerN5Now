using Microsoft.Extensions.Configuration;
using N5Now.Domain.DTO.Permissions;
using N5Now.Domain.Mappins;
using N5Now.Domain.Services.ElasticSearchs.Interfaces;
using Nest;

namespace N5Now.Domain.Services.ElasticSearchs
{
    public class ElasticsearchService : IElasticsearchService
    {
        #region Attributes
        private readonly IConfiguration _configuration;
        private readonly IElasticClient _client;
        #endregion

        #region Builder
        public ElasticsearchService(IConfiguration configuration)
        {
            _configuration = configuration;
            _client = CreateInstance();

            ChekIndex().ConfigureAwait(true);
        }
        #endregion


        #region Methods
        private ElasticClient CreateInstance()
        {
            string host = _configuration.GetSection("ElasticsearchServer:Host").Value;
            string port = _configuration.GetSection("ElasticsearchServer:Port").Value;
            string username = _configuration.GetSection("ElasticsearchServer:Username").Value;
            string password = _configuration.GetSection("ElasticsearchServer:Password").Value;
            var settings = new ConnectionSettings(new Uri(host + ":" + port));
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                settings.BasicAuthentication(username, password);

            return new ElasticClient(settings);
        }

        private async Task ChekIndex()
        {
            string indexName = _configuration.GetSection("ElasticsearchServer:PermisionIndex").Value;
            var anyy = await _client.Indices.ExistsAsync(indexName);
            if (!anyy.Exists)
            {
                var response = await _client.Indices.CreateAsync(indexName,
                ci => ci
                    .Index(indexName)
                    .PermissionMapping()
                    .Settings(s => s.NumberOfShards(3).NumberOfReplicas(1))
                    );
            }
        }

        public async Task<PermissionDto> GetDocument(int id)
        {
            string indexName = _configuration.GetSection("ElasticsearchServer:PermisionIndex").Value;
            var response = await _client.GetAsync<PermissionDto>(id, q => q.Index(indexName));

            return response.Source;
        }

        public async Task<List<PermissionDto>> GetDocuments()
        {
            string indexName = _configuration.GetSection("ElasticsearchServer:PermisionIndex").Value;
            ISearchResponse<PermissionDto> results = await _client.SearchAsync<PermissionDto>(s => s
                 .Index(indexName)
                 .Query(q => q
                    .MatchAll()
                 )
                 .Size(50) // Indicar cantidad de registros devueltos, por defecto se limita a 10
                );

            return results.Documents.ToList();
        }

        public async Task InsertBulkDocuments(List<PermissionDto> permissions)
        {
            string indexName = _configuration.GetSection("ElasticsearchServer:PermisionIndex").Value;
            await _client.IndexManyAsync(permissions, index: indexName);
        }

        public async Task InsertDocument(PermissionDto permission)
        {
            string indexName = _configuration.GetSection("ElasticsearchServer:PermisionIndex").Value;
            await _client.CreateAsync(permission, q => q.Index(indexName));
        }

        public async Task UpdateDocument(PermissionDto permission)
        {
            string indexName = _configuration.GetSection("ElasticsearchServer:PermisionIndex").Value;
            await _client.UpdateAsync<PermissionDto>(permission.Id, a => a.Index(indexName).Doc(permission));
        }

        public async Task DeleteByIdDocument(int id)
        {
            string indexName = _configuration.GetSection("ElasticsearchServer:PermisionIndex").Value;
            await _client.DeleteAsync(DocumentPath<PermissionDto>.Id(id).Index(indexName));
        }

        public async Task DeleteIndex()
        {
            string indexName = _configuration.GetSection("ElasticsearchServer:PermisionIndex").Value;
            await _client.Indices.DeleteAsync(indexName);
        }

        #endregion



    }
}
