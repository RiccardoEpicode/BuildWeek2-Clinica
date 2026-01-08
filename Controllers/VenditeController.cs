using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuildWeek2.Data;
using BuildWeek2.Models.Entities;
using BuildWeek2.Models.Dto.Vendita;
using System.Security.Claims;

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
        public async Task<ActionResult<IEnumerable<GetVenditaDto>>> GetVendite()
        {
            var vendite = await _context.Vendite
                 .Select(v => new GetVenditaDto
                 {
                     VenditaId = v.VenditaId,
                     DataVendita = v.DataVendita,
                     CodiceFiscale = v.CodiceFiscale,
                     NumeroRicetta = v.NumeroRicetta
                 })
                 .ToListAsync();
            return Ok(vendite);
        }

        // GET: api/Venditas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetVenditaIdDto>> GetVendita(Guid id)
        {
            var vendita = await _context.Vendite
                .Where(v => v.VenditaId == id)
                .Select(v => new GetVenditaIdDto
                {
                    VenditaId = v.VenditaId,
                    DataVendita = v.DataVendita,
                    CodiceFiscale = v.CodiceFiscale,
                    NumeroRicetta = v.NumeroRicetta
                })
                .FirstOrDefaultAsync();
            if (vendita == null)
            {
                return NotFound();
            }
            return Ok(vendita);
        }

        // PUT: api/Venditas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVenditaDto(Guid id, UpdateVenditaDto updateVenditaDto)
        {
            var vendita = await _context.Vendite.FindAsync(id);
            if (vendita == null)
            {
                return NotFound();
            }
            vendita.DataVendita = updateVenditaDto.DataVendita;
            vendita.CodiceFiscale = updateVenditaDto.CodiceFiscale;
            vendita.NumeroRicetta = updateVenditaDto.NumeroRicetta;
            _context.Entry(vendita).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await VenditaExists(id))
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
        public async Task<ActionResult<Vendita>> PostVenditaDto(CreateVenditaDto createVenditaDto)
        {
            var FarmacistaId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (FarmacistaId == null)
            {
                return Unauthorized();
            }
            var vendita = new Vendita
            {
                VenditaId = Guid.NewGuid(),
                DataVendita = createVenditaDto.DataVendita,
                CodiceFiscale = createVenditaDto.CodiceFiscale,
                NumeroRicetta = createVenditaDto.NumeroRicetta,
                ProdottiId = createVenditaDto.ProdottiId,
                FarmacistaId = Guid.Parse(FarmacistaId)
            };
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

        private async Task<bool> VenditaExists(Guid id)
        {
            return await _context.Vendite.AnyAsync(e => e.VenditaId == id);
        }
    }
}
