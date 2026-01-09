using BuildWeek2.Data;
using BuildWeek2.Models.Dto.Fornitore;
using BuildWeek2.Models.Dto.RicoveroAnimale;
using BuildWeek2.Models.Entities;
using BuildWeek2.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BuildWeek2.Services
{
    public class RicoveroAnimaleService : IRicoveroAnimaleService
    {
        private readonly AppDbContext _context;
        public RicoveroAnimaleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetRicoveroAnimaleDto>> GetAllAnimaliRicoverati()
        {
            var animalericoverato = await _context.RicoveriAnimali
                .Select(a => new GetRicoveroAnimaleDto
                {
                    RicoveroAnimaleId = a.RicoveroAnimaleId,
                    DataInizioRicovero = a.DataInizioRicovero,
                    DataFineRicovero = a.DataFineRicovero,
                    Attivo = a.Attivo,
                    NomeAnimale = a.Animale.Nome
                })
                .ToListAsync();
            return animalericoverato;
        }

        public async Task<RicoveroAnimale> GetAnimaliRicoveratiById(Guid Id)
        {
            return await this._context.RicoveriAnimali.AsNoTracking().FirstOrDefaultAsync(s => s.RicoveroAnimaleId == Id);
        }

        // Updated to match interface: returns Task<Fornitore>
        public async Task<RicoveroAnimale> UpdateAnimaliRicoveratiAsync(RicoveroAnimale ricoveroAnimale)
        {
            _context.RicoveriAnimali.Update(ricoveroAnimale);
            await _context.SaveChangesAsync();
            return ricoveroAnimale;
        }

        // Updated to match interface: returns Task
        public async Task CreateAnimaliRicoveratiAsync(RicoveroAnimale ricoveroAnimale)
        {
            _context.RicoveriAnimali.Add(ricoveroAnimale);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAnimaliRicoveratiAsync(Guid id)
        {
            var ricoveroAnimale = await _context.RicoveriAnimali.FindAsync(id);
            if (ricoveroAnimale != null)
            {
                _context.RicoveriAnimali.Remove(ricoveroAnimale);
                await _context.SaveChangesAsync();
            }
        }
    }
}
