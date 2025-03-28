using System.ComponentModel.DataAnnotations;

namespace APILivro.Moldes;

public class Livro
{
    [Key]
    [Required]
    public int Id { get; set; }

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
