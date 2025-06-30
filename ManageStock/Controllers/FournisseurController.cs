using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManageStock.Data;
using ManageStock.Models;
using ManageStock.Data.Services.Fournisseur;

namespace ManageStock.Controllers
{
    public class FournisseurController : Controller
    {
        private readonly IFournisseurService _FournisseurService;

        public FournisseurController(IFournisseurService FournisseurService)
        {
            _FournisseurService = FournisseurService;
        }

        // GET: Fournisseur
        public async Task<IActionResult> Index()
        {
            return View(await _FournisseurService.GetAll());
        }

        // Fix for CS8629: Nullable value type may be null.
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fournisseur = await _FournisseurService.DetailFournisseur(id.Value);

            if (fournisseur == null)
            {
                return NotFound();
            }

            return View(fournisseur);
        }

        // GET: Fournisseur/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fournisseur/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFournisseur,Nom,Adresse,Email,Téléphone")] Fournisseur fournisseur)
        {
            if (ModelState.IsValid)
            {
                await _FournisseurService.Add(fournisseur);
                return RedirectToAction(nameof(Index));
            }
            return View(fournisseur);
        }

        // GET: Fournisseur/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fournisseur = await _FournisseurService.GetById((int)id);
            if (fournisseur == null)
            {
                return NotFound();
            }
            return View(fournisseur);
        }

        // POST: Fournisseur/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFournisseur,Nom,Adresse,Email,Téléphone")] Fournisseur fournisseur)
        {
            if (id != fournisseur.IdFournisseur)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _FournisseurService.Update(fournisseur);
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    var FournisseurExist=await FournisseurExists(fournisseur.IdFournisseur);
                    if (!FournisseurExist)
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
            return View(fournisseur);
        }

        // GET: Fournisseur/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fournisseur = await _FournisseurService.FirstorDefault((int)id);
                
            if (fournisseur == null)
            {
                return NotFound();
            }

            return View(fournisseur);
        }

        // POST: Fournisseur/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fournisseur = await _FournisseurService.GetById(id);
            if (fournisseur != null)
            {
                await _FournisseurService.Delete(id);
            }

            
            return RedirectToAction(nameof(Index));
        }

        private async Task <bool> FournisseurExists(int id)
        {
            return await _FournisseurService.Exists(id);
        }
    }
}
