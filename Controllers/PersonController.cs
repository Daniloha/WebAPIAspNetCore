using Microsoft.AspNetCore.Mvc;
using WebApiCadastro.Models;
using WebApiCadastro.Models.Services;

namespace WebApiCadastro.Controllers
{
    [ApiVersion("1.0")]//Versão da API
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]//Rota 
    public class PersonController : ControllerBase
    {
        //Logger de erros
        private readonly ILogger<PersonController> _logger;
        //Serviço de dados
        private IPersonService _personService;

        public PersonController(ILogger<PersonController> logger, IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Pessoa pessoa)
        {
            if(pessoa == null)
            {
                return BadRequest();
            }
            return Ok(_personService.Create(pessoa));
        }

        [HttpGet("{ID}")]
        public IActionResult FindByID(long ID)
        {
            var pessoa = _personService.FindById(ID);
            if (pessoa == null)
            {
                return NotFound();
            }
            return Ok(pessoa);
        }
        [HttpGet]
        public IActionResult FindAll()
        {
            try
            {
                var pessoas = _personService.FindAll();
                return Ok(pessoas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar dados: {ex.Message}");
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] Pessoa pessoa)
        {
            if (pessoa == null) return BadRequest();
            return Ok(_personService.Update(pessoa));
        }

        [HttpDelete("{ID}")]
        public IActionResult Delete(long ID)
        {
            if (ID <= 0) return BadRequest();

            _personService.Delete(ID);
            return NoContent();
        }

    }
}
