using Microsoft.EntityFrameworkCore;
using WebApiCadastro.Models.Base;
using WebApiCadastro.Models.Context;

namespace WebApiCadastro.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        // = new MySQLContext();

        private MySQLContext _context;
        private DbSet<T> _dataset;

        public GenericRepository(MySQLContext context)
        {
            _context = context;
            _dataset = _context.Set<T>();
        }
        T IRepository<T>.Create(T item)
        {
            try
            {
                _dataset.Add(item);
                _context.SaveChanges();
                return item;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        public List<T> FindAll()
        {
            return _dataset.ToList();
        }

        public T FindByID(long id)
        {
            return _dataset.SingleOrDefault(p => p.ID.Equals(id));
        }

        public T Update(T item)
        {
            var result = _dataset.SingleOrDefault(p => p.ID.Equals(item.ID));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(item);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            else
            {
                return null;
            }
        }  
        public void Delete(long id)
        {
            var result = _dataset.SingleOrDefault(p => p.ID.Equals(id));
            if (result != null)
            {
                try
                {
                    _dataset.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }      
        public bool Exists(long iD)
        {
            return _dataset.Any(p => p.ID.Equals(iD));
        }
    }
}
