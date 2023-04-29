using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No5Now.Test
{
    public static class ConfigurationMock
    {
        public static Dictionary<string, string> ConfigAppSetting()
        {
            Dictionary<string, string> appSettingTest = new Dictionary<string, string>
            {
                { "KafkaProducer:Topic", "TopicN5Now"},
                { "BootstrapServers:Topic", "localhost:9092"},

                { "ElasticsearchServer:Host", "http://localhost"},
                { "ElasticsearchServer:Port", "9200"},
                { "ElasticsearchServer:PermisionIndex", "permissions"},
            };

            return appSettingTest;
        }
    }
}
