using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiCadastro.Models.Base;

namespace WebApiCadastro.Models
{
    [Table("pessoa")]// Nome da tabela
    public class Pessoa : BaseEntity
    {
        [Column("Nome")]
        public string? Nome { get; set; }
        [Column("Sobrenome")]
        public string? Sobrenome { get; set; }
        [Column("Genero")]
        public string? Genero { get; set; }
        [Column("DataNascimento")]
        public DateOnly DataNascimento { get; set; }
        [Column("Email")]
        public string? Email { get; set; }
        [Column("Telefone")]
        public string? Telefone { get; set; }
        [Column("Cpf")]
        public string? Cpf { get; set; }
        [Column("Senha")]
        public string? senha { get; set; }
    }
}
