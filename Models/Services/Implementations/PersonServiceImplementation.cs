using WebApiCadastro.Models.Context;
using WebApiCadastro.Models.Services;

namespace WebApiCadastro.Models.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        private MySQLContext _context;

        public PersonServiceImplementation(MySQLContext context)
        {
            _context = context;
        }
        Pessoa IPersonService.Create(Pessoa pessoa)
        {
            try
            {
                _context.Add(pessoa);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return pessoa;
        }

        void IPersonService.Delete(long id)
        {
            var result = _context.Pessoas.SingleOrDefault(p => p.ID.Equals(id));
            if (result != null)
            {
                try
                {
                    _context.Pessoas.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        List<Pessoa> IPersonService.FindAll()
        {
            return _context.Pessoas.ToList();
        }

        Pessoa IPersonService.FindById(long id)
        {
            return _context.Pessoas.SingleOrDefault(p => p.ID.Equals(id));
        }

        Pessoa IPersonService.Update(Pessoa pessoa)
        {
            if (!Exists(pessoa.ID)) return new Pessoa();
            var result = _context.Pessoas.SingleOrDefault(p => p.ID.Equals(pessoa.ID));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(pessoa);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            return pessoa;
        }
        private bool Exists(long iD)
        {
            return _context.Pessoas.Any(p => p.ID.Equals(iD));
        }
    }
}
