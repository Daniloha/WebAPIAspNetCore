using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiCadastro.Models
{
    [Table("pessoa")]// Nome da tabela
    public class Pessoa
    {
        public long ID { get; set; }
        public string? Nome { get; set; }
        public string? Sobrenome { get; set; }
        public string? Genero { get; set; }
        public DateOnly DataNascimento { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public string? Cpf { get; set; }
        public string? senha { get; set; }
    }
}
