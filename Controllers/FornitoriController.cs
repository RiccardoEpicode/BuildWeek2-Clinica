using BuildWeek2.Data;
using BuildWeek2.Models.Dto.Animale;
using BuildWeek2.Models.Dto.Fornitore;
using BuildWeek2.Models.Dto.Prodotti;
using BuildWeek2.Models.Entities;
using BuildWeek2.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildWeek2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FornitoriController : ControllerBase
    {
        private readonly IFornitoreService _service;

        public FornitoriController(IFornitoreService service)
        {
            _service = service;
        }
        // GET: api/Animales
        [HttpGet]

        public async Task<ActionResult<IEnumerable<GetFornitoreDto>>> GetFornitori()
        {
            var fornitore = await _service.GetAllFornitori();
            return Ok(fornitore);
        }

        [HttpGet("{Id:guid}")]

        public async Task<ActionResult<GetFornitoreDto>> GetProdottoById(Guid Id)
        {
            var fornitore = await _service.GetFornitoreById(Id);
            if (fornitore == null)
            {
                return NotFound();
            }
            return Ok(fornitore);
        }

        [HttpPost]
        public async Task<ActionResult<CreateFornitoreDto>> CreateFornitore(CreateFornitoreDto fornitoreDto)
        {
            var fornitore = new Fornitore
            {
                Nome = fornitoreDto.Nome,
                Recapito = fornitoreDto.Recapito,
                Indirizzo = fornitoreDto.Indirizzo
            };
            await _service.CreateFornitoreAsync(fornitore);
            return CreatedAtAction(nameof(GetProdottoById), new { Id = fornitore.FornitoreId }, fornitore);

        }
        //PUT
        [HttpPut("{Id:guid}")]
        public async Task<IActionResult> UpdateFornitore(Guid Id, UpdateFornitoreDto fornitoreDto)
        {
            var existingFotnitore = await _service.GetFornitoreById(Id);
            if (existingFotnitore == null)
            {
                return NotFound();
            }
            existingFotnitore.Nome = fornitoreDto.Nome;
            existingFotnitore.Recapito = fornitoreDto.Recapito;
            existingFotnitore.Indirizzo = fornitoreDto.Indirizzo;
            await _service.UpdateFornitoreAsync(existingFotnitore);
            return NoContent();
        }

        [HttpDelete("{Id:guid}")]
        public async Task<IActionResult> DeleteFornitore(Guid Id)
        {
            await _service.DeleteFornitoreAsync(Id);
            return NoContent();
        }

    }
}
