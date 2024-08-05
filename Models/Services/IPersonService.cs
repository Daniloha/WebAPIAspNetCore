namespace WebApiCadastro.Models.Services
{
    public interface IPersonService
    {
        Pessoa Create(Pessoa Pessoa);
        Pessoa FindById(long id);
        List<Pessoa> FindAll();
        Pessoa Update(Pessoa Pessoa);
        void Delete(long id);
    }
}
