using Azure;
using DesafioLivrosMVC.Models;
using DesafioLivrosMVC.Models.LivroDTO;
using System.Formats.Asn1;
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

        return await ObterRequisicaoAsync<List<Livro>>($"{_baseUrl}/obter-livro");
    }

    public async Task<Livro> ObterDadosLivroPorIDAsync(int id)
    {
        return await ObterRequisicaoAsync<Livro>($"{_baseUrl}/obter-livro-por-id/{id}");

    }

    public async void EnviarLivroAsync(AdicionarLivroDTO livroDTO)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/adicionar-livro", livroDTO);
        response.EnsureSuccessStatusCode();
    }

    public async void AtualizarDadosDoLivroAsync(int id, AtualizarLivroDTO livro)
    {
        var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/atualizar-livro/{id}", livro);
        response.EnsureSuccessStatusCode();
    }

    public async void DeletarDadosDoLivroAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"{_baseUrl}/atualizar-livro/{id}");
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
