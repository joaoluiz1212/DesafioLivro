using DesafioLivrosMVC.API;
using DesafioLivrosMVC.Interface;
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
        try
        {
            return await _client.ObterDadosLivroAsync(pesquisa: pesquisa);

        }

        catch (HttpRequestException ex)
        {
            throw new ServiceException("Erro ao consultar o Livro", ex);
        }
    }

    public async Task<Livro> ObterLivroPorIDAsync(int id)
    {
        try
        {
            return await _client.ObterDadosLivroPorIDAsync(id: id);

        }

        catch (HttpRequestException ex)
        {
            throw new ServiceException("Erro ao consultar o Livro por ID", ex);
        }
    }

    public async Task IncluirLivro(Livro livro)
    {
        try
        {
            var ObjetoEnvio = MontarObjetoEnvio(livro);

            await _client.EnviarLivroAsync(ObjetoEnvio: ObjetoEnvio);
        }

        catch (HttpRequestException ex)
        {
            throw new ServiceException("Erro ao Incluir o Livro", ex);
        }
    }

    public async Task EditarLivroPorID(Livro livro)
    {
        try
        {
            var ObjetoEnvio = MontarObjetoEnvio(livro);

            await _client.AtualizarDadosDoLivroAsync(id: livro.Id, ObjetoEnvio: ObjetoEnvio);
        }

        catch (HttpRequestException ex)
        {
            throw new ServiceException("Erro ao Editar o Livro", ex);
        }
    }

    public async Task DeletarLivroPorID(int id)
    {
        try
        {
            await _client.DeletarDadosDoLivroAsync(id: id);

        }

        catch (HttpRequestException ex)
        {
            throw new ServiceException("Erro ao Deletar o Livro", ex);
        }
    }

    private LivroEnvio MontarObjetoEnvio(Livro livro)
    {
        return new LivroEnvio
        {
            Titulo = livro.Titulo,
            AnoPublicacao = livro.AnoPublicacao,
            Autor = livro.Autor,
            Genero = livro.Genero,
            Preco = livro.Preco
        };
    }
}
