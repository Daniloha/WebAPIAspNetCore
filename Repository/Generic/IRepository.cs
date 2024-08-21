using System;
using System.Collections.Generic;
using WebApiCadastro.Models;
using WebApiCadastro.Models.Base;
using WebApiCadastro.Models.Context;


namespace WebApiCadastro.Repository.Generic
{
    public interface IRepository<T> where T : BaseEntity
    {

        T Create(T item);
        T FindByID(long id);
        T Update(T item);
        void Delete(long id);
        List<T> FindAll();
        bool Exists(long iD);


    }
}