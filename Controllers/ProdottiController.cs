using BuildWeek2.Data;
using BuildWeek2.Models.Dto.Animale;
using BuildWeek2.Models.Dto.Prodotti;
using BuildWeek2.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildWeek2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdottiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdottiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Prodottis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetProdottiDto>>> GetProdotti()
        {
            var prodotti = await _context.Prodotti
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
            return Ok(prodotti);
        }

        // GET: api/Prodottis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetProdottiIdDto>> GetProdotti(Guid id)
        {
            var prodotti = await _context.Prodotti
           .Where(a => a.ProdottiId == id)
           .Select(a => new GetProdottiIdDto
           {
               ProdottiId = a.ProdottiId,
               NomeProdotto = a.NomeProdotto,
               Medicinale = a.Medicinale,
               Usi = a.Usi,
               CodiceArmadietto = a.CodiceArmadietto,
               CodiceCassetto = a.CodiceCassetto
           })
           .FirstOrDefaultAsync(a => a.ProdottiId == id);


            if (prodotti == null)
            {
                return NotFound();
            }

            return Ok(prodotti);
        }

        // PUT: api/Prodottis/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProdotti(Guid id, UpdateProdottiDto prodottiDto)
        {
            var prodotti = await _context.Prodotti.FindAsync(id);

            if (prodotti == null)
            {
                return NotFound();
            }
            {
                prodotti.NomeProdotto = prodottiDto.NomeProdotto;
                prodotti.Medicinale = prodottiDto.Medicinale;
                prodotti.Usi = prodottiDto.Usi;
                prodotti.CodiceArmadietto = prodottiDto.CodiceArmadietto;
                prodotti.CodiceCassetto = prodottiDto.CodiceCassetto;

            }

            _context.Entry(prodotti).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProdottiExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Prodottis
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Prodotti>> PostProdotti(CreateProdottiDto prodottiDto)
        {
            var prodotti = new Prodotti
            {
                NomeProdotto = prodottiDto.NomeProdotto,
                Medicinale = prodottiDto.Medicinale,
                Usi = prodottiDto.Usi,
                CodiceArmadietto = prodottiDto.CodiceArmadietto,
                CodiceCassetto = prodottiDto.CodiceCassetto,
                FornitoreId = prodottiDto.FornitoreId

            };

            _context.Prodotti.Add(prodotti);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProdotti", new { id = prodotti.ProdottiId }, prodotti);
        }

        // DELETE: api/Prodottis/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProdotti(Guid id)
        {
            var prodotti = await _context.Prodotti.FindAsync(id);
            if (prodotti == null)
            {
                return NotFound();
            }

            _context.Prodotti.Remove(prodotti);
            await _context.SaveChangesAsync();

            return NoContent(); ;
        }

        private async Task<bool> ProdottiExists(Guid id)
        {
            return await _context.Prodotti.AnyAsync(e => e.ProdottiId == id);
        }
    }
}
