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
    public class ProduitsController : Controller
    {
        private readonly ManageStockContext _context;

        public ProduitsController(ManageStockContext context)
        {
            _context = context;
        }

        // GET: Produits
        public async Task<IActionResult> Index()
        {
            var manageStockContext = _context.Produits.Include(p => p.IdCategorieNavigation).Include(p => p.IdFournisseurNavigation);
            return View(await manageStockContext.ToListAsync());
            //return View( await _context.Produits.ToListAsync());
        }

        // GET: Produits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produit = await _context.Produits
                .Include(p => p.IdCategorieNavigation)
                .Include(p => p.IdFournisseurNavigation)
                .FirstOrDefaultAsync(m => m.IdProduit == id);
            if (produit == null)
            {
                return NotFound();
            }

            return View(produit);
        }

        // GET: Produits/Create
        public IActionResult Create()
        {
            ViewData["IdCategorie"] = new SelectList(_context.Categories, "IdCategorie", "Nom");
            ViewData["IdFournisseur"] = new SelectList(_context.Fournisseurs, "IdFournisseur", "Nom");
            return View();
        }

        // POST: Produits/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProduit,Nom,Description,CodeBarres,PrixUnitaire,SeuilAlerte,IdCategorie,IdFournisseur")] Produit produit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategorie"] = new SelectList(_context.Categories, "IdCategorie", "Nom", produit.IdCategorie);
            ViewData["IdFournisseur"] = new SelectList(_context.Fournisseurs, "IdFournisseur", "Nom", produit.IdFournisseur);
            return View(produit);
        }

        // GET: Produits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produit = await _context.Produits.FindAsync(id);
            if (produit == null)
            {
                return NotFound();
            }
            ViewData["IdCategorie"] = new SelectList(_context.Categories, "IdCategorie", "Nom", produit.IdCategorie);
            ViewData["IdFournisseur"] = new SelectList(_context.Fournisseurs, "IdFournisseur", "Nom", produit.IdFournisseur);
            return View(produit);
        }

        // POST: Produits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProduit,Nom,Description,CodeBarres,PrixUnitaire,SeuilAlerte,IdCategorie,IdFournisseur")] Produit produit)
        {
            if (id != produit.IdProduit)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProduitExists(produit.IdProduit))
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
            ViewData["IdCategorie"] = new SelectList(_context.Categories, "IdCategorie", "Nom", produit.IdCategorie);
            ViewData["IdFournisseur"] = new SelectList(_context.Fournisseurs, "IdFournisseur", "Nom", produit.IdFournisseur);
            return View(produit);
        }

        // GET: Produits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produit = await _context.Produits
                .Include(p => p.IdCategorieNavigation)
                .Include(p => p.IdFournisseurNavigation)
                .FirstOrDefaultAsync(m => m.IdProduit == id);
            if (produit == null)
            {
                return NotFound();
            }

            return View(produit);
        }

        // POST: Produits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produit = await _context.Produits.FindAsync(id);
            if (produit != null)
            {
                _context.Produits.Remove(produit);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProduitExists(int id)
        {
            return _context.Produits.Any(e => e.IdProduit == id);
        }
    }
}
