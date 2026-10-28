using APIAula01.Models;
using Microsoft.EntityFrameworkCore;
namespace APIAula01.Data;
/// <summary>
/// A "porta" da API para o banco de dados.
/// </summary>
public class AppDbContext : DbContext
{
    // As opções (qual banco usar e onde ele está) vêm do Program.cs
    public AppDbContext(DbContextOptions<AppDbContext> options) :
    base(options)
    {
    }
    // Cada DbSet vira uma tabela no banco.
    // Use como se fosse a antiga List<Jogo> do controller.
    public DbSet<Jogo> Jogos => Set<Jogo>();
}
