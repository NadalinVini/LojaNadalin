using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cliente.UI.Models;
using Microsoft.AspNetCore.Authorization;

namespace Cliente.UI.Controllers
{
    [Authorize]
    public class EstadosController : Controller
    {
        private readonly Services.IRepositoryGeneric<Estado> repositoryEstado;

        public EstadosController(Services.IRepositoryGeneric<Estado> repoEstado)
        {
            repositoryEstado = repoEstado;
        }

        // GET: Estados
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = await repositoryEstado.GetAllAsync();
            return View(applicationDbContext);
        }

        // GET: Estados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estado = await repositoryEstado.GetAsync(id.Value);
            if (estado == null)
            {
                return NotFound();
            }

            return View(estado);
        }

        // GET: Estados/Create
        public IActionResult Create()
        {
            ViewData["EstadoId"] = new SelectList(repositoryEstado.GetAll(), "EstadoId", "Nome");
            return View();
        }

        // POST: Estados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] Estado estado)
        {
            if (ModelState.IsValid)
            {

                await repositoryEstado.InsertAsync(estado);
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstadoId"] = new SelectList(repositoryEstado.GetAll(), "EstadoId", "Nome");
            return View(estado);
        }

        // GET: Estados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estado = await repositoryEstado.GetAsync(id.Value);

            if (estado == null)
            {
                return NotFound();
            }
            ViewData["EstadoId"] = new SelectList(repositoryEstado.GetAll(), "EstadoId", "Nome");
            return View(estado);
        }

        // POST: Estados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Estado estado)
        {
            if (id != estado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await repositoryEstado.UpdateAsync(id, estado);
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstadoId"] = new SelectList(repositoryEstado.GetAll(), "EstadoId", "Nome");
            return View(estado);
        }

        // GET: Estados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estado = await repositoryEstado.GetAsync(id);
            if (estado == null)
            {
                return NotFound();
            }

            return View(estado);
        }

        // POST: Estados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await repositoryEstado.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
