using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cliente.UI.Data;
using Cliente.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Cliente.UI.Controllers
{
    [Authorize]
    public class CidadesController : Controller
    {
        private readonly Services.IRepositoryGeneric<Cidade> repositoryCidade;
        private readonly Services.IRepositoryGeneric<Estado> repositoryEstado;
        

        public CidadesController(Services.IRepositoryGeneric<Cidade> repoCidade,
                                 Services.IRepositoryGeneric<Estado> repoEstado)
        {
            repositoryCidade = repoCidade;
            repositoryEstado = repoEstado;
        }

        // GET: Cidades
        public async Task<IActionResult> Index(int? id)
        {
            var applicationDbContext = await repositoryCidade.GetAllAsync(c => id == null || c.EstadoId == id, c => c.Estado);
            return View(applicationDbContext);
        }

        // GET: Cidades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cidade = await repositoryCidade.GetAsync(id.Value);
            ViewData["EstadoId"] = new SelectList(repositoryEstado.GetAll(), "EstadoId", "Nome");
            if (cidade == null)
            {
                return NotFound();
            }

            return View(cidade);
        }

        // GET: Cidades/Create
        public IActionResult Create()
        {
            ViewData["EstadoId"] = new SelectList(repositoryEstado.GetAll(), "Id", "Nome");
            return View();
        }

        // POST: Cidades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,EstadoId")] Cidade cidade)
        {
            if (ModelState.IsValid)
            {
                await repositoryCidade.InsertAsync(cidade);
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstadoId"] = new SelectList(await repositoryCidade.GetAllAsync(), "EstadoId", "Nome", cidade.EstadoId);
            return View(cidade);
        }

        // GET: Cidades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cidade = await repositoryCidade.GetAsync(id.Value);

            if (cidade == null)
            {
                return NotFound();
            }

            var list = await repositoryEstado.GetAllAsync();

            ViewData["EstadoId"] = new SelectList(list, "Id", "Nome", cidade.EstadoId);
            return View(cidade);
        }

        // POST: Cidades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,EstadoId")] Cidade cidade)
        {
            if (id != cidade.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await repositoryCidade.UpdateAsync(cidade.Id, cidade);
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstadoId"] = new SelectList(repositoryEstado.GetAll(), "Id", "Nome", cidade.EstadoId);
            return View(cidade);
        }

        // GET: Cidades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cidade = await repositoryCidade.GetAsync(id);

            if (cidade == null)
            {
                return NotFound();
            }

            return View(cidade);
        }

        // POST: Cidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await repositoryCidade.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
