using DesafioLivrosMVC.API;
using DesafioLivrosMVC.Models;
using DesafioLivrosMVC.Models.JsonEnvio;

namespace DesafioLivrosMVC.Service;

public class LivroService
{
    private readonly APIClient _client;

    public LivroService()
    {
        _client = new APIClient();
    }

    public async Task<List<Livro>> ConsultarLivroAsync(string? pesquisa)
    {
        return await _client.ObterDadosLivroAsync(pesquisa: pesquisa);
    }

    public async Task<Livro> ObterLivroPorIDAsync(int id)
    {
        return await _client.ObterDadosLivroPorIDAsync(id: id);
    }

    public async Task IncluirLivro(Livro livro)
    {
        var livroDTO = new AdicionarLivro
        {
            Titulo = livro.Titulo,
            AnoPublicacao = livro.AnoPublicacao,
            Autor = livro.Autor,
            Genero = livro.Genero,
            Preco = livro.Preco
        };

       await _client.EnviarLivroAsync(livroDTO: livroDTO);
    }

    public async Task EditarLivroPorID(Livro livro)
    {
        var livroDTO = new AtualizarLivro
        {
            Titulo = livro.Titulo,
            AnoPublicacao = livro.AnoPublicacao,
            Autor = livro.Autor,
            Genero = livro.Genero,
            Preco = livro.Preco
        };

        await _client.AtualizarDadosDoLivroAsync(id: livro.Id, livro: livroDTO);
    }

    public async Task DeletarLivroPorID(int id)
    {
       await _client.DeletarDadosDoLivroAsync(id: id);
    }
}
