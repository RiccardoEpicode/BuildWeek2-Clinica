using BuildWeek2.Data;
using BuildWeek2.Models.Dto.Fornitore;
using BuildWeek2.Models.Entities;
using BuildWeek2.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BuildWeek2.Services
{
    public class FornitoriService : IFornitoreService
    {
        private readonly AppDbContext _context;
        public FornitoriService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetFornitoreDto>> GetAllFornitori()
        {
            var fornitore = await _context.Fornitori
                .Select(a => new GetFornitoreDto
                {
                    FornitoreId = a.FornitoreId,
                    Nome = a.Nome,
                    Recapito = a.Recapito,
                    Indirizzo = a.Indirizzo
                })
                .ToListAsync();
            return fornitore;
        }

        public async Task<Fornitore> GetFornitoreById(Guid Id)
        {
            return await this._context.Fornitori.AsNoTracking().FirstOrDefaultAsync(s => s.FornitoreId == Id);
        }

        // Updated to match interface: returns Task<Fornitore>
        public async Task<Fornitore> UpdateFornitoreAsync(Fornitore fornitore)
        {
            _context.Fornitori.Update(fornitore);
            await _context.SaveChangesAsync();
            return fornitore;
        }

        // Updated to match interface: returns Task
        public async Task CreateFornitoreAsync(Fornitore fornitore)
        {
            _context.Fornitori.Add(fornitore);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFornitoreAsync(Guid id)
        {
            var fornitore = await _context.Fornitori.FindAsync(id);
            if (fornitore != null)
            {
                _context.Fornitori.Remove(fornitore);
                await _context.SaveChangesAsync();
            }
        }
    }
}
