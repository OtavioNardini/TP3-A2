using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestauranteAPP_TP3.Data;   // ajuste namespace se for diferente
using RestauranteAPP_TP3.Models; // ajuste para o namespace dos seus models
using System.Linq;

public class CardapioController : Controller
{
    private readonly ApplicationDbContext _db;

    public CardapioController(ApplicationDbContext db)
    {
        _db = db;
    }

public IActionResult Index()
    {
        var itens = _db.ItensCardapio.ToList();
        var sugestao = itens.FirstOrDefault(i => i.SugestaoDoChefe);
        ViewBag.SugestaoDoChefeId = sugestao?.Id;
        return View(itens);
    }



    public IActionResult Create()
    {
        ViewData["Periodo"] = new SelectList(Enum.GetValues(typeof(PeriodoCardapio)));
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ItemCardapio model)
    {
        if (ModelState.IsValid)
        {
            _db.ItensCardapio.Add(model);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Periodos = Enum.GetValues(typeof(PeriodoCardapio));
        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var item = await _db.ItensCardapio.FindAsync(id);
        if (item == null) return NotFound();

        ViewData["Periodo"] = new SelectList(Enum.GetValues(typeof(PeriodoCardapio)));
        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ItemCardapio model)
    {
        if (ModelState.IsValid)
        {
            _db.Update(model);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Periodos = Enum.GetValues(typeof(PeriodoCardapio));
        return View(model);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var item = await _db.ItensCardapio.FindAsync(id);
        if (item == null) return NotFound();
        return View(item);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var item = await _db.ItensCardapio.FindAsync(id);
        if (item == null) return NotFound();

        _db.ItensCardapio.Remove(item);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }



    public IActionResult SugestaoDoChefe()
    {
        var sugestoes = _db.ItensCardapio
            .Where(i => i.SugestaoDoChefe == true)
            .ToList();

        return View(sugestoes);
    }
}
