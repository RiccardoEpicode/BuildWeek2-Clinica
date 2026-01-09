using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuildWeek2.Data;
using BuildWeek2.Models.Dto.Animale;
using BuildWeek2.Services.Interfaces;

namespace BuildWeek2.Controllers;
// API controller for managing "Animale" entities

[Route("api/[controller]")]
[ApiController]
public class AnimaleController : ControllerBase
{
    private readonly IAnimaleService _service;

    public AnimaleController(IAnimaleService service)
    {
        _service = service;
    }

    // GET: api/Animales
    [HttpGet]

    public async Task<ActionResult<IEnumerable<GetAnimaleIdDto>>> GetAnimali()
    {
        var animali = await _service.GetAllAnimaliAsync();
        return Ok(animali);
    }

    [HttpGet("{Id:guid}")]

    public async Task<ActionResult<Animale>> GetAnimaleById(Guid Id)
    {
        var animale = await _service.GetByIdAsNoTracking(Id);
        if (animale == null)
        {
            return NotFound();
        }
        return Ok(animale);
    }

    [HttpPost]
    public async Task<ActionResult<CreateAnimaleDto>> CreateAnimale(CreateAnimaleDto animaleDto)
    {
        var animale = new Animale
        {
            DataRegistrazione = animaleDto.DataRegistrazione,
            Nome = animaleDto.Nome,
            Tipologia = animaleDto.Tipologia,
            ColoreMantello = animaleDto.ColoreMantello,
            DataNascita = animaleDto.DataNascita,
            PresenzaMicrochip = animaleDto.PresenzaMicrochip,
            NumeroMicrochip = animaleDto.NumeroMicrochip,
            Proprietario = animaleDto.Proprietario
        };
        var createdAnimale = await _service.CreateAnimaleAsync(animale);
        return CreatedAtAction(nameof(GetAnimaleById), new { Id = createdAnimale.AnimaleId }, createdAnimale);

    }
    //PUT
    [HttpPut("{Id:guid}")]

    public async Task<IActionResult> UpdateAnimale(Guid Id, UpdateAnimaleDto animaleDto)
    {
      
        var existingAnimale = await _service.GetByIdAsNoTracking(Id);
        if (existingAnimale == null)
        {
            return NotFound();
        }
        existingAnimale.DataRegistrazione = animaleDto.DataRegistrazione;
        existingAnimale.Nome = animaleDto.Nome;
        existingAnimale.Tipologia = animaleDto.Tipologia;
        existingAnimale.ColoreMantello = animaleDto.ColoreMantello;
        existingAnimale.DataNascita = animaleDto.DataNascita;
        existingAnimale.PresenzaMicrochip = animaleDto.PresenzaMicrochip;
        existingAnimale.NumeroMicrochip = animaleDto.NumeroMicrochip;
        existingAnimale.Proprietario = animaleDto.Proprietario;
        await _service.Save(existingAnimale);
        return NoContent();

    }

    [HttpDelete("{Id:guid}")]
    public async Task<IActionResult> DeleteAnimale(Guid Id)
    {
        await _service.DeleteAnimaleAsync(Id);
        return NoContent();
    }

}
