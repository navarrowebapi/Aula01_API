var builder = WebApplication.CreateBuilder(args);

// Registra os controllers (as classes que recebem as requisições HTTP)
builder.Services.AddControllers();

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
