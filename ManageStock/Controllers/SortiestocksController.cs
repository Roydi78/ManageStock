using ManageStock.Data;
using ManageStock.Data.Services.EntreeStock;
using ManageStock.Data.Services.Entrepot;
using ManageStock.Data.Services.Produit;
using ManageStock.Data.Services.SortieStock;
using ManageStock.Data.Services.Stock;
using ManageStock.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageStock.Controllers
{
    public class SortiestocksController : Controller
    {
        private readonly ISortieStockService _SortieStockService;
        private readonly IEntrepotService _EntrepotService;
        private readonly IProduitService _ProduitService;
        private readonly IStockService _StockService;

        public SortiestocksController(ISortieStockService SortieStockService, IEntrepotService EntrepotService, IProduitService ProduitService, IStockService StockService)
        {
            _SortieStockService = SortieStockService;
            _EntrepotService = EntrepotService;
            _ProduitService = ProduitService;
            _StockService = StockService;
        }

        // GET: Sortiestocks
        public async Task<IActionResult> Index()
        {
            return View(await _SortieStockService.GetAll());
        }

        // GET: Sortiestocks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sortiestock = await _SortieStockService.Detail(id.Value);
            if (sortiestock == null)
            {
                return NotFound();
            }

            return View(sortiestock);
        }
  
        public async Task<IActionResult> Create()
        {
         
            ViewData["ProduitStock"] = await GetProduitenStock();

            return View();
        }

        // POST: Sortiestocks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSortie,IdProduit,Quantité,DateSortie,Destination,IdEntrepot")] Sortiestock sortiestock)
        {
            if (ModelState.IsValid)
            {
                await _SortieStockService.Add(sortiestock);
                return RedirectToAction(nameof(Index));
            }

            ViewData["ProduitStock"] = await GetProduitenStock();

            return View(sortiestock);
        }



        // GET: Sortiestocks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sortiestock = await _SortieStockService.FirstorDefault((int)id);
            if (sortiestock == null)
            {
                return NotFound();
            }

            //ViewData["ProduitStock"] = await GetProduitenStock();
            return View(sortiestock);
        }

        // POST: Sortiestocks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSortie,IdProduit,Quantité,DateSortie,Destination,IdEntrepot")] Sortiestock sortiestock)
        {
            if (id != sortiestock.IdSortie)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _SortieStockService.Update(sortiestock);
                }
                catch (DbUpdateConcurrencyException)
                {
                    var existingsortieStock = await _SortieStockService.Exists(sortiestock.IdSortie);
                    if (!existingsortieStock)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["ProduitStock"] = await GetProduitenStock();
            return View(sortiestock);
        }

        // GET: Sortiestocks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sortiestock = await _SortieStockService.FirstorDefault((int)id);
            if (sortiestock == null)
            {
                return NotFound();
            }

            return View(sortiestock);
        }

        // POST: Sortiestocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _SortieStockService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool SortiestockExists(int id)
        {
            //return _context.Sortiestocks.Any(e => e.IdSortie == id);
            return true;
        }

        private async Task<List<object>> GetProduitenStock()
        {
            var stock = await _StockService.GetAllNoGrp();

            var selectList = stock
                .Select(s => new
                {
                    Id = s.IdProduit,
                    IdEntrep = s.IdEntrepot,
                    ProduitNom = s.IdProduitNavigation.Nom,
                    EntrepotNom = s.IdEntrepotNavigation.Nom,
                    Text = $"{s.IdProduitNavigation.Nom} - {s.IdEntrepotNavigation.Nom}",
                    Qtedispo = s.QuantitéDisponible
                })
                .Cast<object>() // nécessaire car tu retournes List<object>
                .ToList();

            return selectList;
        }
    }
}
