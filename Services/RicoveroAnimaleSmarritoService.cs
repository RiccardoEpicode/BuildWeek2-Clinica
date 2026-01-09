using BuildWeek2.Data;
using BuildWeek2.Models.Dto.RicoveroAnimaleSmarrito;
using BuildWeek2.Models.Entities;
using BuildWeek2.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BuildWeek2.Services
{
    public class RicoveroAnimaleSmarritoService: IRicoveroAnimaleSmarritoService
    {
        private readonly AppDbContext _context;
        public RicoveroAnimaleSmarritoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetAnimaleSmarritoDto>> GetAllAnimaliRicoverati()
        {
            var animaleSmarritoRicoverato = await _context.RicoveriAnimaliSmarriti
                .Select(a => new GetAnimaleSmarritoDto
                {
                    RicoveroAnimaleSmarritoId = a.RicoveroAnimaleSmarritoId,
                    DataInizioRicoveroSmarrito = a.DataInizioRicoveroSmarrito,
                    DataFineRicoveroSmarrito = a.DataFineRicoveroSmarrito,
                    Tipologia = a.Tipologia,
                    ColoreMantello = a.ColoreMantello,
                    DataNascita = a.DataNascita,
                    NumeroMicrochip = a.NumeroMicrochip,
                    Attivo = a.Attivo
                })
                .ToListAsync();
            return animaleSmarritoRicoverato;
        }

        public async Task<RicoveroAnimaleSmarrito> GetAnimaliSmarritiRicoveratiById(Guid Id)
        {
            return await this._context.RicoveriAnimaliSmarriti.AsNoTracking().FirstOrDefaultAsync(s => s.RicoveroAnimaleSmarritoId == Id);
        }

        // Updated to match interface: returns Task<Fornitore>
        public async Task<RicoveroAnimaleSmarrito> UpdateAnimaliSmarritiRicoveratiAsync(RicoveroAnimaleSmarrito ricoveroAnimaleSmarrito)
        {
            _context.RicoveriAnimaliSmarriti.Update(ricoveroAnimaleSmarrito);
            await _context.SaveChangesAsync();
            return ricoveroAnimaleSmarrito;
        }

        // Updated to match interface: returns Task
        public async Task CreateAnimaliSmarritiRicoveratiAsync(RicoveroAnimaleSmarrito ricoveroAnimaleSmarrito)
        {
            _context.RicoveriAnimaliSmarriti.Add(ricoveroAnimaleSmarrito);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAnimaliRicoveratiAsync(Guid id)
        {
            var ricoveroAnimaleSmarrito = await _context.RicoveriAnimaliSmarriti.FindAsync(id);
            if (ricoveroAnimaleSmarrito != null)
            {
                _context.RicoveriAnimaliSmarriti.Remove(ricoveroAnimaleSmarrito);
                await _context.SaveChangesAsync();
            }
        }
    }
}
