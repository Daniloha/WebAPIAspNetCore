using WebApiCadastro.Models;

namespace WebApiCadastro.Business
{
    public interface IBookBusiness
    {
        Livros Create(Livros livro);
        Livros FindByID(long id);
        Livros Update(Livros livro);
        void Delete(long id);
        List<Livros> FindAll();
    }
}
