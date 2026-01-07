
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuildWeek2.Data;
using BuildWeek2.Models.Entities;
using BuildWeek2.Models.Dto.RicoveroAnimale;

namespace BuildWeek2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RicoveroAnimaleController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RicoveroAnimaleController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/RicoveroAnimales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetRicoveroAnimaleDto>>> GetRicoveriAnimali()
        {
        var ricoveroAnimali = await _context.RicoveriAnimali
                .Select(r => new GetRicoveroAnimaleDto
                {
                    RicoveroAnimaleId = r.RicoveroAnimaleId,
                    DataInizioRicovero = r.DataInizioRicovero,
                    DataFineRicovero = r.DataFineRicovero,
                    Attivo = r.Attivo,
                })
                .ToListAsync();
            return Ok(ricoveroAnimali);
        }

        // GET: api/RicoveroAnimales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetRicoveroAnimaleIdDto>> GetRicoveroAnimale(Guid id)
        {
            var ricoveroAnimaleId = await _context.RicoveriAnimali
                .Where(r => r.RicoveroAnimaleId == id)
                .Select(r => new GetRicoveroAnimaleIdDto
                {
                    RicoveroAnimaleId = r.RicoveroAnimaleId,
                    DataInizioRicovero = r.DataInizioRicovero,
                    DataFineRicovero = r.DataFineRicovero,
                    Attivo = r.Attivo,
                    AnimaleId = r.AnimaleId
                })
                .FirstOrDefaultAsync();

            if (ricoveroAnimaleId == null) { 
                return NotFound();
            }
            return Ok(ricoveroAnimaleId);
        }


        // PUT: api/RicoveroAnimales/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRicoveroAnimale(Guid id, UpdateRicoveroAnimaleDto ricoveroAnimaleDto)
        {
           var ricoveroAnimale = await _context.RicoveriAnimali.FindAsync(id);
            if (ricoveroAnimale == null)
            {
                return NotFound();
            }

            {
                ricoveroAnimale.DataInizioRicovero = ricoveroAnimaleDto.DataInizioRicovero;
                ricoveroAnimale.DataFineRicovero = ricoveroAnimaleDto.DataFineRicovero;
                ricoveroAnimale.Attivo = ricoveroAnimaleDto.Attivo;
            }

            _context.Entry(ricoveroAnimale).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RicoveroAnimaleExists(id))
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

        // POST: api/RicoveroAnimales
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RicoveroAnimale>> PostRicoveroAnimale(RicoveroAnimale ricoveroAnimale)
        {
            _context.RicoveriAnimali.Add(ricoveroAnimale);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRicoveroAnimale", new { id = ricoveroAnimale.RicoveroAnimaleId }, ricoveroAnimale);
        }

        // DELETE: api/RicoveroAnimales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRicoveroAnimale(Guid id)
        {
            var ricoveroAnimale = await _context.RicoveriAnimali.FindAsync(id);
            if (ricoveroAnimale == null)
            {
                return NotFound();
            }

            _context.RicoveriAnimali.Remove(ricoveroAnimale);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RicoveroAnimaleExists(Guid id)
        {
            return _context.RicoveriAnimali.Any(e => e.RicoveroAnimaleId == id);
        }
    }
}
