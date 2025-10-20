using AutoMapper;
using Coronavirus.Application.DTOs.Requests;
using Coronavirus.Application.DTOs.Responses;
using Coronavirus.Application.Interfaces;
using Coronavirus.Domain.Entities;
using Coronavirus.Domain.Interfaces;
using Coronavirus.Domain.ValueObjects;
using FluentValidation;

namespace Coronavirus.Application.Services;

public class InfectadoService(
    IInfectadoRepository repo,
    IMapper mapper,
    IValidator<CreateInfectadoRequest> createValidator,
    IValidator<UpdateInfectadoRequest> updateValidator) : IInfectadoService
{
    private readonly IInfectadoRepository _repo = repo;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<CreateInfectadoRequest> _createValidator = createValidator;
    private readonly IValidator<UpdateInfectadoRequest> _updateValidator = updateValidator;

    public async Task<InfectadoResponse> CriarAsync(CreateInfectadoRequest request)
    {
        await _createValidator.ValidateAndThrowAsync(request);
        var localizacao = new Localizacao(request.Latitude, request.Longitude);
        var entity = Infectado.Criar(request.DataNascimento, request.Sexo, localizacao);
        await _repo.AdicionarAsync(entity);
        return _mapper.Map<InfectadoResponse>(entity);
    }

    public async Task<IEnumerable<InfectadoResponse>> ObterTodosAsync()
    {
        var itens = await _repo.ObterTodosAsync();
        return itens.Select(_mapper.Map<InfectadoResponse>);
    }

    public async Task<InfectadoResponse?> ObterPorIdAsync(Guid id)
    {
        var item = await _repo.ObterPorIdAsync(id);
        return item is null ? null : _mapper.Map<InfectadoResponse>(item);
    }

    public async Task<InfectadoResponse> AtualizarAsync(Guid id, UpdateInfectadoRequest request)
    {
        await _updateValidator.ValidateAndThrowAsync(request);
        var entity = await _repo.ObterPorIdAsync(id) 
            ?? throw new KeyNotFoundException("Infectado não encontrado");
        entity.Atualizar(request.Sexo, request.Latitude, request.Longitude);
        await _repo.AtualizarAsync(entity);
        return _mapper.Map<InfectadoResponse>(entity);
    }

    public async Task RemoverAsync(Guid id)
    {
        var entity = await _repo.ObterPorIdAsync(id) 
            ?? throw new KeyNotFoundException("Infectado não encontrado");
        await _repo.RemoverAsync(id);
    }
}
