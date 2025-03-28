using Microsoft.EntityFrameworkCore;

namespace APILivro.Moldes;

public class Context : DbContext
{

    public Context(DbContextOptions<Context> options) : base(options) { }

    public DbSet<Livro> Livros { get; set; }
}
