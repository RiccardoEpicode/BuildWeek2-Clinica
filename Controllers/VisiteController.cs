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
    public class VisiteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VisiteController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Visitas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Visita>>> GetVisite()
        {
            return await _context.Visite.ToListAsync();
        }

        // GET: api/Visitas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Visita>> GetVisita(Guid id)
        {
            var visita = await _context.Visite.FindAsync(id);

            if (visita == null)
            {
                return NotFound();
            }

            return visita;
        }

        // PUT: api/Visitas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVisita(Guid id, Visita visita)
        {
            if (id != visita.VisitaId)
            {
                return BadRequest();
            }

            _context.Entry(visita).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitaExists(id))
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

        // POST: api/Visitas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Visita>> PostVisita(Visita visita)
        {
            _context.Visite.Add(visita);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVisita", new { id = visita.VisitaId }, visita);
        }

        // DELETE: api/Visitas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisita(Guid id)
        {
            var visita = await _context.Visite.FindAsync(id);
            if (visita == null)
            {
                return NotFound();
            }

            _context.Visite.Remove(visita);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VisitaExists(Guid id)
        {
            return _context.Visite.Any(e => e.VisitaId == id);
        }
    }
}
