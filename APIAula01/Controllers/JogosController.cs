using APIAula01.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIAula01.Controllers;

/// <summary>
/// Versão 1: os dados ficam em uma lista na memória (sem banco de dados).
/// Tudo o que for cadastrado se perde quando a aplicação é reiniciada.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class JogosController : ControllerBase
{
    // "static" porque o ASP.NET cria um novo controller a cada requisição.
    // Sem o static, a lista seria recriada (e esvaziada) toda vez.
    private static readonly List<Jogo> _jogos =
    [
        new Jogo { Id = 1, Titulo = "The Legend of Zelda: Tears of the Kingdom", Genero = "Aventura", Plataforma = "Nintendo Switch", AnoLancamento = 2023, Zerado = true },
        new Jogo { Id = 2, Titulo = "Hollow Knight: Silksong", Genero = "Metroidvania", Plataforma = "PC", AnoLancamento = 2025, Zerado = false },
        new Jogo { Id = 3, Titulo = "EA Sports FC 26", Genero = "Esporte", Plataforma = "PlayStation 5", AnoLancamento = 2025, Zerado = false }
    ];

    // Controla o próximo Id (papel que o banco de dados fará na versão 2)
    private static int _proximoId = 4;

    // GET: api/Jogos
    [HttpGet]
    public ActionResult<IEnumerable<Jogo>> GetJogos()
    {
        return Ok(_jogos);
    }

    // GET: api/Jogos/5
    [HttpGet("{id}")]
    public ActionResult<Jogo> GetJogo(int id)
    {
        var jogo = _jogos.FirstOrDefault(j => j.Id == id);

        if (jogo is null)
        {
            return NotFound();
        }

        return Ok(jogo);
    }

    // POST: api/Jogos
    [HttpPost]
    public ActionResult<Jogo> PostJogo(Jogo jogo)
    {
        jogo.Id = _proximoId++;
        _jogos.Add(jogo);

        // 201 Created + cabeçalho Location apontando para GET api/Jogos/{id}
        return CreatedAtAction(nameof(GetJogo), new { id = jogo.Id }, jogo);
    }

    // PUT: api/Jogos/5
    [HttpPut("{id}")]
    public IActionResult PutJogo(int id, Jogo jogoAtualizado)
    {
        var jogo = _jogos.FirstOrDefault(j => j.Id == id);

        if (jogo is null)
        {
            return NotFound();
        }

        jogo.Titulo = jogoAtualizado.Titulo;
        jogo.Genero = jogoAtualizado.Genero;
        jogo.Plataforma = jogoAtualizado.Plataforma;
        jogo.AnoLancamento = jogoAtualizado.AnoLancamento;
        jogo.Zerado = jogoAtualizado.Zerado;

        return NoContent();
    }

    // DELETE: api/Jogos/5
    [HttpDelete("{id}")]
    public IActionResult DeleteJogo(int id)
    {
        var jogo = _jogos.FirstOrDefault(j => j.Id == id);

        if (jogo is null)
        {
            return NotFound();
        }

        _jogos.Remove(jogo);

        return NoContent();
    }
}
