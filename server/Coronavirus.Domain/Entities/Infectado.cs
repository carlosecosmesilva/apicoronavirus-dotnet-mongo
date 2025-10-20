using System;
using Coronavirus.Domain.ValueObjects;

namespace Coronavirus.Domain.Entities;

public class Infectado
{
    public Guid Id { get; set; }
    public DateTime DataNascimento { get; set; }
    public Sexo Sexo { get; set; }
    public Localizacao Localizacao { get; set; } = null!;
    public DateTime DataRegistro { get; set; }
    public DateTime? DataAtualizacao { get; set; }

    public static Infectado Criar(DateTime dataNascimento, Sexo sexo, Localizacao localizacao)
    {
        return new Infectado
        {
            Id = Guid.NewGuid(),
            DataNascimento = dataNascimento,
            Sexo = sexo,
            Localizacao = localizacao,
            DataRegistro = DateTime.UtcNow
        };
    }

    public void Atualizar(Sexo sexo, double latitude, double longitude)
    {
        Sexo = sexo;
        Localizacao = new Localizacao(latitude, longitude);
        DataAtualizacao = DateTime.UtcNow;
    }
}