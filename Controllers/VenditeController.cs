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
    public class VenditeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VenditeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Venditas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vendita>>> GetVendite()
        {
            return await _context.Vendite.ToListAsync();
        }

        // GET: api/Venditas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vendita>> GetVendita(Guid id)
        {
            var vendita = await _context.Vendite.FindAsync(id);

            if (vendita == null)
            {
                return NotFound();
            }

            return vendita;
        }

        // PUT: api/Venditas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVendita(Guid id, Vendita vendita)
        {
            if (id != vendita.VenditaId)
            {
                return BadRequest();
            }

            _context.Entry(vendita).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VenditaExists(id))
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

        // POST: api/Venditas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vendita>> PostVendita(Vendita vendita)
        {
            _context.Vendite.Add(vendita);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVendita", new { id = vendita.VenditaId }, vendita);
        }

        // DELETE: api/Venditas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendita(Guid id)
        {
            var vendita = await _context.Vendite.FindAsync(id);
            if (vendita == null)
            {
                return NotFound();
            }

            _context.Vendite.Remove(vendita);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VenditaExists(Guid id)
        {
            return _context.Vendite.Any(e => e.VenditaId == id);
        }
    }
}
