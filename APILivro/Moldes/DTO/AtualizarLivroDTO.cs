using System.ComponentModel.DataAnnotations;

namespace APILivro.Moldes.DTO;

public class AtualizarLivroDTO
{
    [Required]
    public string Titulo { get; set; }

    [Required]
    public string Autor { get; set; }

    [Required]
    public int AnoPublicacao { get; set; }

    [Required]
    public string Genero { get; set; }

    [Required]
    public decimal Preco { get; set; }
}
