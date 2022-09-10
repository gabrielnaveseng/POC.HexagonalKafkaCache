using AutoMapper;
using Moq;
using POC.HexagonalKafkaCache.Core.Application;
using POC.HexagonalKafkaCache.Core.Domain.Entities;
using POC.HexagonalKafkaCache.Core.Domain.Enum;
using POC.HexagonalKafkaCache.Core.Ports.In.Queries;
using POC.HexagonalKafkaCache.Core.Ports.Out;

namespace POC.HexagonalKafkaCache.UnitTests
{
    public class UnitTest1
    {
        private readonly ListAllClientsUseCase _listAllClientsUseCase;
        private readonly Mock<IClientRepository> _clientRepository;
        private readonly Mock<IMapper> _mapper;


        public UnitTest1()
        {
            _clientRepository = new();
            _mapper = new();
            _listAllClientsUseCase = new ListAllClientsUseCase(_clientRepository.Object, _mapper.Object);
        }

        [Fact]
        public async Task GetAllClients_WhenHaveNoClients_ResultinInEmptyCollection()
        {
            _clientRepository.Setup(c => c.GetALlClientsAsync()).ReturnsAsync(new List<Client>());
            _mapper.Setup(c => c.Map<IEnumerable<ListAllClientsDtoResponse>>(It.IsAny<object>())).Returns(new List<ListAllClientsDtoResponse>());
            
            var result = await _listAllClientsUseCase.GetAllClients();

            Assert.Empty(result);
            _clientRepository.Verify(c => c.GetALlClientsAsync(), Times.Once);
            _mapper.Verify(c => c.Map<IEnumerable<ListAllClientsDtoResponse>>(It.IsAny<object>()), Times.Once);
        }

        [Fact]
        public async Task GetAllClients_WhenHaveClients_ResultACollection()
        {
            _clientRepository.Setup(c => c.GetALlClientsAsync()).ReturnsAsync(new List<Client>() { MockClient() });
            _mapper.Setup(c => c.Map<IEnumerable<ListAllClientsDtoResponse>>(It.IsAny<object>())).Returns(new List<ListAllClientsDtoResponse>() { MockClientsDto() });

            var result = await _listAllClientsUseCase.GetAllClients();

            Assert.Single(result);
            _clientRepository.Verify(c => c.GetALlClientsAsync(), Times.Once);
            _mapper.Verify(c => c.Map<IEnumerable<ListAllClientsDtoResponse>>(It.IsAny<object>()), Times.Once);
        }

        private Client MockClient() => new Client("Name", DateTime.Now, Gender.NON_BINARY);
        private ListAllClientsDtoResponse MockClientsDto() => new ListAllClientsDtoResponse("Name", DateTime.Now, Gender.NON_BINARY);
    }
}