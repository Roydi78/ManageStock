using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManageStock.Data;
using ManageStock.Models;

namespace ManageStock.Controllers
{
    public class InventairesController : Controller
    {
        private readonly ManageStockContext _context;

        public InventairesController(ManageStockContext context)
        {
            _context = context;
        }

        // GET: Inventaires
        public async Task<IActionResult> Index()
        {
            var manageStockContext = _context.Inventaires.Include(i => i.IdEntrepotNavigation);
            return View(await manageStockContext.ToListAsync());
        }

        // GET: Inventaires/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventaire = await _context.Inventaires
                .Include(i => i.IdEntrepotNavigation)
                .Include(i => i.Detailinventaires)
                 .ThenInclude(d => d.IdProduitNavigation)
                .FirstOrDefaultAsync(m => m.IdInventaire == id);

            if (inventaire == null)
            {
                return NotFound();
            }

            return View(inventaire);
        }

        // GET: Inventaires/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Entrepots = new SelectList(await _context.Entrepots.ToListAsync(), "IdEntrepot", "Nom");
            ViewBag.Produits = new SelectList(await _context.Produits.ToListAsync(), "IdProduit", "Nom");

            return View(new Inventaire());
        }

        // POST: Inventaires/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Inventaire inventaire)
        {
            if (ModelState.IsValid)
            {
                inventaire.DateInventaire = DateOnly.FromDateTime(DateTime.Now);
                _context.Inventaires.Add(inventaire);
                await _context.SaveChangesAsync(); // enregistre l'inventaire + les détails


                return RedirectToAction(nameof(Index));                
            }

            ViewBag.Entrepots = new SelectList(await _context.Entrepots.ToListAsync(), "IdEntrepot", "Nom");
            ViewBag.Produits = new SelectList(await _context.Produits.ToListAsync(), "IdProduit", "Nom");
            return View(inventaire);

        }

        // GET: Inventaires/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var inventaire = await _context.Inventaires
                .Include(i => i.Detailinventaires)
                .FirstOrDefaultAsync(i => i.IdInventaire == id);

            if (inventaire == null) return NotFound();

            ViewBag.Entrepots = new SelectList(await _context.Entrepots.ToListAsync(), "IdEntrepot", "Nom", inventaire.IdEntrepot);
            ViewBag.Produits = new SelectList(await _context.Produits.ToListAsync(), "IdProduit", "Nom");

            return View(inventaire);
        }

        // POST: Inventaires/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Inventaire inventaire)
        {
            if (id != inventaire.IdInventaire) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Entrepots = new SelectList(await _context.Entrepots.ToListAsync(), "IdEntrepot", "Nom", inventaire.IdEntrepot);
                ViewBag.Produits = new SelectList(await _context.Produits.ToListAsync(), "IdProduit", "Nom");
                return View(inventaire);
            }

            try
            {

                // Supprimer les anciens détails
                var anciens = _context.Detailinventaires.Where(d => d.IdInventaire == id);
                _context.Detailinventaires.RemoveRange(anciens);

                
                // Ajouter les nouveaux détails
                _context.Update(inventaire);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Inventaires.Any(e => e.IdInventaire == id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Inventaires/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventaire = await _context.Inventaires
                .Include(i => i.IdEntrepotNavigation)
                .FirstOrDefaultAsync(m => m.IdInventaire == id);
            if (inventaire == null)
            {
                return NotFound();
            }

            return View(inventaire);
        }

        // POST: Inventaires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventaire = await _context.Inventaires.FindAsync(id);

            if (inventaire == null)
                return NotFound();

            // Vérifie s’il y a des détails liés
            bool hasDetails = await _context.Detailinventaires.AnyAsync(d => d.IdInventaire == id);

            if (hasDetails)
            {
                TempData["ErrorMessage"] = "❌ Impossible de supprimer cet inventaire car des produits y sont associés.";
                return RedirectToAction(nameof(Index));
            }

            _context.Inventaires.Remove(inventaire);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "✅ Inventaire supprimé avec succès.";
            return RedirectToAction(nameof(Index));
        }

        private bool InventaireExists(int id)
        {
            return _context.Inventaires.Any(e => e.IdInventaire == id);
        }
    }
}
