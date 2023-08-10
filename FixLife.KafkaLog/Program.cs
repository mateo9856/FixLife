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
                BrokerAddressFamily = BrokerAddressFamily.V4
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe("plan-logs");

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