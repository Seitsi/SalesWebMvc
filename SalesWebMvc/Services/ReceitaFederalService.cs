using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class ReceitaFederalService
    {
        private readonly SalesWebMvcContext _context;

        public ReceitaFederalService(SalesWebMvcContext context)
        {
            _context = context;
        }

    }
}
