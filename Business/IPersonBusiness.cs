using WebApiCadastro.Data.VO;


namespace WebApiCadastro.Business
{
    public interface IPersonBusiness
    {
        PessoaVO Create(PessoaVO pessoa);
        PessoaVO FindByID(long id);
        PessoaVO Update(PessoaVO pessoa);
        void Delete(long id);
        List<PessoaVO> FindAll();


    }
}