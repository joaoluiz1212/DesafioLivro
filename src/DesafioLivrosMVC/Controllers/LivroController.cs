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

    public IActionResult CriarLivro()
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

        try
        {
            await _service.IncluirLivro(livro: livro);

            TempData["MensagemSucesso"] = "Livro adicionado com sucesso!";
            return RedirectToAction("Index");
        }

        catch (ServiceException ex)
        {
            TempData["MensagemErro"] = $"Erro ao adicionar livro: {ex.Message}";
            return View("Index");
        }

    }

    public async Task<IActionResult> EditarLivro(int Id)
    {
        try
        {
            var livro = await _service.ObterLivroPorIDAsync(id: Id);

            return View(livro);
        }

        catch (ServiceException ex)
        {
            TempData["MensagemErro"] = $"Erro ao buscar livro por ID: {ex.Message}";
            return View("Index");
        }
    }

    [HttpPost]
    public async Task<IActionResult> AtualizarLivro(Livro livro)
    {
        if (!ModelState.IsValid)
        {
            return View(livro);
        }

        try
        {
            await _service.EditarLivroPorID(livro: livro);
            TempData["MensagemSucesso"] = "Livro editado com sucesso!";
            return RedirectToAction("Index");
        }

        catch (ServiceException ex)
        {
            TempData["MensagemErro"] = $"Erro ao atualizar livro: {ex.Message}";
            return View("Index");
        }
    }
    public async Task<IActionResult> DeletarLivro(int Id)
    {
        try
        {
            var livro = await _service.ObterLivroPorIDAsync(id: Id);

            return View(livro);
        }

        catch (ServiceException ex)
        {
            TempData["MensagemErro"] = $"Erro ao buscar o livro por id: {ex.Message}";
            return View("Index");
        }
    }

    [HttpPost]
    public async Task<IActionResult> DeletarLivroPorID(int Id)
    {

        try
        {
            await _service.DeletarLivroPorID(id: Id);

            TempData["MensagemSucesso"] = "Livro deletado com sucesso!";
            return RedirectToAction("Index");
        }

        catch (ServiceException ex)
        {
            TempData["MensagemErro"] = $"Erro ao deletar livro: {ex.Message}";
            return View("Index");
        }
    }
}
