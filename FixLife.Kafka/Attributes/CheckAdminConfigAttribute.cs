using Confluent.Kafka;
using System;

namespace FixLife.Kafka.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CheckAdminConfigAttribute : Attribute
    {
        public CheckAdminConfigAttribute()
        {
            ValidateConfig(AdminConfigCache.Config);
        }

        private void ValidateConfig(AdminClientConfig? config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("Config wasn't initialized, before use method, call ApplyConfig()");
            }
        }

    }
}
