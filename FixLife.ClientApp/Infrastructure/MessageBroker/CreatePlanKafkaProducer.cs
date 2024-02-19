using Confluent.Kafka;
using Confluent.Kafka.Admin;
using FixLife.Kafka.Interfaces;
using Kfk = FixLife.Kafka.Interfaces;

namespace FixLife.ClientApp.Infrastructure.MessageBroker
{
    public class CreatePlanKafkaProducer
    {
        private const string TopicName = "FixLife-CreatePlanLogs";
        private const string BootstapServer = "localhost:9092";

        private readonly Kfk.IAdminClientService _adminClientService;
        private readonly Kfk.IProducer<string, string> _producerService;

        public CreatePlanKafkaProducer(IAdminClientService adminClientService, Kfk.IProducer<string, string> producer)
        {
            _adminClientService = adminClientService;
            _producerService = producer;

            _adminClientService.ApplyConfig(new AdminClientConfig
            {
                BootstrapServers = BootstapServer,
                ApiVersionRequestTimeoutMs = 2500,    
            });

            _producerService.BuildConfig(BootstapServer, "FixLife", BrokerAddressFamily.V4);

        }
        public async Task CreateMessage(string user = "", string plans = "")
        {
            var getTopic = await _adminClientService.GetOrCreateTopic(TopicName);

            if (getTopic == null)
                throw new Exception($"Topic: {TopicName} not found!");

            _producerService.CreateMessage(user, plans);

            await _producerService.ProduceAsync(TopicName);
        }
    }
}
