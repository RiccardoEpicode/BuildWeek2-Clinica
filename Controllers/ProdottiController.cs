using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuildWeek2.Data;
using BuildWeek2.Models.Entities;

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
        public async Task<ActionResult<IEnumerable<Prodotti>>> GetProdotti()
        {
            return await _context.Prodotti.ToListAsync();
        }

        // GET: api/Prodottis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Prodotti>> GetProdotti(Guid id)
        {
            var prodotti = await _context.Prodotti.FindAsync(id);

            if (prodotti == null)
            {
                return NotFound();
            }

            return prodotti;
        }

        // PUT: api/Prodottis/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProdotti(Guid id, Prodotti prodotti)
        {
            if (id != prodotti.ProdottiId)
            {
                return BadRequest();
            }

            _context.Entry(prodotti).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdottiExists(id))
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
        public async Task<ActionResult<Prodotti>> PostProdotti(Prodotti prodotti)
        {
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

            return NoContent();
        }

        private bool ProdottiExists(Guid id)
        {
            return _context.Prodotti.Any(e => e.ProdottiId == id);
        }
    }
}
