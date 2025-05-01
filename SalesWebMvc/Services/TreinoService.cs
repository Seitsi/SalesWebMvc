using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
    public class TreinoService
    {
        private readonly SalesWebMvcContext _context;

        public TreinoService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Treino>> FindAllAsync()
        {
            return await _context.Treino.OrderBy(x => x.Data).ToListAsync();
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var treino = await _context.Treino.FindAsync(id);
                if (treino != null)
                {
                    _context.Treino.Remove(treino);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task InsertAsync(Treino treino)
        {
            _context.Add(treino);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Treino treino)
        {
            bool hasAny = await _context.Treino.AnyAsync(x => x.Id == treino.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id não encontrado!");
            }
            try
            {
                _context.Update(treino);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
