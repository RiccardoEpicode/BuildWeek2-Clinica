using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuildWeek2.Data;
using BuildWeek2.Models.Entities;
using BuildWeek2.Models.Dto.Visita;

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
        public async Task<ActionResult<IEnumerable<GetVisitaDto>>> GetVisite()
        {
            var visite = await _context.Visite
                .Select(v => new GetVisitaDto
                {
                    VisitaId = v.VisitaId,
                    DataVisita = v.DataVisita,
                    EsameEffettuato = v.EsameEffettuato,
                    DescrizioneEsame = v.DescrizioneEsame
                })
                .ToListAsync();
            return Ok(visite);
        }

        // GET: api/Visitas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetVisitaIdDto>> GetVisitaIdDto(Guid id)
        {
            var visita = await _context.Visite
                .Where(v => v.VisitaId == id)
                .Select(v => new GetVisitaIdDto
                {
                    VisitaId = v.VisitaId,
                    DataVisita = v.DataVisita,
                    EsameEffettuato = v.EsameEffettuato,
                    DescrizioneEsame = v.DescrizioneEsame
                })
                .FirstOrDefaultAsync();
            if (visita == null)
                {
                return NotFound();
            }
            return Ok(visita);
        }

        // PUT: api/Visitas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVisita(Guid id, UpdateVisitaDto updateVisitaDto)
        {
            var visita = await _context.Visite.FindAsync(id);
            if (visita == null)
            {
                return NotFound();
            }
            visita.DataVisita = updateVisitaDto.DataVisita;
            visita.EsameEffettuato = updateVisitaDto.EsameEffettuato;
            visita.DescrizioneEsame = updateVisitaDto.DescrizioneEsame;
            _context.Entry(visita).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await VisitaExists(id))
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
        public async Task<ActionResult<Visita>> PostVisitaDto(CreateVisitaDto createVisitaDto)
        {
            var visita = new Visita
            {
                VisitaId = createVisitaDto.VisitaId,
                DataVisita = createVisitaDto.DataVisita,
                EsameEffettuato = createVisitaDto.EsameEffettuato,
                DescrizioneEsame = createVisitaDto.DescrizioneEsame,
                RicoveroAnimaleSmarritoId = createVisitaDto.RicoveroAnimaleSmarritoId,
                AnimaleId = createVisitaDto.AnimaleId
            };
            _context.Visite.Add(visita);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetVisitaIdDto", new { id = visita.VisitaId }, visita);

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

        private async Task<bool> VisitaExists(Guid id)
        {
            return  await _context.Visite.AnyAsync(e => e.VisitaId == id);
        }
    }
}
