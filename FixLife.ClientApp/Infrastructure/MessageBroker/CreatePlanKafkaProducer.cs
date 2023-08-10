using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.ClientApp.Infrastructure.MessageBroker
{
    public class CreatePlanKafkaProducer : IDisposable
    {
        private ProducerConfig _config;

        public CreatePlanKafkaProducer()
        {
            _config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
                ClientId = "FixLife",
                BrokerAddressFamily = BrokerAddressFamily.V4
            };
        }

        public async Task CreateMessage(string user = "", string plans = "")
        {
            try
            {
                using var producer = new ProducerBuilder<string, string>(_config).Build();
                var message = new Message<string, string>()
                {
                    Key = "Plan create",
                    Value = string.Format("Value created by {0}, created plans: {1}", user, plans)
                };
                var deliver = await producer.ProduceAsync("logs-topic", message);
            } catch (Exception ex)
            {

            }
        }

        public void Dispose()
        {
            _config = null;
        }
    }
}
