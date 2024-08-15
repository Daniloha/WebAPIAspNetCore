using WebApiCadastro.Data.VO;

namespace WebApiCadastro.Business
{
    public interface IBookBusiness
    {
        LivrosVO Create(LivrosVO livro);
        LivrosVO FindByID(long id);
        LivrosVO Update(LivrosVO livro);
        void Delete(long id);
        List<LivrosVO> FindAll();
    }
}
