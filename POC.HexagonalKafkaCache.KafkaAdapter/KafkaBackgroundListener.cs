using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace POC.HexagonalKafkaCache.Infrastructure.KafkaRepository
{
    public class KafkaBackgroundListener : IHostedService
    {
        private Timer? _timer = null;
        private int executionCount = 0;
        private readonly ProducerConfig ProducerConfig;

        public KafkaBackgroundListener(IConfiguration configuration)
        {
            ProducerConfig = new ProducerConfig(new Dictionary<string, string>() { 
                { "group.id", "newConsumerFromBegin" }, 
                { "auto.offset.reset", "earliest" } 
            });
            ProducerConfig.BootstrapServers = configuration.GetConnectionString("KafkaBootstrapServer"); 
            ProducerConfig.ClientId = "myClientId";
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));

            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            var count = Interlocked.Increment(ref executionCount);

            Console.WriteLine($"Timed Hosted Service is working. Count: {count}");

            using (var consumer = new ConsumerBuilder<Ignore, string>(ProducerConfig).Build())
            {
                var partitions = new TopicPartition[] { 
                    new TopicPartition("poc-topic", new Partition(0)),
                    new TopicPartition("poc-topic", new Partition(1)),
                    new TopicPartition("poc-topic", new Partition(2)) 
                };

                consumer.Assign(partitions);
                
                var consumeResult = consumer.Consume(TimeSpan.FromSeconds(1));

                if(consumeResult is not null)
                    Console.WriteLine($"Message result: {consumeResult.Message.Value}");

                consumer.Close();
            }
        }
        public Task StopAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Timed Hosted Service is stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}
