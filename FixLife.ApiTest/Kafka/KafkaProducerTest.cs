using Confluent.Kafka;
using FluentAssertions;
using Moq;
using Kfk = FixLife.Kafka.Interfaces;
using KfkImp = FixLife.Kafka.Services;

namespace FixLife.ApiTest.Kafka
{
    public class KafkaProducerTest
    {
        private const string TopicName = "FixLife-CreatePlanLogs";
        private const string BootstapServer = "localhost:9092";

        private Kfk.IAdminClientService _adminClientService;
        private Kfk.IProducer<string, string> _producerService;

        private void SetUp()
        {
            _adminClientService = new KfkImp.AdminClientService();
            _producerService = new KfkImp.ProducerService<string, string>();

            _adminClientService.ApplyConfig(new AdminClientConfig
            {
                BootstrapServers = BootstapServer,
                ApiVersionRequestTimeoutMs = 2500,
            });

            _producerService.BuildConfig(BootstapServer, "FixLife", BrokerAddressFamily.V4);
        }

        [Fact]
        public async Task CreateMessage_ShouldReturnOK()
        {
            SetUp();

            string user = new Mock<string>().Object;
            string plans = new Mock<string>().Object;

            var getTopic = await _adminClientService.GetOrCreateTopic(TopicName);

            _producerService.CreateMessage(user, plans);

            await _producerService.ProduceAsync(TopicName);

            getTopic.Should().NotBeNull();
        }

    }
}
