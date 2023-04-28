using Infraestructure.Core.Repository;
using Infraestructure.Core.Repository.Interface;
using Infraestructure.Core.UnitOfWork;
using Infraestructure.Core.UnitOfWork.Interface;
using N5Now.Domain.Services.ElasticSearchs;
using N5Now.Domain.Services.ElasticSearchs.Interfaces;
using N5Now.Domain.Services.Kafka;
using N5Now.Domain.Services.Kafka.Interface;
using N5Now.Domain.Services.Permissions;
using N5Now.Domain.Services.Permissions.Interfaces;

namespace N5Now.Handlers
{
    public static class DependencyInyectionHandler
    {
        public static void DependencyInyectionConfig(IServiceCollection services)
        {
            // Repository await UnitofWork parameter ctor explicit
            services.AddScoped<CustomValidationFilterAttribute, CustomValidationFilterAttribute>();

            // Infrastructure
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IElasticsearchService, ElasticsearchService>();
            services.AddScoped<IKafkaServices, KafkaServices>();


            //Domain
            services.AddTransient<IPermissionServices, PermissionServices>();
            services.AddTransient<IPermissionTypeServices, PermissionTypeServices>();

        }
    }
}
