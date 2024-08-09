using System;
using System.Collections.Generic;
using WebApiCadastro.Models;


namespace WebApiCadastro.Repository
{
    public interface IPersonRepository
    {
        Pessoa Create(Pessoa pessoa);
        Pessoa FindByID(long id);
        Pessoa Update(Pessoa pessoa);
        void Delete(long id);
        List<Pessoa> FindAll();
        bool Exists(long iD);


    }
}