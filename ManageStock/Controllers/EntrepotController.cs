using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManageStock.Data;
using ManageStock.Models;
using ManageStock.Data.Services.Entrepot;

namespace ManageStock.Controllers
{
    public class EntrepotController : Controller
    {
        private readonly IEntrepotService _EntrepotService;

        public EntrepotController(IEntrepotService EntrepotService)
        {
            _EntrepotService = EntrepotService;
        }

        // GET: Entrepot
        public async Task<IActionResult> Index()
        {
            return View(await _EntrepotService.GetAll());
        }

        // GET: Entrepot/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entrepot = await _EntrepotService.GetById((int)id);
            if (entrepot == null)
            {
                return NotFound();
            }

            return View(entrepot);
        }

        // GET: Entrepot/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Entrepot/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEntrepot,Nom,Adresse")] Entrepot entrepot)
        {
            if (ModelState.IsValid)
            {
                await _EntrepotService.Add(entrepot);
                return RedirectToAction(nameof(Index));
            }
            return View(entrepot);
        }

        // GET: Entrepot/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entrepot = await _EntrepotService.GetById((int)id);
            if (entrepot == null)
            {
                return NotFound();
            }
            return View(entrepot);
        }

        // POST: Entrepot/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEntrepot,Nom,Adresse")] Entrepot entrepot)
        {
            if (id != entrepot.IdEntrepot)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _EntrepotService.Update(entrepot);

                }
                catch (DbUpdateConcurrencyException)
                {
                    var EntrepotExist = await EntrepotExists(entrepot.IdEntrepot);
                    if (!EntrepotExist)
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
            return View(entrepot);
        }

        // GET: Entrepot/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entrepot = await _EntrepotService.FirstorDefault((int)id);
            if (entrepot == null)
            {
                return NotFound();
            }

            return View(entrepot);
        }

        // POST: Entrepot/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entrepot = await _EntrepotService.GetById(id);
            if (entrepot != null)
            {
                await _EntrepotService.Delete(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> EntrepotExists(int id)
        {
            return await _EntrepotService.Exists(id);
        }
    }
}
