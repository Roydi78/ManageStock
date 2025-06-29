using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManageStock.Data;
using ManageStock.Models;
using ManageStock.Data.Services.Categorie;

namespace ManageStock.Controllers
{
    public class CategorieController : Controller
    {
        private readonly ICategorieService _categorieservice;

        public CategorieController(ICategorieService categorieservice)
        {
            _categorieservice = categorieservice;
        }

        // GET: Categorie
        public async Task<IActionResult> Index()
        {
            var categorie = await _categorieservice.GetAll();
            return View(categorie);      
        }

        // GET: Categorie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorie = await _categorieservice.GetById((int)id);
            if (categorie == null)
            {
                return NotFound();
            }

            return View(categorie);
        }

        // GET: Categorie/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categorie/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCategorie,Nom,Description")] Categorie categorie)
        {
            if (ModelState.IsValid)
            {
                await _categorieservice.Add(categorie);
                return RedirectToAction(nameof(Index));
            }
            return View(categorie);
        }

        // GET: Categorie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorie = await _categorieservice.GetById((int)id);
            if (categorie == null)
            {
                return NotFound();
            }
            return View(categorie);
        }

        // POST: Categorie/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCategorie,Nom,Description")] Categorie categorie)
        {
            if (id != categorie.IdCategorie)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _categorieservice.Update(id,categorie);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CategorieExists(id))
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
            return View(categorie);
        }

        // GET: Categorie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorie = await _categorieservice.GetById((int)id);
            if (categorie == null)
            {
                return NotFound();
            }

            return View(categorie);
        }

        // POST: Categorie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!await CategorieExists(id))
            {
                return NotFound();
            }
   
            await _categorieservice.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CategorieExists(int id)
        {
            return await _categorieservice.Exists(id);
        }
    }
}
