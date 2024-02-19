using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Text;

namespace FixLife.Kafka
{
    public class AdminConfigCache
    {
        public static AdminClientConfig Config { get; set; }
    }
}
