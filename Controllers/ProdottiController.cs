using BuildWeek2.Data;
using BuildWeek2.Models.Dto.Animale;
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
    public class ProdottiController : ControllerBase
    {
        private readonly IProdottiService _service;

        public ProdottiController(IProdottiService service)
        {
            _service = service;
        }
        // GET: api/Animales
        [HttpGet]

        public async Task<ActionResult<IEnumerable<GetProdottiDto>>> GetProdotti()
        {
            var prodotti = await _service.GetAllProducts();
            return Ok(prodotti);
        }

        [HttpGet("{Id:guid}")]

        public async Task<ActionResult<GetProdottiIdDto>> GetProdottoById(Guid Id)
        {
            var prodotto = await _service.GetProdottoById(Id);
            if (prodotto == null)
            {
                return NotFound();
            }
            return Ok(prodotto);
        }

        [HttpPost]
        public async Task<ActionResult<CreateProdottiDto>> CreateProdotto(CreateProdottiDto prodottoDto)
        {
            var prodotto = new Prodotti
            {
                NomeProdotto = prodottoDto.NomeProdotto,
                Medicinale = prodottoDto.Medicinale,
                Usi = prodottoDto.Usi,
                CodiceArmadietto = prodottoDto.CodiceArmadietto,
                CodiceCassetto = prodottoDto.CodiceCassetto
            };
            var createdProdotto = await _service.CreateProdottoAsync(prodotto);
            return CreatedAtAction(nameof(GetProdottoById), new { Id = createdProdotto.ProdottiId }, createdProdotto);


        }
        //PUT
        [HttpPut("{Id:guid}")]
        public async Task<IActionResult> UpdateProdotto(Guid Id, UpdateProdottiDto prodottoDto)
        {
            var existingProdotto = await _service.GetProdottoById(Id);
            if (existingProdotto == null)
            {
                return NotFound();
            }
            existingProdotto.NomeProdotto = prodottoDto.NomeProdotto;
            existingProdotto.Medicinale = prodottoDto.Medicinale;
            existingProdotto.Usi = prodottoDto.Usi;
            existingProdotto.CodiceArmadietto = prodottoDto.CodiceArmadietto;
            existingProdotto.CodiceCassetto = prodottoDto.CodiceCassetto;
            await _service.UpdateProductAsync(existingProdotto);
            return NoContent();
        }

        [HttpDelete("{Id:guid}")]
        public async Task<IActionResult> DeleteProdotto(Guid Id)
        {
            await _service.DeleteProdottoAsync(Id);
            return NoContent();
        }

    }
}
