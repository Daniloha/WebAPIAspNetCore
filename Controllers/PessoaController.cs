using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using WebApiCadastro.Business;
using WebApiCadastro.Data.VO;
using WebApiCadastro.HyperMedia.Filters;

namespace WebApiCadastro.Controllers
{


    [ApiVersion("1")]// Vers�o da API    [ApiController]// Controlador
    [Route("api/[controller]/v{version:apiVersion}")]// Rota
    public class PessoaController : ControllerBase
    {
        //Logger de logs de erros
        private readonly ILogger<PessoaController> _logger;

        //Servi�o de dados
        private IPersonBusiness _personBuisness;

        public PessoaController(ILogger<PessoaController> logger, IPersonBusiness personBusiness)
        {
            _logger = logger;
            _personBuisness = personBusiness;
        }

        [HttpGet]// Retorna todos os dados
        [ProducesResponseType((200), Type = typeof(List<PessoaVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            try
            {
                var pessoas = _personBuisness.FindAll();
                return Ok(pessoas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar dados: {ex.Message}");
            }
        }
        [HttpGet("{ID}")]// Retorna apenas um dado
        [ProducesResponseType((200), Type = typeof(PessoaVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long ID)
        {
            var pessoa = _personBuisness.FindByID(ID);
            if (pessoa == null)
            {
                return NotFound();
            }
            return Ok(pessoa);
        }

        [HttpPost]// Cria um novo dado
        [ProducesResponseType((200), Type = typeof(PessoaVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] PessoaVO pessoa)
        {
            if (pessoa == null) return BadRequest();

            return Ok(_personBuisness.Create(pessoa));
        }

        [HttpPut]// Atualiza um dado
        [ProducesResponseType((200), Type = typeof(PessoaVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] PessoaVO pessoa)
        {
            if (pessoa == null) return BadRequest();
            return Ok(_personBuisness.Update(pessoa));
        }

        [HttpDelete("{ID}")]// Deleta um dado
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(long ID)
        {
            if (ID <= 0) return BadRequest();

            _personBuisness.Delete(ID);
            return NoContent();
        }
    }
}
