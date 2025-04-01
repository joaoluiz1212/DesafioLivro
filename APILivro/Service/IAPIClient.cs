using APILivro.Moldes;
using APILivro.Moldes.DTO;

namespace APILivro.Service;

public interface IAPIClient
{
    Task<Livro> AdicionarLivroAsync(CriarLivroDTO livroParaAdicionar);
    Task<List<Livro>> ObterLivroAsync(string? pesquisa);
    Task<Livro> ObterLivroPorIDAsync(int id);
    Task<bool> AtualizarLivroAsync(int id, AtualizarLivroDTO livroParaAtualizar);
    Task<bool> DeletarLivroAsync(int id);

}
