namespace POC.HexagonalKafkaCache.Core.Ports.In.Commands
{
    public interface ISaveClientUseCase
    {
        Task SaveClient(SaveClientCommand command);
    }
}
