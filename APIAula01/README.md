# APIAula01

API REST desenvolvida em ASP.NET Core 8.0 para gerenciamento de tarefas (Todo Items) com operações CRUD completas.

## Tecnologias

- **ASP.NET Core 8.0** - Framework web
- **Entity Framework Core 8.0** - ORM para acesso a dados
- **SQL Server** - Banco de dados (LocalDB)
- **Swagger/OpenAPI** - Documentação interativa da API

## Estrutura do Projeto

```
APIAula01/
├── Controllers/          # Controladores da API
│   └── TodoItemsController.cs
├── Models/              # Modelos de dados
│   └── TodoItem.cs
├── Data/                # Contexto do Entity Framework
│   └── APIAula01Context.cs
├── Migrations/          # Migrações do banco de dados
└── Program.cs           # Configuração e inicialização da aplicação
```

## Pré-requisitos

- .NET 8.0 SDK
- SQL Server LocalDB (incluído no Visual Studio)

## Como Executar

1. Certifique-se de que o banco de dados está configurado no `appsettings.json`
2. Execute as migrações (se necessário):
   ```bash
   dotnet ef database update
   ```
3. Execute a aplicação:
   ```bash
   dotnet run
   ```
4. Acesse a documentação Swagger em: `https://localhost:{porta}/swagger`

## Endpoints da API

Base URL: `/api/TodoItems`

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/TodoItems` | Lista todas as tarefas |
| GET | `/api/TodoItems/{id}` | Obtém uma tarefa específica por ID |
| POST | `/api/TodoItems` | Cria uma nova tarefa |
| PUT | `/api/TodoItems/{id}` | Atualiza uma tarefa existente |
| DELETE | `/api/TodoItems/{id}` | Remove uma tarefa |

## Modelo de Dados

### TodoItem

```json
{
  "id": 0,
  "name": "string",
  "isComplete": false
}
```

**Propriedades:**
- `Id` (long) - Identificador único da tarefa
- `Name` (string?) - Nome/descrição da tarefa
- `IsComplete` (bool) - Indica se a tarefa está concluída

## Exemplos de Uso

### Criar uma nova tarefa
```http
POST /api/TodoItems
Content-Type: application/json

{
  "name": "Estudar ASP.NET Core",
  "isComplete": false
}
```

### Atualizar uma tarefa
```http
PUT /api/TodoItems/1
Content-Type: application/json

{
  "id": 1,
  "name": "Estudar ASP.NET Core",
  "isComplete": true
}
```

### Listar todas as tarefas
```http
GET /api/TodoItems
```

### Obter tarefa por ID
```http
GET /api/TodoItems/1
```

### Deletar uma tarefa
```http
DELETE /api/TodoItems/1
```

## Documentação Interativa (Swagger)

Durante o desenvolvimento, acesse o Swagger UI para testar os endpoints diretamente no navegador:

- URL: `https://localhost:{porta}/swagger` (modo Development)

O Swagger fornece uma interface gráfica para explorar e testar todos os endpoints da API.

