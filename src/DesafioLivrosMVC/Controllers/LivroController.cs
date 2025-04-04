using DesafioLivrosMVC.Models;
using DesafioLivrosMVC.Service;
using Microsoft.AspNetCore.Mvc;
using DesafioLivrosMVC.Exceptions;

namespace DesafioLivrosMVC.Controllers;

public class LivroController : Controller
{
    private readonly ILivroService _livroService;

    public LivroController(ILivroService livroService)
    {
        _livroService = livroService;
    }

    public async Task<IActionResult> Index(string? pesquisa)
    {
        var livros = await _livroService.ConsultarLivroAsync(pesquisa);

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
            await _livroService.IncluirLivro(livro: livro);

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
            var livro = await _livroService.ObterLivroPorIDAsync(id: Id);

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
            await _livroService.EditarLivroPorID(livro: livro);
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
            var livro = await _livroService.ObterLivroPorIDAsync(id: Id);

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
            await _livroService.DeletarLivroPorID(id: Id);

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
