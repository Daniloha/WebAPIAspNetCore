using System;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiCadastro.Models.Base;

namespace WebApiCadastro.Models
{
    [Table("livros")]
    public class Livros : BaseEntity
    {
        [Column("Titulo")]
        public string? Titulo { get; set; }
        [Column("Autor")]
        public string? Autor { get; set; }
        [Column("Genero")]
        public string? Genero { get; set; }
        [Column("DataPublicacao")]
        public DateOnly DataPublicacao { get; set; }
        [Column("UF")]
        public string? UF { get; set; }
        [Column("Editora")]
        public string? Editora { get; set; }
        [Column("Quantidade")]
        public int Quantidade { get; set; }
        [Column("Valor")]
        public float Valor { get; set; }
    }
}
