using Confluent.Kafka;

namespace FixLife.KafkaLog
{
    public class Program
    {
        static void Main(string[] args)
        {
            System.Timers.Timer timer = new System.Timers.Timer(20000);
            timer.Enabled = true;
            timer.Elapsed += WriteCloseInfo;

            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                ClientId = "FixLife",
                GroupId = "default",
                BrokerAddressFamily = BrokerAddressFamily.V4,
                EnablePartitionEof = true,
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config)
                .SetErrorHandler(((_, e) => Console.WriteLine($"Detected error ! : Code : {e.Code}\n Reason: {e.Reason}")))
                .Build();
            consumer.Subscribe("FixLife-CreatePlanLogs");

            string key = "";

            while(key == Console.ReadLine())
            {
                var consumeResult = consumer.Consume();
                Console.WriteLine(consumeResult.Value);
            }

            consumer.Close();
        }

        private static void WriteCloseInfo(object? sender, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("Type any key to close!");
        }
    }

}