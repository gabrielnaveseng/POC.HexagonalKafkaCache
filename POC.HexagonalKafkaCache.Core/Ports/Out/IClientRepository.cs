using POC.HexagonalKafkaCache.Core.Domain.Entities;

namespace POC.HexagonalKafkaCache.Core.Ports.Out
{
    public interface IClientRepository
    {
        Task SaveClientAsync(Client client);
        Task<IEnumerable<Client>> GetALlClientsAsync();
    }
}
