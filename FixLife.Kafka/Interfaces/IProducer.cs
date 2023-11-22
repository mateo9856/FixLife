using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.Kafka.Interfaces
{
    public interface IProducer<T, T1>
    {
        void BuildConfig(string bootstrapServer, string clientId, BrokerAddressFamily brokerAddressFamily);
        void CreateMessage(T key, T1 value);
        Task ProduceAsync(string topic);
    }
}
