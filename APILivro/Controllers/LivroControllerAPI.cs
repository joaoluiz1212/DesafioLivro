using APILivro.Moldes;
using APILivro.Moldes.DTO;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APILivro.Controllers;

[ApiController]
[Route("api")]
public class LivroControllerAPI : ControllerBase
{
    private readonly Context _context;
    private IMapper _mapper;

    public LivroControllerAPI(Context contexto, IMapper mapper)
    {
        _context = contexto;
        _mapper = mapper;

    }

    [HttpPost]
    [Route("adicionar-livro")]
    public ActionResult AdicionarLivro([FromBody] CriarLivroDTO livroParaAdicionar)
    {
        Livro livro = _mapper.Map<Livro>(livroParaAdicionar);

        _context.Livros.Add(livro);
        _context.SaveChanges();

        return Ok();
    }

    [HttpGet]
    [Route("obter-livro")]
    public ActionResult ObterLivro([FromQuery] string? pesquisa)
    {
        var livro = string.IsNullOrEmpty(pesquisa)
            ? _context.Livros.ToList()
            : _context.Livros.Where(t => t.Titulo.Contains(pesquisa) || t.Autor.Contains(pesquisa)).ToList();

        return Ok(livro);
    }

    [HttpGet]
    [Route("obter-livro-por-id/{id}")]
    public ActionResult ObterLivroPorID(int id)
    {
        var livro = _context.Livros.FirstOrDefault(t => t.Id == id);

        if (livro == null)
            return NotFound();

        return Ok(livro);
    }

    [HttpPut()]
    [Route("atualizar-livro/{id}")]
    public ActionResult AtualizarLivro(int id, AtualizarLivroDTO livroParaAtualizar)
    {
        var livro = _context.Livros.FirstOrDefault(t => t.Id == id);

        if (livro == null)
        {
            return NoContent();
        }

        _mapper.Map(livroParaAtualizar, livro);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPatch()]
    [Route("atualizar-livro-parcial/{id}")]
    public ActionResult AtualizarLivroParcial(int id, JsonPatchDocument<AtualizarLivroDTO> patchLivro)
    {
        var livro = _context.Livros.FirstOrDefault(t => t.Id == id);

        if (livro == null)
        {
            return NoContent();
        }

        var livroParaAtualizar = _mapper.Map<AtualizarLivroDTO>(livro);

        patchLivro.ApplyTo(livroParaAtualizar, ModelState);

        if (!TryValidateModel(livroParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(livroParaAtualizar, livro);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete()]
    [Route("deletar-livro/{id}")]
    public ActionResult DeletarLivro(int id)
    {
        var livro = _context.Livros.FirstOrDefault(tt => tt.Id == id);

        if (livro == null)
            return NotFound();

        _context.Remove(livro);
        _context.SaveChanges();

        return NoContent();
    }
}
