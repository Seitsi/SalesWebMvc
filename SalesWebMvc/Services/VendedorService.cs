using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
    public class VendedorService
    {
        private readonly SalesWebMvcContext _context;

        public VendedorService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Vendedor>> FindAllAsync()
        {
            return await _context.Vendedor.Include(v => v.Departamento).ToListAsync();
        }

        public async Task<Vendedor> GetByIdAsync(int id)
        {
            return await _context.Vendedor.Include(v => v.Departamento).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task DeleteAsync(int id) 
        { 
            var vendedor = await _context.Vendedor.FindAsync(id);
            if (vendedor != null)
            {
                _context.Vendedor.Remove(vendedor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task InsertAsync(Vendedor vendedor)
        {
            _context.Add(vendedor);
            _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Vendedor vendedor)
        {
            bool hasAny = await _context.Vendedor.AnyAsync(x => x.Id == vendedor.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id não encontrado!");
            }
            try
            {
                _context.Update(vendedor);
                _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e) 
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
