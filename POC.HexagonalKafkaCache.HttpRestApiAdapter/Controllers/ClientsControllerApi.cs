using Microsoft.AspNetCore.Mvc;
using POC.HexagonalKafkaCache.Core.Ports.In.Commands;
using POC.HexagonalKafkaCache.Core.Ports.In.Queries;

namespace POC.HexagonalKafkaCache.Controllers
{
    [Route("api/v1/clients")]
    [ApiController]
    public class ClientsControllerApi : ControllerBase
    {
        private readonly IListAllClientsUseCase _listAllClientsUseCase;
        private readonly ISaveClientUseCase _saveClientUseCase;

        public ClientsControllerApi(IListAllClientsUseCase listAllClientsUseCase, 
            ISaveClientUseCase saveClientUseCase)
        {
            _listAllClientsUseCase = listAllClientsUseCase;
            _saveClientUseCase = saveClientUseCase;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListAllClientsDtoResponse>>> Get()
        {
            return Ok(await _listAllClientsUseCase.GetAllClients());
        }

        [HttpPost]
        public async Task<ActionResult> Post(SaveClientCommand command)
        {
            await _saveClientUseCase.SaveClient(command);
            return Created(string.Empty, command);
        }
    }
}
