using Confluent.Kafka;
using System;
using System.Threading.Tasks;
using Kfk = Confluent.Kafka;

namespace FixLife.Kafka.Services
{
    public class ConsumerService<T, T1> : Interfaces.IConsumer<T, T1>
    {
        private ConsumerConfig _config = new ConsumerConfig();

        private Kfk.IConsumer<T, T1> _consumer;

        private string? DefaultErrorStr { get; set; }

        public void BuildConfig(string bootstrapServer, string groupId, string clientId, BrokerAddressFamily brokerAddressFamily)
        {
            _config.BootstrapServers = bootstrapServer;
            _config.GroupId = groupId;
            _config.ClientId = clientId;
            _config.BrokerAddressFamily = brokerAddressFamily;
            _config.EnablePartitionEof = true;

            _consumer = new ConsumerBuilder<T, T1>(_config)
                .SetErrorHandler(((_, e) => DefaultErrorValue(e.Code.ToString(), e.Reason)))
                .Build();
        }

        public T1 ConsumeValue()
        {
            var consumeValue = _consumer.Consume();

            if (consumeValue == null)
                throw new ArgumentNullException("Error with Consumer, method returns null.");

            return consumeValue.Message.Value;
        }

        public void SubscribeTopic(string topic)
        {
            _consumer.Subscribe(topic);
        }

        public void Unsubscribe()
        {
            _consumer.Unsubscribe();
        }

        private void DefaultErrorValue(string code, string reason)
            => DefaultErrorStr = $"Detected error ! : Code : {code}\n Reason: {reason}";
    }
}
