using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using N5Now.Domain.DTO;
using N5Now.Domain.Services.Kafka.Interface;
using Serilog;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace N5Now.Domain.Services.Kafka
{
    public class KafkaServices : IKafkaServices
    {
        #region attributes
        private readonly IConfiguration _config;
        #endregion

        #region Builder
        public KafkaServices(IConfiguration configuration)
        {
            _config = configuration;
        }
        #endregion


        #region Methods
        public async Task<bool> SendServices(ServiceKafkaDto service)
        {
            string bootstrapServers = _config.GetSection("KafkaProducer:BootstrapServers").Value;
            string topic = _config.GetSection("KafkaProducer:Topic").Value;
            string message = JsonSerializer.Serialize(service);

            ProducerConfig config = new ProducerConfig
            {
                BootstrapServers = bootstrapServers,
                ClientId = Dns.GetHostName()
            };

            try
            {
                using (var producer = new ProducerBuilder<Null, string>(config).Build())
                {
                    var result = await producer.ProduceAsync(topic, new Message<Null, string>
                    {
                        Value = message
                    });

                    Debug.WriteLine($"Delivery Timestamp:{result.Timestamp.UtcDateTime}");
                    return await Task.FromResult(true);
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, $"Error MessageKafka - Producer: {ex.Message}");
                Console.WriteLine($"Error occured: {ex.Message}");
            }

            return await Task.FromResult(false);
        }
        #endregion
    }
}
