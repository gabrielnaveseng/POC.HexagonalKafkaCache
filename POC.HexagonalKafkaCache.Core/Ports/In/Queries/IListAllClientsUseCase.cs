namespace POC.HexagonalKafkaCache.Core.Ports.In.Queries
{
    public interface IListAllClientsUseCase
    {
        Task<IEnumerable<ListAllClientsDtoResponse>> GetAllClients();
    }
}
