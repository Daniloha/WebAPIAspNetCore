using WebApiCadastro.Models;
using WebApiCadastro.Repository.Generic;
using WebApiCadastro.Data.VO;
using WebApiCadastro.Data.Converter.Implementations;



namespace WebApiCadastro.Business.Implementations
{
    public class PersonBuisnessImplementation : IPersonBusiness
    {
        // = new MySQLContext();
        
        private readonly IRepository<Pessoa> _repository;
        private readonly PersonConverter _converter;

        public PersonBuisnessImplementation(IRepository<Pessoa> repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public PessoaVO Create(PessoaVO pessoa)
        {
            var personEntity = _converter.Parse(pessoa);
            personEntity = _repository.Create(personEntity);
            return _converter.Parse(personEntity);
        }

        public void Delete(long id)
        {

            _repository.Delete(id);
            
        }

        public List<PessoaVO> FindAll()
        {
      
            return    _converter.Parse(_repository.FindAll());
        }

        public PessoaVO FindByID(long id)
        {
            return _converter.Parse(_repository.FindByID(id));
        }

        public PessoaVO Update(PessoaVO pessoa)
        {

            var personEntity = _converter.Parse(pessoa);
            personEntity = _repository.Update(personEntity);
            return _converter.Parse(personEntity);
        }
    }
}