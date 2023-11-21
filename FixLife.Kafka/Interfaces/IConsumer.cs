using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.Kafka.Interfaces
{
    public interface IConsumer
    {
        void BuildConfig(string bootstrapServer, string groupId, BrokerAddressFamily brokerAddressFamily);
        void SubscribeTopic(string topic);
        void Unsubscribe();
        Task ConsumeAsync();
    }
}
