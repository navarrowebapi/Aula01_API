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
│   ├── TodoItemsController.cs
│   └── CategoriesController.cs
├── Models/              # Modelos de dados
│   ├── TodoItem.cs
│   └── Category.cs
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

### TodoItems
Base URL: `/api/TodoItems`

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/TodoItems` | Lista todas as tarefas (com Category incluída) |
| GET | `/api/TodoItems/{id}` | Obtém uma tarefa específica por ID (com Category incluída) |
| POST | `/api/TodoItems` | Cria uma nova tarefa |
| PUT | `/api/TodoItems/{id}` | Atualiza uma tarefa existente |
| DELETE | `/api/TodoItems/{id}` | Remove uma tarefa |

### Categories
Base URL: `/api/Categories`

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/Categories` | Lista todas as categorias (com TodoItems incluídos) |
| GET | `/api/Categories/{id}` | Obtém uma categoria específica por ID (com TodoItems incluídos) |
| POST | `/api/Categories` | Cria uma nova categoria |
| PUT | `/api/Categories/{id}` | Atualiza uma categoria existente |
| DELETE | `/api/Categories/{id}` | Remove uma categoria |

## Modelo de Dados

### TodoItem

```json
{
  "id": 0,
  "name": "string",
  "isComplete": false,
  "categoryId": 0,
  "category": {
    "id": 0,
    "name": "string",
    "description": "string"
  }
}
```

**Propriedades:**
- `Id` (long) - Identificador único da tarefa
- `Name` (string?) - Nome/descrição da tarefa
- `IsComplete` (bool) - Indica se a tarefa está concluída
- `CategoryId` (int?) - Chave estrangeira para Category (opcional)
- `Category` (Category?) - Propriedade de navegação para a categoria relacionada

### Category

```json
{
  "id": 0,
  "name": "string",
  "description": "string",
  "todoItems": [
    {
      "id": 0,
      "name": "string",
      "isComplete": false
    }
  ]
}
```

**Propriedades:**
- `Id` (int) - Identificador único da categoria
- `Name` (string) - Nome da categoria
- `Description` (string?) - Descrição da categoria (opcional)
- `TodoItems` (ICollection<TodoItem>) - Coleção de TodoItems relacionados

## Relacionamentos entre Models

Este projeto demonstra como criar relacionamentos entre models usando Entity Framework Core. O relacionamento implementado é **1:N (Um para Muitos)** entre `Category` e `TodoItem`.

### Por que usar relacionamentos?

Relacionamentos entre models permitem:
- **Organização de dados**: Agrupar dados relacionados logicamente
- **Integridade referencial**: Manter consistência entre tabelas
- **Consultas eficientes**: Acessar dados relacionados facilmente
- **Redução de redundância**: Evitar duplicação de dados

### Como funciona o relacionamento

#### 1. **Chave Estrangeira (Foreign Key)**
No modelo `TodoItem`, adicionamos a propriedade `CategoryId` que armazena o ID da categoria:

```csharp
public int? CategoryId { get; set; }  // Chave estrangeira (nullable)
```

#### 2. **Propriedades de Navegação**
As propriedades de navegação permitem acessar objetos relacionados:

**No TodoItem (lado "muitos"):**
```csharp
public virtual Category? Category { get; set; }  // Navegação para uma Category
```

**Na Category (lado "um"):**
```csharp
public virtual ICollection<TodoItem> TodoItems { get; set; }  // Navegação para muitos TodoItems
```

#### 3. **Configuração no DbContext**
No método `OnModelCreating` do `APIAula01Context`, configuramos o relacionamento:

```csharp
modelBuilder.Entity<TodoItem>()
    .HasOne(t => t.Category)              // TodoItem tem uma Category
    .WithMany(c => c.TodoItems)           // Category tem muitos TodoItems
    .HasForeignKey(t => t.CategoryId)     // Chave estrangeira
    .OnDelete(DeleteBehavior.SetNull);    // Comportamento ao deletar
```

**Explicação da configuração:**
- `HasOne()`: Define que TodoItem tem uma Category
- `WithMany()`: Define que Category tem muitos TodoItems
- `HasForeignKey()`: Especifica qual propriedade é a chave estrangeira
- `OnDelete(DeleteBehavior.SetNull)`: Se uma categoria for deletada, `CategoryId` nos TodoItems vira `null` (relacionamento opcional)

#### 4. **Carregamento de Dados Relacionados (Eager Loading)**
Para carregar dados relacionados nas consultas, usamos `Include()`:

```csharp
// Carregar TodoItems com suas Categories
var todoItems = await _context.TodoItem
    .Include(t => t.Category)
    .ToListAsync();

// Carregar Category com seus TodoItems
var category = await _context.Category
    .Include(c => c.TodoItems)
    .FirstOrDefaultAsync(c => c.Id == id);
```

### Tipos de Relacionamentos

1. **1:N (Um para Muitos)** - Implementado neste projeto
   - Uma Category tem muitos TodoItems
   - Um TodoItem tem uma Category (ou nenhuma)

2. **N:N (Muitos para Muitos)**
   - Exemplo: TodoItem pode ter muitas Tags, e Tag pode estar em muitos TodoItems
   - Requer uma tabela intermediária (junction table)

3. **1:1 (Um para Um)**
   - Exemplo: TodoItem pode ter um TodoItemDetail
   - Cada entidade tem referência única à outra

### Exemplos Práticos

#### Criar um TodoItem com Category:
```http
POST /api/TodoItems
Content-Type: application/json

{
  "name": "Estudar Entity Framework",
  "isComplete": false,
  "categoryId": 1
}
```

#### Criar uma Category:
```http
POST /api/Categories
Content-Type: application/json

{
  "name": "Estudos",
  "description": "Tarefas relacionadas a estudos"
}
```

#### Obter Category com seus TodoItems:
```http
GET /api/Categories/1
```

Retorna a Category com todos os TodoItems relacionados incluídos no JSON.

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

