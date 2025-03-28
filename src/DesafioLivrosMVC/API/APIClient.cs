using Azure;
using DesafioLivrosMVC.Models;
using DesafioLivrosMVC.Models.JsonEnvio;
using System.Text.Json;

namespace DesafioLivrosMVC.API;

public class APIClient
{
    private readonly HttpClient _httpClient;
    private string _baseUrl = "http://localhost:5070/api";

    public APIClient()
    {
        _httpClient = new HttpClient();
    }

    public async Task<List<Livro>> ObterDadosLivroAsync(string? pesquisa)
    {
        if (!string.IsNullOrEmpty(pesquisa))
        {
            _baseUrl = $"{_baseUrl}/obter-livro?pesquisa={pesquisa}";
        }
        else
            _baseUrl = $"{_baseUrl}/obter-livro";

        return await ObterRequisicaoAsync<List<Livro>>(_baseUrl);
    }

    public async Task<Livro> ObterDadosLivroPorIDAsync(int id)
    {
        return await ObterRequisicaoAsync<Livro>($"{_baseUrl}/obter-livro-por-id/{id}");

    }

    public async Task EnviarLivroAsync(AdicionarLivro livroDTO)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/adicionar-livro", livroDTO);
        response.EnsureSuccessStatusCode();
    }

    public async Task AtualizarDadosDoLivroAsync(int id, AtualizarLivro livro)
    {
        var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/atualizar-livro/{id}", livro);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeletarDadosDoLivroAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"{_baseUrl}/deletar-livro/{id}");
        response.EnsureSuccessStatusCode();
    }

    private async Task<T> ObterRequisicaoAsync<T>(string url)
    {
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(content);
    }


}
