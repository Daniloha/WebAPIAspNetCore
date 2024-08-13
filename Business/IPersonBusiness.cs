using System;
using System.Collections.Generic;
using WebApiCadastro.Models;


namespace WebApiCadastro.Business
{
    public interface IPersonBusiness
    {
        Pessoa Create(Pessoa pessoa);
        Pessoa FindByID(long id);
        Pessoa Update(Pessoa pessoa);
        void Delete(long id);
        List<Pessoa> FindAll();


    }
}