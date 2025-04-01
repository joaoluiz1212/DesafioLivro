using APILivro.Moldes;
using APILivro.Moldes.DTO;
using APILivro.Service;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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

        var novoLivro = await _ApiService.AdicionarLivroAsync(livroParaAdicionar: livroParaAdicionar);

        if (novoLivro == null)
            return NotFound();

        return CreatedAtAction(nameof(ObterLivroPorID), new { id = novoLivro.Id }, novoLivro);
    }

    [HttpGet]
    [Route("obter-livro")]
    public async Task<ActionResult> ObterLivro([FromQuery] string? pesquisa)
    {
        var livro = await _ApiService.ObterLivroAsync(pesquisa: pesquisa);

        if (livro == null)
            return NotFound();

        return Ok(livro);
    }

    [HttpGet]
    [Route("obter-livro-por-id/{id}")]
    public async Task<ActionResult> ObterLivroPorID(int id)
    {
        var livro = await _ApiService.ObterLivroPorIDAsync(id: id);

        if (livro == null)
            return NotFound();

        return Ok(livro);
    }

    [HttpPut()]
    [Route("atualizar-livro/{id}")]
    public async Task<ActionResult> AtualizarLivro(int id, AtualizarLivroDTO livroParaAtualizar)
    {
        var retornoAtualizarLivro = await _ApiService.AtualizarLivroAsync(id: id, livroParaAtualizar: livroParaAtualizar);

        if (!retornoAtualizarLivro)
            return NotFound();

        return NoContent();
    }

    [HttpDelete()]
    [Route("deletar-livro/{id}")]
    public async Task<ActionResult> DeletarLivro(int id)
    {
        var livro = await _ApiService.DeletarLivroAsync(id: id);

        if (livro == false)
            return NotFound();

        return NoContent();
    }
}
