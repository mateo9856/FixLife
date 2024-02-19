using Confluent.Kafka;
using FixLife.Kafka.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.Kafka.Services
{
    public class ProducerService<T, T1> : Interfaces.IProducer<T, T1>
    {
        private ProducerConfig _config = new ProducerConfig();

        private Confluent.Kafka.IProducer<T, T1> _producer;

        private Message<T, T1> _message = new Message<T, T1>();

        public void BuildConfig(string bootstrapServer, string clientId, BrokerAddressFamily brokerAddressFamily)
        {
            _config.BootstrapServers = bootstrapServer;
            _config.ClientId = clientId;
            _config.BrokerAddressFamily = brokerAddressFamily;

            _producer = new ProducerBuilder<T, T1>(_config).Build();
        }

        public void CreateMessage(T key, T1 value)
        {
            _message.Key = key;
            _message.Value = value;
        }

        public async Task ProduceAsync(string topic) 
            => await _producer.ProduceAsync(topic, _message);
    }
}
