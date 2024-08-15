using System;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiCadastro.Models.Base;

namespace WebApiCadastro.Models
{
    [Table("livros")]
    public class Livros : BaseEntity
    {
        public string? Titulo { get; set; }
        public string? Autor { get; set; }
        public string? Genero { get; set; }
        public DateOnly DataPublicacao { get; set; }
        public string? UF { get; set; }
        public string? Editora { get; set; }
        public int Quantidade { get; set; }
        public float Valor { get; set; }
    }
}
