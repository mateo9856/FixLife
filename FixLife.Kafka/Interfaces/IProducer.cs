using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.Kafka.Interfaces
{
    public interface IProducer
    {
        void BuildConfig(string bootstrapServer, string groupId, BrokerAddressFamily brokerAddressFamily);
        void CreateMessage<T, T1>(T key, T1 value);
        Task ProduceAsync();
    }
}
