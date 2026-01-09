
using BuildWeek2.Data;
using BuildWeek2.Models.Dto.RicoveroAnimale;
using BuildWeek2.Models.Dto.RicoveroAnimaleSmarrito;
using BuildWeek2.Models.Entities;
using BuildWeek2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace BuildWeek2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RicoveroAnimaleSmarritoController : ControllerBase
    {
        private readonly IRicoveroAnimaleSmarritoService _service;

        public RicoveroAnimaleSmarritoController(IRicoveroAnimaleSmarritoService service)
        {
            _service = service;
        }
        // GET: api/Animales
        [HttpGet]

        public async Task<ActionResult<IEnumerable<GetAnimaleSmarritoDto>>> GetAnimaliSmarritiRicoverati()
        {
            var animaliSmarritiRicoverati = await _service.GetAllAnimaliRicoverati();
            return Ok(animaliSmarritiRicoverati);
        }

        [HttpGet("{Id:guid}")]

        public async Task<ActionResult<GetAnimaleSmarritoIdDto>> GetAnimaliSmarritiRicoveratiById(Guid Id)
        {
            var animaleSmarritoRicoverato = await _service.GetAnimaliSmarritiRicoveratiById(Id);
            if (animaleSmarritoRicoverato == null)
            {
                return NotFound();
            }
            return Ok(animaleSmarritoRicoverato);
        }

        [HttpPost]
        public async Task<ActionResult<CreateAnimaleSmarritoDto>> CreateAnimaleSmarritoRicoverato(CreateAnimaleSmarritoDto animaleSmarritoDto)
        {
            var ricoveroAnimaleSmarrito = new RicoveroAnimaleSmarrito
            {
                DataInizioRicoveroSmarrito = animaleSmarritoDto.DataInizioRicoveroSmarrito,
                DataFineRicoveroSmarrito = animaleSmarritoDto.DataFineRicoveroSmarrito,
                Tipologia = animaleSmarritoDto.Tipologia,
                ColoreMantello = animaleSmarritoDto.ColoreMantello,
                DataNascita = animaleSmarritoDto.DataNascita,
                NumeroMicrochip = animaleSmarritoDto.NumeroMicrochip,
                Attivo = animaleSmarritoDto.Attivo
            };
            await _service.CreateAnimaliSmarritiRicoveratiAsync(ricoveroAnimaleSmarrito);
            return CreatedAtAction(nameof(GetAnimaliSmarritiRicoveratiById), new { Id = ricoveroAnimaleSmarrito.RicoveroAnimaleSmarritoId }, animaleSmarritoDto);

        }
        //PUT
        [HttpPut("{Id:guid}")]
        public async Task<IActionResult> UpdateAnimaliRicoverati(Guid Id, UpdateAnimaleSmarritoDto animaleSmarritoDto)
        {
            var existingRicoveroAnimale = await _service.GetAnimaliSmarritiRicoveratiById(Id);
            if (existingRicoveroAnimale == null)
            {
                return NotFound();
            }
            existingRicoveroAnimale.DataInizioRicoveroSmarrito = animaleSmarritoDto.DataInizioRicoveroSmarrito;
            existingRicoveroAnimale.DataFineRicoveroSmarrito = animaleSmarritoDto.DataFineRicoveroSmarrito;
            existingRicoveroAnimale.Tipologia = animaleSmarritoDto.Tipologia;
            existingRicoveroAnimale.ColoreMantello = animaleSmarritoDto.ColoreMantello;
            existingRicoveroAnimale.DataNascita = animaleSmarritoDto.DataNascita;
            existingRicoveroAnimale.NumeroMicrochip = animaleSmarritoDto.NumeroMicrochip;
            existingRicoveroAnimale.Attivo = animaleSmarritoDto.Attivo;
            await _service.UpdateAnimaliSmarritiRicoveratiAsync(existingRicoveroAnimale);
            return NoContent();

        }

        [HttpDelete("{Id:guid}")]
        public async Task<IActionResult> DeleteFornitore(Guid Id)
        {
            await _service.DeleteAnimaliRicoveratiAsync(Id);
            return NoContent();
        }
    }
}
