using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCadastro.Models;
using WebApiCadastro.Models.Context;
using WebApiCadastro.Repository.Implementations;
using Microsoft.EntityFrameworkCore;
using WebApiCadastro.Repository;



namespace WebApiCadastro.Buisness.Implementations
{
    public class PersonBuisnessImplementation : IPersonBuisness
    {
        // = new MySQLContext();
        
        private readonly IPersonRepository _repository;

        public PersonBuisnessImplementation( IPersonRepository repository)
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