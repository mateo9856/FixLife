using Confluent.Kafka;
using FixLife.Kafka.Services;
using FluentAssertions;
using Moq;
using Kfk = FixLife.Kafka.Interfaces;
using KfkImp = FixLife.Kafka.Services;

namespace FixLife.ApiTest.Kafka
{
    public class KafkaConsumerTest : IDisposable
    {
        private Kfk.IConsumer<Ignore, string> _consumer;

        private const string BootstrapServer = "localhost:9092";
        private const string TopicName = "FixLife-CreatePlanLogs";

        private void SetUp()
        {
            _consumer = new KfkImp.ConsumerService<Ignore, string>();
            _consumer.BuildConfig(BootstrapServer, "default", "FixLife", BrokerAddressFamily.V4);
            _consumer.SubscribeTopic(TopicName);
        }

        [Fact]
        public void Consume_ReturnsOk()
        {
            SetUp();

            var val = _consumer.ConsumeValue();

            val.Should().NotBeNull();
           
        }

        public void Dispose()
        {
            _consumer.Unsubscribe();
        }
    }
}
