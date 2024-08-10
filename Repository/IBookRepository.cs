using WebApiCadastro.Models;

namespace WebApiCadastro.Repository
{
    public interface IBookRepository
    {
        Livros Create(Livros livro);
        Livros FindByID(long id);
        Livros Update(Livros livro);
        void Delete(long id);
        List<Livros> FindAll();
        bool Exists(long iD);
    }
}
