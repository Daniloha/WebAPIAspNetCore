using WebApiCadastro.Models;
using WebApiCadastro.Repository;
using WebApiCadastro.Repository.Implementations;


namespace WebApiCadastro.Business.Implementations
{
    public class BookBusinessImplementation: IBookBusiness
    {
        // = new MySQLContext();

        private readonly IBookRepository _repository;

        public BookBusinessImplementation(IBookRepository repository)
        {
            _repository = repository;
        }

        public Livros Create(Livros livro)
        {

            return _repository.Create(livro);
        }

        public void Delete(long id)
        {

            _repository.Delete(id);

        }

        public List<Livros> FindAll()
        {

            return _repository.FindAll();
        }

        public Livros FindByID(long id)
        {
            return _repository.FindByID(id);

        }

        public Livros Update(Livros livro)
        {

            return _repository.Update(livro);
        }
    }
}
