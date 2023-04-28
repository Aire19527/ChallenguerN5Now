using Confluent.Kafka;
using Kafka.Consumer.Models;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Text.Json;

namespace Kafka.Consumer
{
    public class ApacheKafkaConsumerDemo
    {
        public class ApacheKafkaConsumerService : IHostedService
        {
            #region Attributes         
            private readonly IConfiguration _config;
            #endregion

            #region Builder
            public ApacheKafkaConsumerService(IConfiguration configuration)
            {
                _config = configuration;
            }
            #endregion


            public Task StartAsync(CancellationToken cancellationToken)
            {

                string topic = _config.GetSection("KafkaConsumer:Topic").Value;
                string bootstrapServers = _config.GetSection("KafkaConsumer:BootstrapServers").Value;


                var config = new ConsumerConfig
                {
                    GroupId = $"{topic}_group",
                    BootstrapServers = bootstrapServers,
                    AutoOffsetReset = AutoOffsetReset.Earliest,
                    //SaslMechanism = SaslMechanism.Plain,
                    //SecurityProtocol = SecurityProtocol.SaslSsl
                };

                try
                {
                    using (var consumerBuilder = new ConsumerBuilder<Ignore, string>(config).Build())
                    {
                        consumerBuilder.Subscribe(topic);
                        var cancelToken = new CancellationTokenSource();

                        try
                        {
                            while (true)
                            {
                                var consumer = consumerBuilder.Consume(cancelToken.Token);
                                if (consumer.Message != null)
                                {
                                    var serviceKafka = JsonSerializer.Deserialize<ServiceKafkaDto>(consumer.Message.Value);
                                    Debug.WriteLine($"Processing Order Id: {serviceKafka.Id}");
                                }
                            }
                        }
                        catch (OperationCanceledException)
                        {
                            consumerBuilder.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }

                return Task.CompletedTask;
            }
            public Task StopAsync(CancellationToken cancellationToken)
            {
                return Task.CompletedTask;
            }
        }
    }
}
