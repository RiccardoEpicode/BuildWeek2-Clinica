
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuildWeek2.Data;
using BuildWeek2.Models.Entities;
using BuildWeek2.Models.Dto.RicoveroAnimaleSmarrito;
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
        public async Task<ActionResult<IEnumerable<GetAnimaleSmarritoDto>>> GetRicoveriAnimaliSmarriti()
        {
            var ricoveroAnimaliSmarriti = await _context.RicoveriAnimaliSmarriti
                    .Select(r => new GetAnimaleSmarritoDto
                    {
                        RicoveroAnimaleSmarritoId = r.RicoveroAnimaleSmarritoId,
                        DataInizioRicoveroSmarrito = r.DataInizioRicoveroSmarrito,
                        DataFineRicoveroSmarrito = r.DataFineRicoveroSmarrito,
                        Tipologia = r.Tipologia,
                        ColoreMantello = r.ColoreMantello,
                        DataNascita = r.DataNascita,
                        NumeroMicrochip = r.NumeroMicrochip,
                        Attivo = r.Attivo
                    })
                    .ToListAsync();
            return Ok(ricoveroAnimaliSmarriti);
        }

        // GET: api/RicoveroAnimaleSmarritoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetAnimaleSmarritoIdDto>> GetRicoveroAnimaleSmarrito(Guid id)
        { 
            var ricoveroAnimaleSmarrito = await _context.RicoveriAnimaliSmarriti
                .Where(r => r.RicoveroAnimaleSmarritoId == id)
                .Select(r => new GetAnimaleSmarritoIdDto
                {
                    RicoveroAnimaleSmarritoId = r.RicoveroAnimaleSmarritoId,
                    DataInizioRicoveroSmarrito = r.DataInizioRicoveroSmarrito,
                    DataFineRicoveroSmarrito = r.DataFineRicoveroSmarrito,
                    Tipologia = r.Tipologia,
                    ColoreMantello = r.ColoreMantello,
                    DataNascita = r.DataNascita,
                    NumeroMicrochip = r.NumeroMicrochip,
                    Attivo = r.Attivo
                })
                .FirstOrDefaultAsync();
            if (ricoveroAnimaleSmarrito == null)
            {
                return NotFound();
            }
            return Ok(ricoveroAnimaleSmarrito);

        }

        // PUT: api/RicoveroAnimaleSmarritoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRicoveroAnimaleSmarrito(Guid id, UpdateAnimaleSmarritoDto updateAnimaleSmarritoDto)
        {
            var ricoveroAnimaleSmarrito = await _context.RicoveriAnimaliSmarriti.FindAsync(id);
            if (ricoveroAnimaleSmarrito == null)
            {
                return NotFound();
            }
            ricoveroAnimaleSmarrito.DataInizioRicoveroSmarrito = updateAnimaleSmarritoDto.DataInizioRicoveroSmarrito;
            ricoveroAnimaleSmarrito.DataFineRicoveroSmarrito = updateAnimaleSmarritoDto.DataFineRicoveroSmarrito;
            ricoveroAnimaleSmarrito.Tipologia = updateAnimaleSmarritoDto.Tipologia;
            ricoveroAnimaleSmarrito.ColoreMantello = updateAnimaleSmarritoDto.ColoreMantello;
            ricoveroAnimaleSmarrito.DataNascita = updateAnimaleSmarritoDto.DataNascita;
            ricoveroAnimaleSmarrito.NumeroMicrochip = updateAnimaleSmarritoDto.NumeroMicrochip;
            ricoveroAnimaleSmarrito.Attivo = updateAnimaleSmarritoDto.Attivo;
            _context.Entry(ricoveroAnimaleSmarrito).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await RicoveroAnimaleSmarritoExists(id))
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
        public async Task<ActionResult<RicoveroAnimaleSmarrito>> PostRicoveroAnimaleSmarritoDto(CreateAnimaleSmarritoDto createAnimaleSmarritoDto)
        {
            var ricoveroAnimaleSmarrito = new RicoveroAnimaleSmarrito
            {
                RicoveroAnimaleSmarritoId = Guid.NewGuid(),
                DataInizioRicoveroSmarrito = createAnimaleSmarritoDto.DataInizioRicoveroSmarrito,
                DataFineRicoveroSmarrito = createAnimaleSmarritoDto.DataFineRicoveroSmarrito,
                Tipologia = createAnimaleSmarritoDto.Tipologia,
                ColoreMantello = createAnimaleSmarritoDto.ColoreMantello,
                DataNascita = createAnimaleSmarritoDto.DataNascita,
                NumeroMicrochip = createAnimaleSmarritoDto.NumeroMicrochip,
                Attivo = createAnimaleSmarritoDto.Attivo
            };
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

        private async Task<bool> RicoveroAnimaleSmarritoExists(Guid id)
        {
            return await _context.RicoveriAnimaliSmarriti.AnyAsync(e => e.RicoveroAnimaleSmarritoId == id);
        }
    }
}
