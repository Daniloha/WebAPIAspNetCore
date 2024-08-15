using WebApiCadastro.Business;
using WebApiCadastro.Models;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using WebApiCadastro.Data.VO;

namespace WebApiCadastro.Controllers
{
    [ApiVersion("2.0")]// Versão da API    [ApiController]// Controlador
    [Route("api/[controller]/v{version:apiVersion}")]// Rota
    public class LivrosController : ControllerBase
    {
 
           //Logger de logs de erros
        private readonly ILogger<LivrosController> _logger;

        //Serviço de dados
        private IBookBusiness _bookBusiness;

        public LivrosController(ILogger<LivrosController> logger, IBookBusiness bookBusiness)
        {
            _logger = logger;
            _bookBusiness = bookBusiness;
        }

        [HttpGet]// Retorna todos os dados
        public IActionResult Get()
        {
            try
            {
                var livros = _bookBusiness.FindAll();
                return Ok(livros);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar dados: {ex.Message}");
            }
        }
        [HttpGet("{ID}")]// Retorna apenas um dado
        public IActionResult Get(long ID)
        {
            var livro = _bookBusiness.FindByID(ID);
            if (livro == null)
            {
                return NotFound();
            }
            return Ok(livro);
        }

        [HttpPost]// Cria um novo dado
        public IActionResult Post([FromBody] LivrosVO livro)
        {
            if (livro == null) return BadRequest();

            return Ok(_bookBusiness.Create(livro));
        }

        [HttpPut]// Atualiza um dado
        public IActionResult Put([FromBody] LivrosVO livro)
        {
            if (livro == null) return BadRequest();
            return Ok(_bookBusiness.Update(livro));
        }

        [HttpDelete("{ID}")]// Deleta um dado
        public IActionResult Delete(long ID)
        {
            if (ID <= 0) return BadRequest();

            _bookBusiness.Delete(ID);
            return NoContent();
        }
    }
    }

