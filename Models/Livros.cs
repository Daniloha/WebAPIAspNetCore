using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiCadastro.Models
{
    [Table("livros")]
    public class Livros
    {   
        public int ID { get; set; }
        public string? Titulo { get; set; }
        public string? Autor { get; set; }
        public string? Genero { get; set; }
        public string? DataPublicacao { get; set; }
        public string? UF { get; set; }
        public string? Editora { get; set; }
        public int Quantidade { get; set; }
        public float Valor { get; set; }
    }
}
