using APILivro.Service;
using APILivro.Exceptions;
using APILivro.Moldes.DTO;
using Microsoft.AspNetCore.Mvc;

namespace APILivro.Controllers;

[ApiController]
[Route("api")]
public class LivroControllerAPI : ControllerBase
{
    private readonly IAPIClient _ApiService;

    public LivroControllerAPI(IAPIClient service)
    {
        _ApiService = service;

    }

    [HttpPost]
    [Route("adicionar-livro")]
    public async Task<ActionResult> AdicionarLivro([FromBody] CriarLivroDTO livroParaAdicionar)
    {
        try
        {
            var novoLivro = await _ApiService.AdicionarLivroAsync(livroParaAdicionar: livroParaAdicionar);

            if (novoLivro == null)
                return NotFound();

            return CreatedAtAction(nameof(ObterLivroPorID), new { id = novoLivro.Id }, novoLivro);
        }

        catch (SericeException ex)
        {
            return BadRequest($"Erro ao adicionar o livro {livroParaAdicionar.Titulo}: {ex.Message}");
        }

    }

    [HttpGet]
    [Route("obter-livro")]
    public async Task<ActionResult> ObterLivro([FromQuery] string? pesquisa)
    {
        try
        {
            var livro = await _ApiService.ObterLivroAsync(pesquisa: pesquisa);

            if (livro == null)
                return NotFound();

            return Ok(livro);
        }
        catch (SericeException ex)
        {
            return BadRequest($"Erro ao obter os dados do livro: {ex.Message}");
        }

    }

    [HttpGet]
    [Route("obter-livro-por-id/{id}")]
    public async Task<ActionResult> ObterLivroPorID(int id)
    {
        try
        {
            var livro = await _ApiService.ObterLivroPorIDAsync(id: id);

            if (livro == null)
                return NotFound();

            return Ok(livro);
        }
        catch (SericeException ex)
        {
            return BadRequest($"Erro ao obter os dados por id do livro: {ex.Message}");
        }
    }

    [HttpPut()]
    [Route("atualizar-livro/{id}")]
    public async Task<ActionResult> AtualizarLivro(int id, AtualizarLivroDTO livroParaAtualizar)
    {
        try
        {
            var retornoAtualizarLivro = await _ApiService.AtualizarLivroAsync(id: id, livroParaAtualizar: livroParaAtualizar);

            if (!retornoAtualizarLivro)
                return NotFound();

            return NoContent();
        }
        catch (SericeException ex)
        {
            return BadRequest($"Erro ao atualizar o livro {livroParaAtualizar.Titulo}: {ex.Message}");
        }
    }

    [HttpDelete()]
    [Route("deletar-livro/{id}")]
    public async Task<ActionResult> DeletarLivro(int id)
    {
        try
        {
            var livro = await _ApiService.DeletarLivroAsync(id: id);

            if (livro == false)
                return NotFound();

            return NoContent();
        }
        catch (SericeException ex)
        {
            return BadRequest($"Erro ao deletar o livro: {ex.Message}");
        }
    }
}
