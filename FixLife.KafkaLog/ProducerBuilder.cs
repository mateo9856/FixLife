using Confluent.Kafka;
using Kfk = FixLife.Kafka.Interfaces;

namespace FixLife.KafkaLog
{
    public class ProducerBuilder
    {
        private readonly Kfk.IConsumer<Ignore, string> _consumer;

        private const string BootstrapServer = "localhost:9092";
        private const string TopicName = "FixLife-CreatePlanLogs";

        public ProducerBuilder(Kfk.IConsumer<Ignore, string> consumer)
        {
            _consumer = consumer;
            _consumer.BuildConfig(BootstrapServer, "default", "FixLife", BrokerAddressFamily.V4);
        }

        public void StartProduce()
        {
            _consumer.SubscribeTopic(TopicName);
        }

        public void StopProduce()
        {
            _consumer.Unsubscribe();
        }
    }
}
