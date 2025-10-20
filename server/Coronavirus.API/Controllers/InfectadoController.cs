using Coronavirus.Application.DTOs.Requests;
using Coronavirus.Application.DTOs.Responses;
using Coronavirus.Application.Interfaces.Services;
namespace Coronavirus.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InfectadoController(IInfectadoService service) : ControllerBase
{
    private readonly IInfectadoService _service = service;

    [HttpPost]
    [ProducesResponseType(typeof(InfectadoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Criar([FromBody] CreateInfectadoRequest request)
    {
        var resultado = await _service.CriarAsync(request);
        return CreatedAtAction(nameof(ObterPorId), new { id = resultado.Id }, resultado);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<InfectadoResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterTodos()
    {
        var resultado = await _service.ObterTodosAsync();
        return Ok(resultado);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(InfectadoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var resultado = await _service.ObterPorIdAsync(id);
        return resultado == null ? NotFound() : Ok(resultado);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(InfectadoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] UpdateInfectadoRequest request)
    {
        var resultado = await _service.AtualizarAsync(id, request);
        return Ok(resultado);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Remover(Guid id)
    {
        await _service.RemoverAsync(id);
        return NoContent();
    }
}