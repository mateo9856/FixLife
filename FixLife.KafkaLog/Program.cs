using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Kfk = FixLife.Kafka;

namespace FixLife.KafkaLog
{
    public class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
            .AddSingleton<Kfk.Interfaces.IConsumer<Ignore, string>, Kfk.Services.ConsumerService<Ignore, string>>()
            .BuildServiceProvider();

            System.Timers.Timer timer = new System.Timers.Timer(20000);
            timer.Enabled = true;
            timer.Elapsed += WriteCloseInfo;

            var consumerService = serviceProvider
            .GetService<Kfk.Interfaces.IConsumer<Ignore, string>>();

            var consumer = new ConsumerBuilder(consumerService);
            consumer.StartProduce();

            string key = "";

            while(key == Console.ReadLine())
            {
                var consumeResult = consumer.ConsumeValue();
                Console.WriteLine(consumeResult.Value);
            }

            consumer.StopProduce();
        }

        private static void WriteCloseInfo(object? sender, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("Type any key to close!");
        }
    }

}