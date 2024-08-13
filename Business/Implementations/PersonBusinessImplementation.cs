using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCadastro.Models;
using WebApiCadastro.Models.Context;
using Microsoft.EntityFrameworkCore;
using WebApiCadastro.Repository;
using WebApiCadastro.Repository.Generic;



namespace WebApiCadastro.Business.Implementations
{
    public class PersonBuisnessImplementation : IPersonBusiness
    {
        // = new MySQLContext();
        
        private readonly IRepository<Pessoa> _repository;

        public PersonBuisnessImplementation(IRepository<Pessoa> repository)
        {
            _repository = repository;
        }

        public Pessoa Create(Pessoa pessoa)
        {
        
            return _repository.Create(pessoa);
        }

        public void Delete(long id)
        {

            _repository.Delete(id);
            
        }

        public List<Pessoa> FindAll()
        {
      
            return    _repository.FindAll();
        }

        public Pessoa FindByID(long id)
        {
            return    _repository.FindByID(id);

        }

        public Pessoa Update(Pessoa pessoa)
        { 
           
            return    _repository.Update(pessoa);
        }
    }
}