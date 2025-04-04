using APILivro.Moldes;
using APILivro.Moldes.DTO;
using APILivro.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace APILivro.Service;

public class APIService : IAPIClient
{
    private IMapper _mapper;
    private readonly Context _context;

    public APIService(Context contexto, IMapper mapper)
    {
        _context = contexto;
        _mapper = mapper;
    }

    public async Task<Livro> AdicionarLivroAsync(CriarLivroDTO livroParaAdicionar)
    {
        var livro = _mapper.Map<Livro>(livroParaAdicionar);

        await _context.Livros.AddAsync(livro);
        await _context.SaveChangesAsync();

        return livro;
    }

    public async Task<List<Livro>> ObterLivroAsync(string? pesquisa)
    {
        var livro = string.IsNullOrEmpty(pesquisa)
     ? await _context.Livros.ToListAsync()
     : await _context.Livros.Where(t => t.Titulo.Contains(pesquisa) || t.Autor.Contains(pesquisa)).AsNoTracking().ToListAsync();

        return livro;
    }

    public async Task<Livro> ObterLivroPorIDAsync(int id)
    {
        return await _context.Livros.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<bool> AtualizarLivroAsync(int id, AtualizarLivroDTO livroParaAtualizar)
    {
        var livro = await ObterLivroPorIDAsync(id);

        if (livro == null)
            return false;

        _mapper.Map(livroParaAtualizar, livro);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeletarLivroAsync(int id)
    {
        var livro = await ObterLivroPorIDAsync(id: id);

        if (livro == null)
            return false;

        _context.Remove(livro);
        await _context.SaveChangesAsync();

        return true;
    }
}
