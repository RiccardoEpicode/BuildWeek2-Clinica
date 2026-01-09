using BuildWeek2.Data;
using BuildWeek2.Models.Dto.Prodotti;
using BuildWeek2.Models.Entities;
using BuildWeek2.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BuildWeek2.Services
{
    public class ProdottiService : IProdottiService
    {
        private readonly AppDbContext _context;
        public ProdottiService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetProdottiDto>> GetAllProducts()
        {
            var animali = await _context.Prodotti
                .Select(a => new GetProdottiDto
                {
                    ProdottiId = a.ProdottiId,
                    NomeProdotto = a.NomeProdotto,
                    Medicinale = a.Medicinale,
                    Usi = a.Usi,
                    CodiceArmadietto = a.CodiceArmadietto,
                    CodiceCassetto = a.CodiceCassetto

                })
                .ToListAsync();
            return animali;
        }

        public async Task<Prodotti> GetProdottoById(Guid Id)
        {
            return await this._context.Prodotti.AsNoTracking().FirstOrDefaultAsync(s => s.ProdottiId == Id);
        }

        //SAVE FOR UPDATE
        public async Task UpdateProductAsync(Prodotti prodotto)
        {
            _context.Prodotti.Update(prodotto);
            await _context.SaveChangesAsync();

        }



        public async Task<Prodotti> CreateProdottoAsync(Prodotti prodotto)
        {

            _context.Prodotti.Add(prodotto);
            await _context.SaveChangesAsync();
            return prodotto;

        }

        public async Task DeleteProdottoAsync(Guid id)
        {
            var prodotto = await _context.Prodotti.FindAsync(id);
            if (prodotto != null)
            {
                _context.Prodotti.Remove(prodotto);
                await _context.SaveChangesAsync();
            }
        }

    }
}
