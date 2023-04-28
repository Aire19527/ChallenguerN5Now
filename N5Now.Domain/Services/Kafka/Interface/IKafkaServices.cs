using N5Now.Domain.DTO;

namespace N5Now.Domain.Services.Kafka.Interface
{
    public interface IKafkaServices
    {
        Task<bool> SendServices(ServiceKafkaDto service);
    }
}
