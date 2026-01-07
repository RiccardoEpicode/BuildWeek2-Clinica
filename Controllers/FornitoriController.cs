using BuildWeek2.Data;
using BuildWeek2.Models.Dto.Animale;
using BuildWeek2.Models.Dto.Fornitore;
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
    public class FornitoriController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FornitoriController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Fornitores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetFornitoreDto>>> GetFornitori()
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
            return Ok(fornitore);
        }

        // GET: api/Fornitores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetFornitoreIdDto>> GetFornitore(Guid id)
        {
            var fornitore = await _context.Fornitori
                 .Where(a => a.FornitoreId == id)
                 .Select(a => new GetFornitoreIdDto
                 {
                    FornitoreId = a.FornitoreId,
                    Nome = a.Nome,
                    Recapito = a.Recapito,
                    Indirizzo = a.Indirizzo
                 })
                 .FirstOrDefaultAsync(a => a.FornitoreId == id);


            if (fornitore == null)
            {
                return NotFound();
            }

            return Ok(fornitore);
        }

        // PUT: api/Fornitores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFornitore(Guid id, UpdateFornitoreDto fornitoreDto)
        {
            var fornitore = await _context.Fornitori.FindAsync(id);

            if (fornitore == null)
            {
                return NotFound();
            }
            {
                fornitore.Nome = fornitoreDto.Nome;
                fornitore.Recapito = fornitoreDto.Recapito;
                fornitore.Indirizzo = fornitoreDto.Indirizzo;
            }



            _context.Entry(fornitore).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await FornitoreExists(id))
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

        // POST: api/Fornitores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Fornitore>> PostFornitore(UpdateFornitoreDto fornitoreDto)
        {
            var fornitore = new Fornitore
            {
                Nome = fornitoreDto.Nome,
                Recapito = fornitoreDto.Recapito,
                Indirizzo = fornitoreDto.Indirizzo
            };

            _context.Fornitori.Add(fornitore);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFornitore", new { id = fornitore.FornitoreId }, fornitore);
        }

        // DELETE: api/Fornitores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFornitore(Guid id)
        {
            var fornitore = await _context.Fornitori.FindAsync(id);
            if (fornitore == null)
            {
                return NotFound();
            }

            _context.Fornitori.Remove(fornitore);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> FornitoreExists(Guid id)
        {
            return await _context.Fornitori.AnyAsync(e => e.FornitoreId == id);
        }
    }
}
