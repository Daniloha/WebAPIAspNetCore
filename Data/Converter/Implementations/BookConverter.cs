using WebApiCadastro.Data.Converter.Contract;
using WebApiCadastro.Data.VO;
using WebApiCadastro.Models;

namespace WebApiCadastro.Data.Converter.Implementations
{
    public class BookConverter : IParser<LivrosVO, Livros>, IParser<Livros, LivrosVO>
    {
        public Livros Parse(LivrosVO origin)
        {
            if (origin == null) return null;
            return new Livros
            {
                ID = origin.ID,
                Titulo = origin.Titulo,
                Autor = origin.Autor,
                Genero = origin.Genero,
                DataPublicacao = origin.DataPublicacao,
                UF = origin.UF,
                Editora = origin.Editora,
                Quantidade = origin.Quantidade,
                Valor = origin.Valor
            };
        }

        public LivrosVO Parse(Livros origin)
        {
            if (origin == null) return null;
            return new LivrosVO
            {
                ID = origin.ID,
                Titulo = origin.Titulo,
                Autor = origin.Autor,
                Genero = origin.Genero,
                DataPublicacao = origin.DataPublicacao,
                UF = origin.UF,
                Editora = origin.Editora,
                Quantidade = origin.Quantidade,
                Valor = origin.Valor
            };
        }

        public List<Livros> Parse(List<LivrosVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<LivrosVO> Parse(List<Livros> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
