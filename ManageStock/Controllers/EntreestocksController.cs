using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManageStock.Data;
using ManageStock.Models;
using ManageStock.Data.Services.EntreeStock;
using ManageStock.Data.Services.Entrepot;
using ManageStock.Data.Services.Produit;
using ManageStock.Data.Services.Fournisseur;

namespace ManageStock.Controllers
{
    public class EntreestocksController : Controller
    {
        private readonly IEntreeStockService _EntreeStockService;
        private readonly IEntrepotService _EntrepotService;
        private readonly IProduitService _ProduitService;
        private readonly IFournisseurService _FournisseurService;

        public EntreestocksController(IEntreeStockService EntreeStockService, IEntrepotService EntrepotService, IProduitService ProduitService, IFournisseurService FournisseurService)
        {
            _EntreeStockService = EntreeStockService;
            _EntrepotService = EntrepotService;
            _ProduitService = ProduitService;
            _FournisseurService = FournisseurService;
        }

        // GET: Entreestocks
        public async Task<IActionResult> Index()
        {
            return View(await _EntreeStockService.GetAll());
        }

        // GET: Entreestocks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entreestock = await _EntreeStockService.DetailEntreestock(id.Value);
            if (entreestock == null)
            {
                return NotFound();
            }

            return View(entreestock);
        }

        // GET: Entreestocks/Create
        public IActionResult Create()
        {
            ViewData["IdEntrepot"] = new SelectList(_EntrepotService.GetAll().Result, "IdEntrepot", "Nom");
            ViewData["IdFournisseur"] = new SelectList(_FournisseurService.GetAll().Result, "IdFournisseur", "Nom");
            ViewData["IdProduit"] = new SelectList(_ProduitService.GetAll().Result, "IdProduit", "Nom");
            return View();
        }

        // POST: Entreestocks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEntree,IdProduit,Quantité,DateEntree,IdFournisseur,IdEntrepot")] Entreestock entreestock)
        {
            if (ModelState.IsValid)
            {
                await _EntreeStockService.Add(entreestock);
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdEntrepot"] = new SelectList(_EntrepotService.GetAll().Result, "IdEntrepot", "Nom", entreestock.IdEntrepot);
            ViewData["IdFournisseur"] = new SelectList(_FournisseurService.GetAll().Result, "IdFournisseur", "Nom", entreestock.IdFournisseur);
            ViewData["IdProduit"] = new SelectList(_ProduitService.GetAll().Result, "IdProduit", "Nom", entreestock.IdProduit);
            return View(entreestock);
        }

        // GET: Entreestocks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entreestock = await _EntreeStockService.GetById((int)id);
            if (entreestock == null)
            {
                return NotFound();
            }

            ViewData["IdEntrepot"] = new SelectList(_EntrepotService.GetAll().Result, "IdEntrepot", "Nom", entreestock.IdEntrepot);
            ViewData["IdFournisseur"] = new SelectList(_FournisseurService.GetAll().Result, "IdFournisseur", "Nom", entreestock.IdFournisseur);
            ViewData["IdProduit"] = new SelectList(_ProduitService.GetAll().Result, "IdProduit", "Nom", entreestock.IdProduit);
            return View(entreestock);
        }

        // POST: Entreestocks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEntree,IdProduit,Quantité,DateEntree,IdFournisseur,IdEntrepot")] Entreestock entreestock)
        {
            if (id != entreestock.IdEntree)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _EntreeStockService.Update(entreestock);
                }
                catch (DbUpdateConcurrencyException)
                {
                    var existingEntreeStock = await _EntreeStockService.Exists(entreestock.IdEntree);
                    if (!existingEntreeStock)
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

            ViewData["IdEntrepot"] = new SelectList(_EntrepotService.GetAll().Result, "IdEntrepot", "Nom", entreestock.IdEntrepot);
            ViewData["IdFournisseur"] = new SelectList(_FournisseurService.GetAll().Result, "IdFournisseur", "Nom", entreestock.IdFournisseur);
            ViewData["IdProduit"] = new SelectList(_ProduitService.GetAll().Result, "IdProduit", "Nom", entreestock.IdProduit);
            return View(entreestock);
        }

        // GET: Entreestocks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entreestock = await _EntreeStockService.FirstorDefault((int)id);
            if (entreestock == null)
            {
                return NotFound();
            }

            return View(entreestock);
        }

        // POST: Entreestocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _EntreeStockService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> EntreestockExists(int id)
        {
            return await _EntreeStockService.Exists(id);
        }
    }
}
