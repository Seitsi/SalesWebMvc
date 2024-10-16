using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class VendaService
    {
        private readonly SalesWebMvcContext _context;

        public VendaService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Venda>> FindByDateAsync(DateTime? dataMin, DateTime? dataMax)
        {
            var result = from obj in _context.Venda select obj;
            if (dataMin.HasValue)
            {
                result = result.Where(x => x.Data >= dataMin.Value);
            }
            if (dataMax.HasValue)
            {
                result = result.Where(x => x.Data <= dataMax.Value);
            }
            return await result
                .Include(x => x.Vendedor)
                .Include(x => x.Vendedor.Departamento)
                .OrderByDescending(x => x.Data)
                .ToListAsync();

        }

        public async Task<List<IGrouping<Departamento,Venda>>> FindByDateGroupingAsync(DateTime? dataMin, DateTime? dataMax)
        {
            var result = from obj in _context.Venda select obj;
            if (dataMin.HasValue)
            {
                result = result.Where(x => x.Data >= dataMin.Value);
            }
            if (dataMax.HasValue)
            {
                result = result.Where(x => x.Data <= dataMax.Value);
            }
            return await result
                .Include(x => x.Vendedor)
                .Include(x => x.Vendedor.Departamento)
                .OrderByDescending(x => x.Data)
                .GroupBy(x => x.Vendedor.Departamento)
                .ToListAsync();
        }

        public async Task InsertAsync(Venda venda)
        {
            _context.Add(venda);
            await _context.SaveChangesAsync();
        }
    }
}
