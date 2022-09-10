using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using POC.HexagonalKafkaCache.Core.Domain.Entities;
using POC.HexagonalKafkaCache.Core.Ports.Out;
using POC.HexagonalKafkaCache.Core.Utils;

namespace POC.HexagonalKafkaCache.Infrastructure.SqlDapperRepository
{
    public class ClientRepositorySqlAdapter : IClientRepository
    {
        private readonly string ConnectionString;

        public ClientRepositorySqlAdapter(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [Cache(Seconds = 60 * 5, CacheKey = "GetALlClientsAsync")]
        public async Task<IEnumerable<Client>> GetALlClientsAsync()
        {
            var sql = "select * from Clients";
            using var connection = new SqlConnection(ConnectionString);
            return await connection.QueryAsync<Client>(sql);
        }

        [DeleteCache(CacheKey = "GetALlClientsAsync")]
        public async Task DeleteClientsAsync(string clientName)
        {
            using var connection = new SqlConnection(ConnectionString);
            await connection.ExecuteAsync(@"Delete from Clients where name = @name", clientName);
        }

        [DeleteCache(CacheKey = "GetALlClientsAsync")]
        public async Task SaveClientAsync(Client client)
        {
            using var connection = new SqlConnection(ConnectionString);
            await connection.ExecuteAsync(@"insert Clients(Name, BirthDate, Gender) values (@Name, @BirthDate, @Gender)", client);
        }
    }
}