using System.Text.Json;
using DesafioLivrosMVC.Models;
using DesafioLivrosMVC.Exceptions;
using DesafioLivrosMVC.Models.JsonEnvio;

namespace DesafioLivrosMVC.API;

public class APIClient : IAPIClient
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public APIClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _baseUrl = configuration["ApiBaseUrl"] ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<List<Livro>> ObterDadosLivroAsync(string? pesquisa)
    {
        try
        {
            if (!string.IsNullOrEmpty(pesquisa))
            {
                return await ObterRequisicaoAsync<List<Livro>>($"{_baseUrl}/obter-livro?pesquisa={pesquisa}");
            }
            else
                return await ObterRequisicaoAsync<List<Livro>>($"{_baseUrl}/obter-livro");
        }

        catch (HttpRequestException ex)
        {
            throw new ApiRequestException("Erro ao realizar a requisição no método obter-livro da API", ex);
        }
    }

    public async Task<Livro> ObterDadosLivroPorIDAsync(int id)
    {
        try
        {
            return await ObterRequisicaoAsync<Livro>($"{_baseUrl}/obter-livro-por-id/{id}");

        }

        catch (HttpRequestException ex)
        {
            throw new ApiRequestException("Erro ao realizar a requisição no método obter-livro-por-id da API", ex);
        }
    }

    public async Task EnviarLivroAsync(LivroEnvio ObjetoEnvio)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/adicionar-livro", ObjetoEnvio);
            response.EnsureSuccessStatusCode();
        }

        catch (HttpRequestException ex)
        {
            throw new ApiRequestException("Erro ao realizar a requisição no método adicionar-livro da API", ex);
        }
    }

    public async Task AtualizarDadosDoLivroAsync(int id, LivroEnvio ObjetoEnvio)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/atualizar-livro/{id}", ObjetoEnvio);
            response.EnsureSuccessStatusCode();
        }

        catch (HttpRequestException ex)
        {
            throw new ApiRequestException("Erro ao realizar a requisição no método atualizar-livro da API", ex);
        }
    }

    public async Task DeletarDadosDoLivroAsync(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/deletar-livro/{id}");
            response.EnsureSuccessStatusCode();
        }

        catch (HttpRequestException ex)
        {
            throw new ApiRequestException("Erro ao realizar a requisição no método deletar-livro da API", ex);
        }
    }

    private async Task<T> ObterRequisicaoAsync<T>(string url)
    {
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(content);
    }
}
