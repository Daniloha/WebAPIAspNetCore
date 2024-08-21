using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using WebApiCadastro.Business;
using WebApiCadastro.Data.VO;
using WebApiCadastro.HyperMedia.Filters;
using WebApiCadastro.Models;

namespace WebApiCadastro.Controllers
{


    [ApiVersion("1")]// Versão da API    [ApiController]// Controlador
    [Route("api/[controller]/v{version:apiVersion}")]// Rota
    public class PessoaController : ControllerBase
    {
        //Logger de logs de erros
        private readonly ILogger<PessoaController> _logger;

        //Serviço de dados
        private IPersonBusiness _personBuisness;

        public PessoaController(ILogger<PessoaController> logger, IPersonBusiness personBusiness)
        {
            _logger = logger;
            _personBuisness = personBusiness;
        }

        [HttpGet]// Retorna todos os dados
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
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] PessoaVO pessoa)
        {
            if (pessoa == null) return BadRequest();

            return Ok(_personBuisness.Create(pessoa));
        }

        [HttpPut]// Atualiza um dado
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] PessoaVO pessoa)
        {
            if (pessoa == null) return BadRequest();
            return Ok(_personBuisness.Update(pessoa));
        }

        [HttpDelete("{ID}")]// Deleta um dado
        public IActionResult Delete(long ID)
        {
            if (ID <= 0) return BadRequest();

            _personBuisness.Delete(ID);
            return NoContent();
        }
    }
}
