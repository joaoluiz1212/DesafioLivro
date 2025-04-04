using DesafioLivrosMVC.Models;

namespace DesafioLivrosMVC.Service;

public interface ILivroService
{
    Task<List<Livro>> ConsultarLivroAsync(string? pesquisa);
    Task<Livro> ObterLivroPorIDAsync(int id);
    Task IncluirLivro(Livro livro);
    Task EditarLivroPorID(Livro livro);
    Task DeletarLivroPorID(int id);
}
