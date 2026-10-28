using APIAula01.Data; // NOVO
using Microsoft.EntityFrameworkCore; // NOVO

var builder = WebApplication.CreateBuilder(args);

// Registra os controllers (as classes que recebem as requisições HTTP)
builder.Services.AddControllers();

// NOVO: registra o AppDbContext e diz que o banco é SQLite,
// usando a connection string do appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Swagger: gera a documentação da API e uma página para testar os endpoints
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Em desenvolvimento, habilita a página do Swagger em /swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
