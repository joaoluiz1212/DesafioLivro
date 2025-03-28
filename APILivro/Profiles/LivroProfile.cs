using APILivro.Moldes;
using APILivro.Moldes.DTO;
using AutoMapper;

namespace APILivro.Profiles;

public class LivroProfile : Profile
{

    public LivroProfile()
    {
        CreateMap<CriarLivroDTO, Livro>();
        CreateMap<Livro, AtualizarLivroDTO>();
        CreateMap<AtualizarLivroDTO, Livro>();


    }
}
