using BuildWeek2.Data;
using BuildWeek2.Models.Dto.Animale;
using BuildWeek2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BuildWeek2.Services
{
    public class AnimaleService : IAnimaleService
    {
        private readonly AppDbContext _context;
        public AnimaleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetAnimaleDto>> GetAllAnimaliAsync()
        {
            var animali = await _context.Animali
                .Select(a => new GetAnimaleDto
                {
                    AnimaleId = a.AnimaleId,
                    Nome = a.Nome,
                    Tipologia = a.Tipologia
                })
                .ToListAsync();
            return animali;
        }


        public async Task<Animale> GetByIdAsNoTracking(Guid Id)
        {
            return await this._context.Animali.AsNoTracking().FirstOrDefaultAsync(s => s.AnimaleId == Id);
        }

        //SAVE FOR UPDATE
        public async Task Save(Animale animale)
        {
            _context.Animali.Update(animale);
            await _context.SaveChangesAsync();

        }



        public async Task<Animale> CreateAnimaleAsync(Animale animale)
        {
        
        _context.Animali.Add(animale);
            await _context.SaveChangesAsync();
            return animale;

        }

        public async Task DeleteAnimaleAsync(Guid id)
        {
            var animale = await _context.Animali.FindAsync(id);
            if (animale != null)
            {
                _context.Animali.Remove(animale);
                await _context.SaveChangesAsync();
            }
        }

    }
    }



