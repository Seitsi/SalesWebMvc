using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class VendedorService
    {
        private readonly SalesWebMvcContext _context;

        public VendedorService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public List<Vendedor> FindAll()
        {
            return _context.Vendedor.Include(v => v.Departamento).ToList();
        }

        public Vendedor GetById(int id)
        {
            return _context.Vendedor.Include(v => v.Departamento).FirstOrDefault(obj => obj.Id == id);
        }

        public void Delete(int id) 
        { 
            var vendedor = _context.Vendedor.Find(id);
            if (vendedor != null)
            {
                _context.Vendedor.Remove(vendedor);
                _context.SaveChanges();
            }
        }

        public  void Insert(Vendedor vendedor)
        {
            _context.Add(vendedor);
            _context.SaveChanges();
        }
    }
}
