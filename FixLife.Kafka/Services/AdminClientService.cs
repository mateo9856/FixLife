using Confluent.Kafka;
using Confluent.Kafka.Admin;
using FixLife.Kafka.Attributes;
using FixLife.Kafka.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FixLife.Kafka.Services
{
    public class AdminClientService : IAdminClientService
    {
        private IAdminClient? _adminClient;

        public void ApplyConfig(AdminClientConfig config)
        {
            AdminConfigCache.Config = config;
            _adminClient = new AdminClientBuilder(config).Build();
        }

        [CheckAdminConfig]
        public async Task DeleteTopic(string topicName)
           => await _adminClient.DeleteTopicsAsync(new[] {topicName}.ToList());

        [CheckAdminConfig]
        public async Task<TopicMetadata> GetOrCreateTopic(string topicName)
        {
            var topicMetadata = _adminClient.GetMetadata(TimeSpan.FromSeconds(15));

            if(!topicMetadata.Topics.Any(d => d.Topic.Equals(topicName))) {

                var topicList = new List<TopicSpecification>();

                topicList.Add(new TopicSpecification
                {
                    Name = topicName,
                    NumPartitions = 1,
                    ReplicationFactor = 1,
                });

                await _adminClient.CreateTopicsAsync(topicList);

            }

            return topicMetadata.Topics.Single(a => a.Topic.Equals(topicName));

        }
    }
}
