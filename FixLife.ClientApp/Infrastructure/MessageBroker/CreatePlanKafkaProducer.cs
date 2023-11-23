using Confluent.Kafka;
using Confluent.Kafka.Admin;
using FixLife.Kafka.Interfaces;
using Kfk = FixLife.Kafka.Interfaces;

namespace FixLife.ClientApp.Infrastructure.MessageBroker
{
    public class CreatePlanKafkaProducer : IDisposable
    {
        private ProducerConfig _config;
        private const string TopicName = "FixLife-CreatePlanLogs";

        private readonly Kfk.IAdminClientService _adminClientService;
        private readonly Kfk.IProducer<string, string> _producerService;

        public CreatePlanKafkaProducer(IAdminClientService adminClientService, Kfk.IProducer<string, string> producer)
        {
            _adminClientService = adminClientService;
            _producerService = producer;

            //TODO: Swap and test DI solutions
            _config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
                ClientId = "FixLife",
                BrokerAddressFamily = BrokerAddressFamily.V4,
            };
        }
        public async Task CreateMessage(string user = "", string plans = "")
        {
            try
            {
                await ValidateOrCreateTopic(TopicName);
                using var producer = new ProducerBuilder<string, string>(_config).Build();
                var message = new Message<string, string>()
                {
                    Key = "Plan create",
                    Value = string.Format("Value created by {0}, created plans: {1}", user, plans)
                };
                var deliver = await producer.ProduceAsync(TopicName, message);

            } catch (Exception ex)
            {
                return;
            }
        }

        private async Task ValidateOrCreateTopic(string topic)
        {
            var adminConfig = new AdminClientConfig
            {
                BootstrapServers = "localhost:9092"
            };
            using var adminClient = new AdminClientBuilder(adminConfig).Build();
            var metadata = adminClient.GetMetadata(TimeSpan.FromSeconds(10));
            var isHaveTopic = metadata.Topics.Any(d => d.Topic.Equals(topic));
            
            if (isHaveTopic) {
                return;
            }

            var newTopic = new TopicSpecification
            {
                Name = TopicName,
                NumPartitions = 1,
                ReplicationFactor = 1,
            };

            await adminClient.CreateTopicsAsync(new List<TopicSpecification>
            {
                newTopic,
            });
        }

        public void Dispose()
        {
            _config = null;
        }
    }
}
