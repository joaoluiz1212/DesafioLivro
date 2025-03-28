using System.Text.Json.Serialization;

namespace DesafioLivrosMVC.Models;

public class Livro
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("titulo")]
    public string Titulo { get; set; }

    [JsonPropertyName("autor")]
    public string Autor { get; set; }

    [JsonPropertyName("anoPublicacao")]
    public int AnoPublicacao { get; set; }

    [JsonPropertyName("genero")]
    public string Genero { get; set; }

    [JsonPropertyName("preco")]
    public decimal Preco {  get; set; } 


}

