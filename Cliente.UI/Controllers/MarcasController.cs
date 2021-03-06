﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cliente.UI.Data;
using Cliente.UI.Models;
using Microsoft.AspNetCore.Authorization;

namespace Cliente.UI.Controllers
{
    [Authorize]
    public class MarcasController : Controller
    {
        private readonly Services.IRepositoryGeneric<Marca> repositoryMarca;

        public MarcasController(Services.IRepositoryGeneric<Marca> repoMarca)
        {
            repositoryMarca = repoMarca;
        }

        // GET: Marcas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = await repositoryMarca.GetAllAsync();
            return View(applicationDbContext);
        }

        // GET: Marcas/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marca = await repositoryMarca.GetAsync(id.Value);
            if (marca == null)
            {
                return NotFound();
            }

            return View(marca);
        }

        // GET: Marcas/Create
        public IActionResult Create()
        {
            ViewData["MarcaId"] = new SelectList(repositoryMarca.GetAll(), "Id", "Nome");
            return View();          
        }

        // POST: Marcas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] Marca marca)
        {
            if (ModelState.IsValid)
            {
                await repositoryMarca.InsertAsync(marca);
                return RedirectToAction(nameof(Index));
            }
            ViewData["MarcaId"] = new SelectList(repositoryMarca.GetAll(), "Id", "Nome");
            return View(marca);
        }

        // GET: Marcas/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marca = await repositoryMarca.GetAsync(id.Value);
            if (marca == null)
            {
                return NotFound();
            }
            ViewData["MarcaId"] = new SelectList(repositoryMarca.GetAll(), "Id", "Nome");
            return View(marca);
        }

        // POST: Marcas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nome")] Marca marca)
        {
            if (id != marca.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await repositoryMarca.UpdateAsync(id, marca);
                return RedirectToAction(nameof(Index));
            }
            ViewData["MarcaId"] = new SelectList(repositoryMarca.GetAll(), "Id", "Nome");
            return View(marca);
        }

        // GET: Marcas/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marca = await repositoryMarca.GetAsync(id);
            if (marca == null)
            {
                return NotFound();
            }

            return View(marca);
        }

        // POST: Marcas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await repositoryMarca.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
