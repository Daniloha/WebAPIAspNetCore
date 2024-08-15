using WebApiCadastro.Data.Converter.Contract;
using WebApiCadastro.Data.VO;
using WebApiCadastro.Models;

namespace WebApiCadastro.Data.Converter.Implementations
{
    public class PersonConverter : IParser<PessoaVO, Pessoa>, IParser<Pessoa, PessoaVO>
    {
        public Pessoa Parse(PessoaVO origin)
        {
            if (origin == null) return null;
            return new Pessoa
            {
                ID = origin.ID,
                Nome = origin.Nome,
                Sobrenome = origin.Sobrenome,
                Genero = origin.Genero,
                DataNascimento = origin.DataNascimento,
                Email = origin.Email,
                Cpf = origin.Cpf,
                senha = origin.senha
            };
                 
        }

        public PessoaVO Parse(Pessoa origin)
        {
            if (origin == null) return null;
            return new PessoaVO
            {
                ID = origin.ID,
                Nome = origin.Nome,
                Sobrenome = origin.Sobrenome,
                Genero = origin.Genero,
                DataNascimento = origin.DataNascimento,
                Email = origin.Email,
                Cpf = origin.Cpf,
                senha = origin.senha
            };
        }

        public List<Pessoa> Parse(List<PessoaVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
                
        }

        public List<PessoaVO> Parse(List<Pessoa> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
