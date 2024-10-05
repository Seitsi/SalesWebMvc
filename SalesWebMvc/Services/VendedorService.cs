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
            return _context.Vendedor.ToList();
        }
    }
}
