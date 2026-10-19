# APIAula01 — Versão 1 (lista em memória)

API REST em **ASP.NET Core (.NET 10)** para gerenciar um catálogo de **jogos** com as operações CRUD.

Nesta primeira versão **não há banco de dados**: os jogos ficam guardados em uma lista na memória da aplicação. Isso deixa o foco no que importa primeiro — rotas, verbos HTTP e códigos de status. A consequência: tudo que for cadastrado some quando a aplicação é reiniciada (ela sempre volta com os 3 jogos iniciais).

## Tecnologias

- **ASP.NET Core / .NET 10 (LTS)** — framework web
- **Swagger (Swashbuckle)** — documentação e página para testar a API

## Estrutura

```
APIAula01/
├── Controllers/
│   └── JogosController.cs   # Endpoints da API + lista em memória
├── Models/
│   └── Jogo.cs              # Modelo de dados
├── APIAula01.http           # Requisições prontas para testar
└── Program.cs               # Configuração da aplicação
```

## Pré-requisitos

- .NET 10 SDK

## Como executar

```bash
cd APIAula01
dotnet run
```

Depois acesse o Swagger em `http://localhost:5135/swagger`, ou use o arquivo `APIAula01.http` no VS Code / Visual Studio.

## Endpoints

Base: `/api/Jogos`

| Método | Endpoint | Descrição | Respostas |
|--------|----------|-----------|-----------|
| GET | `/api/Jogos` | Lista todos os jogos | 200 |
| GET | `/api/Jogos/{id}` | Busca um jogo pelo Id | 200, 404 |
| POST | `/api/Jogos` | Cadastra um jogo | 201, 400 |
| PUT | `/api/Jogos/{id}` | Atualiza um jogo | 204, 400, 404 |
| DELETE | `/api/Jogos/{id}` | Remove um jogo | 204, 404 |

## Modelo

```json
{
  "id": 1,
  "titulo": "The Legend of Zelda: Tears of the Kingdom",
  "genero": "Aventura",
  "plataforma": "Nintendo Switch",
  "anoLancamento": 2023,
  "zerado": true
}
```

- `id` (int) — gerado pela API no cadastro
- `titulo` (string) — obrigatório
- `genero`, `plataforma` (string)
- `anoLancamento` (int) — entre 1970 e 2100
- `zerado` (bool) — se o jogo já foi concluído

## Próxima versão

Na versão 2, a lista em memória será trocada por um banco de dados com Entity Framework Core — os endpoints continuam os mesmos.
