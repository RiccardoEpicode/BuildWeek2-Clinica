using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuildWeek2.Data;
using BuildWeek2.Models.Entities;
using BuildWeek2.Models.Dto.RicoveroAnimale;
namespace BuildWeek2.Controllers;

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
                    NomeAnimale = r.Animale.Nome
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
                AnimaleId = r.AnimaleId,
                NomeAnimale = r.Animale.Nome
            })
            .FirstOrDefaultAsync();

        if (ricoveroAnimaleId == null)
        {
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
            if (!await RicoveroAnimaleExists(id))
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
    public async Task<ActionResult<RicoveroAnimale>> PostRicoveroAnimale(CreateRicoveroAnimaleDto ricoveroAnimaleDto)
    {
        var animaleExists = await _context.Animali.AnyAsync(a => a.AnimaleId == ricoveroAnimaleDto.AnimaleId);
        if (!animaleExists)
        {
            return BadRequest("L'animale specificato non esiste.");
        }
        var ricoveroAnimale = new RicoveroAnimale
        {
            DataInizioRicovero = ricoveroAnimaleDto.DataInizioRicovero,
            DataFineRicovero = ricoveroAnimaleDto.DataFineRicovero,
            Attivo = ricoveroAnimaleDto.Attivo,
            AnimaleId = ricoveroAnimaleDto.AnimaleId
        };
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

    private async Task<bool> RicoveroAnimaleExists(Guid id)
    {
        return await _context.RicoveriAnimali.AnyAsync(e => e.RicoveroAnimaleId == id);
    }
}
