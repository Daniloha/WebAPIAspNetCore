using System.ComponentModel.DataAnnotations.Schema;
using WebApiCadastro.HyperMedia;
using WebApiCadastro.HyperMedia.Abstract;


namespace WebApiCadastro.Data.VO
{
    [Table("livros")]
    public class LivrosVO : ISupportHiperMedia
    {
    
        public long ID { get; set; }
        public string? Titulo { get; set; }
        public string? Autor { get; set; }
        public string? Genero { get; set; }
        public DateOnly DataPublicacao { get; set; }
        public string? UF { get; set; }
        public string? Editora { get; set; }
        public int Quantidade { get; set; }
        public float Valor { get; set; }
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
