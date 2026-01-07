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
    public class RicoveroAnimaleSmarritoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RicoveroAnimaleSmarritoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/RicoveroAnimaleSmarritoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RicoveroAnimaleSmarrito>>> GetRicoveriAnimaliSmarriti()
        {
            return await _context.RicoveriAnimaliSmarriti.ToListAsync();
        }

        // GET: api/RicoveroAnimaleSmarritoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RicoveroAnimaleSmarrito>> GetRicoveroAnimaleSmarrito(Guid id)
        {
            var ricoveroAnimaleSmarrito = await _context.RicoveriAnimaliSmarriti.FindAsync(id);

            if (ricoveroAnimaleSmarrito == null)
            {
                return NotFound();
            }

            return ricoveroAnimaleSmarrito;
        }

        // PUT: api/RicoveroAnimaleSmarritoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRicoveroAnimaleSmarrito(Guid id, RicoveroAnimaleSmarrito ricoveroAnimaleSmarrito)
        {
            if (id != ricoveroAnimaleSmarrito.RicoveroAnimaleSmarritoId)
            {
                return BadRequest();
            }

            _context.Entry(ricoveroAnimaleSmarrito).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RicoveroAnimaleSmarritoExists(id))
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

        // POST: api/RicoveroAnimaleSmarritoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RicoveroAnimaleSmarrito>> PostRicoveroAnimaleSmarrito(RicoveroAnimaleSmarrito ricoveroAnimaleSmarrito)
        {
            _context.RicoveriAnimaliSmarriti.Add(ricoveroAnimaleSmarrito);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRicoveroAnimaleSmarrito", new { id = ricoveroAnimaleSmarrito.RicoveroAnimaleSmarritoId }, ricoveroAnimaleSmarrito);
        }

        // DELETE: api/RicoveroAnimaleSmarritoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRicoveroAnimaleSmarrito(Guid id)
        {
            var ricoveroAnimaleSmarrito = await _context.RicoveriAnimaliSmarriti.FindAsync(id);
            if (ricoveroAnimaleSmarrito == null)
            {
                return NotFound();
            }

            _context.RicoveriAnimaliSmarriti.Remove(ricoveroAnimaleSmarrito);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RicoveroAnimaleSmarritoExists(Guid id)
        {
            return _context.RicoveriAnimaliSmarriti.Any(e => e.RicoveroAnimaleSmarritoId == id);
        }
    }
}
