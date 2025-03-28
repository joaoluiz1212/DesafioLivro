using System.ComponentModel.DataAnnotations;

namespace DesafioLivrosMVC.Models.LivroDTO;

public class AdicionarLivroDTO
{
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public int AnoPublicacao { get; set; }
    public string Genero { get; set; }
    public decimal Preco { get; set; }
}
