# API Coronavirus (.NET 8 + MongoDB)

Este repositório está em refatoração para adotar DDD (Domain-Driven Design) e Clean Architecture.

Status do projeto: EM IMPLEMENTAÇÃO/REFATORAÇÃO. A API está sendo modularizada por camadas (Domain, Application, Infrastructure e API). Um cliente web será adicionado futuramente utilizando Angular para exploração e testes dos dados expostos pela API.

---

## Sumário

-   Visão Geral
-   Estrutura de Pastas (atual)
-   Como executar (API atual)
-   Próximos passos (inclui cliente Angular)
-   Referências

---

## Visão Geral

-   ASP.NET Core 8 (Web API)
-   MongoDB (Driver oficial)
-   Organização em camadas: Domain, Application, Infrastructure e API
-   Testes com xUnit, Moq e FluentAssertions

---

## Estrutura de Pastas (atual)

Observação: diretórios de build (bin/ e obj/) e a pasta .vs/ podem existir e foram omitidos da árvore abaixo para facilitar a leitura.

```
apicoronavirus-dotnet-mongo/
├─ .github/
├─ .gitignore
├─ client/
├─ docs/
├─ server/
│  ├─ Coronavirus.sln
│  ├─ Coronavirus.API/
│  │  ├─ Controllers/
│  │  ├─ Filtres/
│  │  ├─ Middlewares/
│  │  ├─ Program.cs
│  │  └─ Coronavirus.API.csproj
│  │
│  ├─ Coronavirus.Application/
│  │  ├─ DTOs/
│  │  ├─ Mappings/
│  │  ├─ Services/
│  │  └─ Validators/
│  │
│  ├─ Coronavirus.Domain/
│  │  ├─ Entities/
│  │  ├─ Exceptions/
│  │  ├─ Interfaces/
│  │  ├─ Validators/
│  │  └─ ValueObjects/
│  │
│  └─ Coronavirus.Infrastructure/
│     ├─ Configuration/
│     ├─ CrossCutting/
│     └─ Data/
│
└─ tests/
	 ├─ Coronavirus.API.Tests/
	 ├─ Coronavirus.Application.Tests/
	 ├─ Coronavirus.Domain.Tests/
	 └─ Coronavirus.Infrastructure.Tests/
```

---

## Como executar (API atual)

Pré-requisitos:

-   .NET 8 SDK instalado
-   MongoDB local ou MongoDB Atlas

Passos:

```powershell
# Na raiz da solution
cd server

# Restaurar dependências
dotnet restore Coronavirus.sln

# Compilar
dotnet build Coronavirus.sln -c Debug

# Executar a API
dotnet run --project .\Coronavirus.API\Coronavirus.API.csproj
```

Após subir, acesse o Swagger (se configurado):

-   https://localhost:5001/swagger

---

## Próximos passos (Roadmap)

-   [Em andamento] Modularizar camadas Application e API (DTOs, Services, Controllers, Middlewares)
-   [Em andamento] Implementar repositórios e mapeamentos do MongoDB (Infrastructure)
-   [Próximo] Configurar validações com FluentValidation
-   [Próximo] Mapear entidades/DTOs com AutoMapper
-   [Próximo] Configurar CORS para o cliente web
-   [Próximo] Criar cliente web em Angular em `client/Coronavirus.Client.Web`
    -   Inicialização (Angular CLI)
    -   Integração com a API (serviços, interceptors)
    -   UI com Angular Material
-   [Opcional] Configurar CI/CD

> Este repositório está em refatoração. Estruturas, nomes e contratos podem mudar enquanto consolidamos a arquitetura.

---

## Referências

-   .NET 8: https://learn.microsoft.com/dotnet/
-   ASP.NET Core: https://learn.microsoft.com/aspnet/core/
-   MongoDB .NET Driver: https://www.mongodb.com/docs/drivers/csharp/
-   Clean Architecture: https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html
-   DDD: https://martinfowler.com/bliki/DomainDrivenDesign.html
