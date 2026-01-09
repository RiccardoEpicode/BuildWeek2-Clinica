using BuildWeek2.Data;
using BuildWeek2.Models.Dto.Fornitore;
using BuildWeek2.Models.Dto.Prodotti;
using BuildWeek2.Models.Dto.RicoveroAnimale;
using BuildWeek2.Models.Entities;
using BuildWeek2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace BuildWeek2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RicoveroAnimaleController : ControllerBase
{
    private readonly IRicoveroAnimaleService _service;

    public RicoveroAnimaleController(IRicoveroAnimaleService service)
    {
        _service = service;
    }
    // GET: api/Animales
    [HttpGet]

    public async Task<ActionResult<IEnumerable<GetRicoveroAnimaleDto>>> GetAnimaliRicoverati()
    {
        var animaliRicoverati = await _service.GetAllAnimaliRicoverati();
        return Ok(animaliRicoverati);
    }

    [HttpGet("{Id:guid}")]

    public async Task<ActionResult<GetRicoveroAnimaleIdDto>> GetAnimaliRicoveratiById(Guid Id)
    {
        var animaleRicoverato = await _service.GetAnimaliRicoveratiById(Id);
        if (animaleRicoverato == null)
        {
            return NotFound();
        }
        return Ok(animaleRicoverato);
    }

    [HttpPost]
    public async Task<ActionResult<CreateRicoveroAnimaleDto>> CreateProdotto(CreateRicoveroAnimaleDto ricoveroAnimaleDto)
    {
        var ricoveroAnimale = new RicoveroAnimale
        {
            DataInizioRicovero = ricoveroAnimaleDto.DataInizioRicovero,
            DataFineRicovero = ricoveroAnimaleDto.DataFineRicovero,
            Attivo = ricoveroAnimaleDto.Attivo,
            AnimaleId = ricoveroAnimaleDto.AnimaleId
        };
        await _service.CreateAnimaliRicoveratiAsync(ricoveroAnimale);
        return CreatedAtAction(nameof(GetAnimaliRicoveratiById), new { Id = ricoveroAnimale.RicoveroAnimaleId }, ricoveroAnimaleDto);

    }
    //PUT
    [HttpPut("{Id:guid}")]
    public async Task<IActionResult> UpdateAnimaliRicoverati(Guid Id, UpdateRicoveroAnimaleDto ricoveroAnimaleDto)
    {
        var existingRicoveroAnimale = await _service.GetAnimaliRicoveratiById(Id);
        if (existingRicoveroAnimale == null)
        {
            return NotFound();
        }
        existingRicoveroAnimale.DataInizioRicovero = ricoveroAnimaleDto.DataInizioRicovero;
        existingRicoveroAnimale.DataFineRicovero = ricoveroAnimaleDto.DataFineRicovero;
        existingRicoveroAnimale.Attivo = ricoveroAnimaleDto.Attivo;
        await _service.UpdateAnimaliRicoveratiAsync(existingRicoveroAnimale);
        return NoContent();
    }

    [HttpDelete("{Id:guid}")]
    public async Task<IActionResult> DeleteFornitore(Guid Id)
    {
        await _service.DeleteAnimaliRicoveratiAsync(Id);
        return NoContent();
    }

}

