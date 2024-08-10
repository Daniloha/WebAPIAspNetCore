using WebApiCadastro.Models.Context;
using WebApiCadastro.Models;

namespace WebApiCadastro.Repository.Implementations
{
    public class BookRepositoryImplementation : IBookRepository
    {
        public MySQLContext _context;

        public BookRepositoryImplementation(MySQLContext context)
        {
            _context = context;
        }

        public Livros Create(Livros livro)
        {
            try
            {
                _context.Add(livro);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return livro;
        }

        public void Delete(long id)
        {
            var result = _context.Biblioteca.SingleOrDefault(p => p.ID.Equals(id));
            if (result != null)
            {
                try
                {
                    _context.Biblioteca.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

        }

        public List<Livros> FindAll()
        {

            return _context.Biblioteca.ToList();
        }




        public Livros FindByID(long id)
        {
            return _context.Biblioteca.SingleOrDefault(p => p.ID.Equals(id));

        }

        public Livros Update(Livros livro)
        {
            if (!Exists(livro.ID)) return null;
            var result = _context.Biblioteca.SingleOrDefault(p => p.ID.Equals(livro.ID));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(livro);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            return livro;
        }

        public bool Exists(long iD)
        {
            return _context.Biblioteca.Any(p => p.ID.Equals(iD));
        }
    }
}
