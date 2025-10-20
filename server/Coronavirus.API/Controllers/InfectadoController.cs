using Coronavirus.Application.DTOs.Requests;
using Coronavirus.Application.DTOs.Responses;
using Coronavirus.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace Coronavirus.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InfectadoController(IInfectadoService service) : ControllerBase
{
    private readonly IInfectadoService _service = service;

    /// <summary>
    /// Adiciona um registro de infectado.
    /// </summary>
    /// <param name="request">Dados do infectado</param>
    /// <returns>Dados do infectado criado</returns>
    [HttpPost]
    [ProducesResponseType(typeof(InfectadoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Criar([FromBody] CreateInfectadoRequest request)
    {
        var resultado = await _service.CriarAsync(request);
        return CreatedAtAction(nameof(ObterPorId), new { id = resultado.Id }, resultado);
    }

    /// <summary>
    /// Obtem todos os registros de infectados.
    /// </summary>
    /// <returns>Lista com todos os infectados cadastrados</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<InfectadoResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterTodos()
    {
        var resultado = await _service.ObterTodosAsync();
        return Ok(resultado);
    }

    /// <summary>
    /// Obtem um registro de infectado pelo Id.
    /// </summary>
    /// <param name="id">Id do infectado</param>
    /// <returns>Dados do infectado</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(InfectadoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var resultado = await _service.ObterPorIdAsync(id);
        return resultado == null ? NotFound() : Ok(resultado);
    }

    /// <summary>
    /// Atualiza um registro de infectado.
    /// </summary>
    /// <param name="id">Id do infectado</param>
    /// <param name="request">Dados para atualização</param>
    /// <returns></returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(InfectadoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] UpdateInfectadoRequest request)
    {
        var resultado = await _service.AtualizarAsync(id, request);
        return Ok(resultado);
    }

    /// <summary>
    /// Remove um registro de infectado.
    /// </summary>
    /// <param name="id">Id do infectado</param>
    /// <returns>Retorna um status 204 No Content</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Remover(Guid id)
    {
        await _service.RemoverAsync(id);
        return NoContent();
    }
}