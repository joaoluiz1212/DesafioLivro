using DesafioLivrosMVC.Models;
using DesafioLivrosMVC.Models.JsonEnvio;

namespace DesafioLivrosMVC.API;

public interface IAPIClient
{
    Task<List<Livro>> ObterDadosLivroAsync(string? pesquisa);
    Task<Livro> ObterDadosLivroPorIDAsync(int id);
    Task EnviarLivroAsync(LivroEnvio ObjetoEnvio);
    Task AtualizarDadosDoLivroAsync(int id, LivroEnvio ObjetoEnvio);
    Task DeletarDadosDoLivroAsync(int id);
}
