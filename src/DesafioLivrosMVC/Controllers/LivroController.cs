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

    public async Task<IActionResult> Index(string? pesquisa)
    {
        var livros = await _service.ConsultarLivroAsync(pesquisa);

        return View(livros);
    }

    public ActionResult CriarLivro()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AdicionarLivro(Livro livro)
    {
        if (!ModelState.IsValid)
        {
            return View(livro);
        }
       await _service.IncluirLivro(livro: livro);

        TempData["MensagemSucesso"] = "Livro adicionado com sucesso!";
        return RedirectToAction("Index");
    }

    public ActionResult EditarLivro(int Id)
    {
        var livro = _service.ObterLivroPorIDAsync(id: Id).Result;

        return View(livro);
    }

    [HttpPost]
    public async Task<IActionResult> AtualizarLivro(Livro livro)
    {
        if (!ModelState.IsValid)
        {
            return View(livro);
        }
        await _service.EditarLivroPorID(livro: livro);
        TempData["MensagemSucesso"] = "Livro editado com sucesso!";
        return RedirectToAction("Index");
    }
    public ActionResult DeletarLivro(int Id)
    {
        var livro = _service.ObterLivroPorIDAsync(id: Id).Result;

        return View(livro);
    }

    [HttpPost]
    public async Task<IActionResult> DeletarLivroPorID(int Id)
    {
        await _service.DeletarLivroPorID(id: Id);

        TempData["MensagemSucesso"] = "Livro deletado com sucesso!";
        return RedirectToAction("Index");
    }
}
