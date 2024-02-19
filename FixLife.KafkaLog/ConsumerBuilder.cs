using Confluent.Kafka;
using Kfk = FixLife.Kafka.Interfaces;

namespace FixLife.KafkaLog
{
    public class ConsumerBuilder
    {
        private readonly Kfk.IConsumer<Ignore, string> _consumer;

        private bool _isSubscribeStarted;

        private const string BootstrapServer = "localhost:9092";
        private const string TopicName = "FixLife-CreatePlanLogs";

        public ConsumerBuilder(Kfk.IConsumer<Ignore, string> consumer)
        {
            _consumer = consumer;
            _consumer.BuildConfig(BootstrapServer, "default", "FixLife", BrokerAddressFamily.V4);
        }

        public void StartProduce()
        {
            _consumer.SubscribeTopic(TopicName);
            _isSubscribeStarted = true;
        }

        public string ConsumeValue()
        {
            if (!_isSubscribeStarted)
                return string.Empty;

            return _consumer.ConsumeValue();
        }

        public void StopProduce()
        {
            if (_isSubscribeStarted)
                _consumer.Unsubscribe();
        }
    }
}
