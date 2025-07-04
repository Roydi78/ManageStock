using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManageStock.Data;
using ManageStock.Models;
using ManageStock.Data.Services.Produit;
using ManageStock.Data.Services.Categorie;
using ManageStock.Data.Services.Fournisseur;

namespace ManageStock.Controllers
{
    public class ProduitsController : Controller
    {
        private readonly IProduitService _ProduitService;
        private readonly ICategorieService _CategorieService;
        private readonly IFournisseurService _FournisseurService;


        public ProduitsController(IProduitService ProduitService, ICategorieService CategorieService, IFournisseurService FournisseurService)
        {
            _ProduitService = ProduitService;
            _CategorieService = CategorieService ?? throw new ArgumentNullException(nameof(CategorieService));
            _FournisseurService = FournisseurService ?? throw new ArgumentNullException(nameof(FournisseurService));
        }


        // GET: Produits
        public async Task<IActionResult> Index()
        {
            return View(await _ProduitService.GetAll());    
        }

        // GET: Produits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produit = await _ProduitService.DetailProduit(id.Value);
            if (produit == null)
            {
                return NotFound();
            }

            return View(produit);
        }

        // Fix for the CS1503 error in the Create method
        public IActionResult Create()
        {
            // Await the result of the asynchronous GetAll() method to resolve the Task<IEnumerable<Categorie>> to IEnumerable<Categorie>
            var categories = _CategorieService.GetAll().Result;
            var fournisseurs = _FournisseurService.GetAll().Result;
            ViewData["IdCategorie"] = new SelectList(categories, "IdCategorie", "Nom");
            ViewData["IdFournisseur"] = new SelectList(fournisseurs, "IdFournisseur", "Nom");
            return View();
        }

        // POST: Produits/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProduit,Nom,Description,CodeBarres,PrixUnitaire,SeuilAlerte,IdCategorie,IdFournisseur")] Produit produit)
        {
            if (ModelState.IsValid)
            {
                await _ProduitService.Add(produit);
                return RedirectToAction(nameof(Index));
            }

            var categories = _CategorieService.GetAll().Result;
            var fournisseurs = _FournisseurService.GetAll().Result;
            ViewData["IdCategorie"] = new SelectList(categories, "IdCategorie", "Nom", produit.IdCategorie);
            ViewData["IdFournisseur"] = new SelectList(fournisseurs, "IdFournisseur", "Nom", produit.IdFournisseur);
            return View(produit);
        }

        // GET: Produits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produit = await _ProduitService.GetById((int)id);
            if (produit == null)
            {
                return NotFound();
            }

            var categories = _CategorieService.GetAll().Result;
            var fournisseurs = _FournisseurService.GetAll().Result;
            ViewData["IdCategorie"] = new SelectList(categories, "IdCategorie", "Nom", produit.IdCategorie);
            ViewData["IdFournisseur"] = new SelectList(fournisseurs, "IdFournisseur", "Nom", produit.IdFournisseur);
            return View(produit);
        }

        // POST: Produits/Edit/5
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
                    await _ProduitService.Update(produit);
                }
                catch (DbUpdateConcurrencyException)
                {
                    var existingProduit = await _ProduitService.Exists(produit.IdProduit);
                    if (!existingProduit)
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
            // If we got this far, something failed; redisplay form
            var categories = _CategorieService.GetAll().Result;
            var fournisseurs = _FournisseurService.GetAll().Result;
            ViewData["IdCategorie"] = new SelectList(categories, "IdCategorie", "Nom", produit.IdCategorie);
            ViewData["IdFournisseur"] = new SelectList(fournisseurs, "IdFournisseur", "Nom", produit.IdFournisseur);
            return View(produit);
        }

        // GET: Produits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produit = await _ProduitService.FirstorDefault((int)id);
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
            await _ProduitService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProduitExists(int id)
        {
            return await _ProduitService.Exists(id);
        }
    }
}
