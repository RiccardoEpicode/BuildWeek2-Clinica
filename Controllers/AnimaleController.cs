using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuildWeek2.Data;
using BuildWeek2.Models.Dto.Animale;

namespace BuildWeek2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnimaleController : ControllerBase
{
    private readonly AppDbContext _context;

    public AnimaleController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Animales
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAnimaleDto>>> GetAnimali()
    {
        var animali = await _context.Animali
            .Select(a => new GetAnimaleDto
            {
                AnimaleId = a.AnimaleId,
                Nome = a.Nome,
                Tipologia = a.Tipologia

            })
            .ToListAsync();
        return Ok(animali);
    }

    // GET: api/Animales/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GetAnimaleIdDto>> GetAnimale(Guid id)
    {
        var animale = await _context.Animali
            .Where(a => a.AnimaleId == id)
            .Select(a => new GetAnimaleIdDto
            {
                AnimaleId = a.AnimaleId,
                Nome = a.Nome,
                Tipologia = a.Tipologia,
                Proprietario = a.Proprietario,
                NumeroMicrochip = a.NumeroMicrochip
            })
            .FirstOrDefaultAsync(a => a.AnimaleId == id);


        if (animale == null)
        {
            return NotFound();
        }

        return Ok(animale);
    }

    // PUT: api/Animales/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAnimale(Guid id, UpdateAnimaleDto animaleDto)
    {


        var animale = await _context.Animali.FindAsync(id);

        if (animale == null)
        {
            return NotFound();
        }
        {
            animale.Nome = animaleDto.Nome;
            animale.DataRegistrazione = animaleDto.DataRegistrazione;
            animale.Tipologia = animaleDto.Tipologia;
            animale.ColoreMantello = animaleDto.ColoreMantello;
            animale.DataNascita = animaleDto.DataNascita;
            animale.PresenzaMicrochip = animaleDto.PresenzaMicrochip;
            animale.NumeroMicrochip = animaleDto.NumeroMicrochip;
            animale.Proprietario = animaleDto.Proprietario;
        }

        _context.Entry(animale).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await AnimaleExists(id))
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

    // POST: api/Animales

    [HttpPost]
    public async Task<ActionResult<Animale>> PostAnimale(CreateAnimaleDto animaleDto)
    {
        var animale = new Animale
        {
            Nome = animaleDto.Nome,
            Tipologia = animaleDto.Tipologia,
            ColoreMantello = animaleDto.ColoreMantello,
            DataNascita = animaleDto.DataNascita,
            PresenzaMicrochip = animaleDto.PresenzaMicrochip,
            NumeroMicrochip = animaleDto.NumeroMicrochip,
            Proprietario = animaleDto.Proprietario
        };

        _context.Animali.Add(animale);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetAnimale", new { id = animale.AnimaleId }, animale);

    }

    // DELETE: api/Animales/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAnimale(Guid id)
    {
        var animale = await _context.Animali.FindAsync(id);
        if (animale == null)
        {
            return NotFound();
        }

        _context.Animali.Remove(animale);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> AnimaleExists(Guid id)
    {
        return await _context.Animali.AnyAsync(e => e.AnimaleId == id);
    }
}
