using POC.HexagonalKafkaCache.Core.Domain.Entities;
using POC.HexagonalKafkaCache.Core.Ports.In.Commands;
using POC.HexagonalKafkaCache.Core.Ports.Out;

namespace POC.HexagonalKafkaCache.Core.Application
{
    public class SaveClientUseCase : ISaveClientUseCase
    {
        private readonly IClientRepository _clientRepository;
        private readonly IKafkaMessageProducerRepository _kafkaRepository;

        public SaveClientUseCase(IClientRepository clientRepository, IKafkaMessageProducerRepository kafkaRepository)
        {
            _clientRepository = clientRepository;
            _kafkaRepository = kafkaRepository;
        }

        public async Task SaveClient(SaveClientCommand command)
        {
            Client client = new(command.Name, command.BirthDate, command.Gender);
            await _clientRepository.SaveClientAsync(client);
            await _kafkaRepository.SendMessage("poc-topic", client);
        }
    }
}
