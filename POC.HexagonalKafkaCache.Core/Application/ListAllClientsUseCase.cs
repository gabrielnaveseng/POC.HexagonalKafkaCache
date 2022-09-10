using AutoMapper;
using POC.HexagonalKafkaCache.Core.Ports.In.Queries;
using POC.HexagonalKafkaCache.Core.Ports.Out;

namespace POC.HexagonalKafkaCache.Core.Application
{
    public class ListAllClientsUseCase : IListAllClientsUseCase
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ListAllClientsUseCase(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ListAllClientsDtoResponse>> GetAllClients()
        {
            var clients = await _clientRepository.GetALlClientsAsync();
            
            //clients.Select(v => _mapper.Map<ListAllClientsDtoResponse>(v))
            return _mapper.Map<IEnumerable<ListAllClientsDtoResponse>>(clients);
        }
    }
}
