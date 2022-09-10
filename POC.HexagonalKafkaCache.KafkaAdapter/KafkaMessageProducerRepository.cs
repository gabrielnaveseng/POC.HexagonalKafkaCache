using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using POC.HexagonalKafkaCache.Core.Ports.Out;

namespace POC.HexagonalKafkaCache.Infrastructure.KafkaRepository
{
    public class KafkaMessageProducerRepository : IKafkaMessageProducerRepository
    {
        private readonly ProducerConfig ProducerConfig;

        public KafkaMessageProducerRepository(IConfiguration configuration)
        {
            ProducerConfig = new ProducerConfig
            {
                BootstrapServers = configuration.GetConnectionString("KafkaBootstrapServer")//,//"host1:9092,host2:9092",
                //ClientId = configuration.GetConnectionString("KafkaClientId")
            };
        }


        public async Task SendMessage(string topic, Object entityMessage)
        {
            using (var producer = new ProducerBuilder<Null, string>(ProducerConfig).Build())
            {
                var message = new Message<Null, string>()
                {
                    Value = JsonSerializer.Serialize(entityMessage)
                };

                await producer.ProduceAsync(new TopicPartition(topic, Partition.Any), message);
            }
        }
    }
}