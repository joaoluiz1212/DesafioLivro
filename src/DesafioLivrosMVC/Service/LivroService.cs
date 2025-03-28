using DesafioLivrosMVC.API;
using DesafioLivrosMVC.Models;
using DesafioLivrosMVC.Models.LivroDTO;

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

    public void IncluirLivro(Livro livro)
    {
        var livroDTO = new AdicionarLivroDTO
        {
            Titulo = livro.Titulo,
            AnoPublicacao = livro.AnoPublicacao,
            Autor = livro.Autor,
            Genero = livro.Genero,
            Preco = livro.Preco
        };

        _client.EnviarLivroAsync(livroDTO: livroDTO);
    }

    public void EditarLivroPorID(Livro livro)
    {
        var livroDTO = new AtualizarLivroDTO
        {
            Titulo = livro.Titulo,
            AnoPublicacao = livro.AnoPublicacao,
            Autor = livro.Autor,
            Genero = livro.Genero,
            Preco = livro.Preco
        };

        _client.AtualizarDadosDoLivroAsync(id: livro.Id, livro: livroDTO);
    }

    public void DeletarLivroPorID(int id)
    {
        _client.DeletarDadosDoLivroAsync(id: id);
    }
}
