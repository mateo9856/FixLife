using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.Kafka.Interfaces
{
    public interface IConsumer<T, T1>
    {
        void BuildConfig(string bootstrapServer, string groupId, string clientId, BrokerAddressFamily brokerAddressFamily);
        void SubscribeTopic(string topic);
        void Unsubscribe();
        T1 ConsumeValue();
    }
}
