using Confluent.Kafka;
using System.Threading.Tasks;

namespace FixLife.Kafka.Interfaces
{
    public interface IAdminClientService
    {
        Task DeleteTopic(string topicName);
        void ApplyConfig(AdminClientConfig config);
        Task<TopicMetadata> GetOrCreateTopic(string topicName);
    }
}
