using WebApiCadastro.Data.Converter.Implementations;
using WebApiCadastro.Data.VO;
using WebApiCadastro.Models;
using WebApiCadastro.Repository.Generic;


namespace WebApiCadastro.Business.Implementations
{
    public class BookBusinessImplementation: IBookBusiness
    {
        // = new MySQLContext();

        private readonly IRepository<Livros> _repository;
        private readonly BookConverter _converter;

        public BookBusinessImplementation(IRepository<Livros> repository)
        {
            _repository = repository;
            _converter = new BookConverter();     
        }

        public LivrosVO Create(LivrosVO livro)
        {

            var bookEntity = _converter.Parse(livro);
            bookEntity = _repository.Create(bookEntity);
            return _converter.Parse(bookEntity);
        }

        public void Delete(long id)
        {

            _repository.Delete(id);

        }

        public List<LivrosVO> FindAll()
        {

            return _converter.Parse(_repository.FindAll());
        }

        public LivrosVO FindByID(long id)
        {
            return _converter.Parse(_repository.FindByID(id));

        }

        public LivrosVO Update(LivrosVO livro)
        {

            var bookEntity = _converter.Parse(livro);
            bookEntity = _repository.Update(bookEntity);
            return _converter.Parse(bookEntity);
        }
    }
}
