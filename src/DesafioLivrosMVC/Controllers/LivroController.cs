using DesafioLivrosMVC.Models;
using DesafioLivrosMVC.Service;
using Microsoft.AspNetCore.Mvc;

namespace DesafioLivrosMVC.Controllers;

public class LivroController : Controller
{
    private readonly LivroService _service;

    public LivroController()
    {
        _service = new LivroService();
    }


    public ActionResult Index(string? pesquisa)
    {
        var livros = _service.ConsultarLivroAsync(pesquisa).Result;

        return View(livros);
    }

    public ActionResult CriarLivro()
    {
        return View();
    }

    [HttpPost]
    public ActionResult AdicionarLivro(Livro livro)
    {
        if (!ModelState.IsValid)
        {
            return View(livro);
        }
        _service.IncluirLivro(livro: livro);

        return View("Index");
    }

    public ActionResult EditarLivro(int Id)
    {
        var livro = _service.ObterLivroPorIDAsync(id: Id).Result;

        return View(livro);
    }

    [HttpPost]
    public ActionResult AtualizarLivro(Livro livro)
    {
        if (!ModelState.IsValid)
        {
            return View(livro);
        }
        _service.EditarLivroPorID(livro: livro);
        return View("Index");
    }
    public ActionResult DeletarLivro(int Id)
    {
        var livro = _service.ObterLivroPorIDAsync(id: Id).Result;

        return View(livro);
    }

    [HttpPost]
    public ActionResult DeletarLivroPorID(int Id)
    {
        _service.DeletarLivroPorID(id: Id);

        return View("Index");
    }
}
