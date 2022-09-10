namespace POC.HexagonalKafkaCache.Core.Ports.Out
{
    public interface IKafkaMessageProducerRepository
    {
        Task SendMessage(string topic, object entityMessage);
    }
}
