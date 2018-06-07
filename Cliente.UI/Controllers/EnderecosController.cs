using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cliente.UI.Data;
using Cliente.UI.Models;

namespace Cliente.UI.Controllers
{
    public class EnderecosController : Controller
    {
        protected readonly Services.IRepositoryGeneric<Endereco> repository;
        protected readonly Services.IRepositoryGeneric<Cidade> repositoryCidade;

        public EnderecosController(Services.EnderecoRepository repo,
            Services.RepositoryGeneric<Cidade> repoCidade)
        {
            repository = repo;
        }

        // GET: Enderecoes
        public async Task<IActionResult> Index()
        {
            return View(await repository.GetAllAsync());
        }

        // GET: Enderecoes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var endereco = await repository.GetAsync(id.Value);
            if (endereco == null)
            {
                return NotFound();
            }

            return View(endereco);
        }

        // GET: Enderecoes/Create
        public IActionResult Create()
        {
            ViewData["CidadeId"] = new SelectList(repositoryCidade.GetAll(), "Id", "Nome");
            return View();
        }

        // POST: Enderecoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CidadeId,Cep")] Endereco endereco)
        {
            if (ModelState.IsValid)
            {
                await repository.InsertAsync(endereco);
                return RedirectToAction(nameof(Index));
            }

            ViewData["CidadeId"] = new SelectList(repositoryCidade.GetAll(), "Id", "Nome", endereco.CidadeId);
            return View(endereco);
        }

        // GET: Enderecoes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var endereco = await repository.GetAsync(id.Value);
            if (endereco == null)
            {
                return NotFound();
            }
            ViewData["CidadeId"] = new SelectList(repositoryCidade.GetAll(), "Id", "Nome", endereco.CidadeId);
            return View(endereco);
        }

        // POST: Enderecoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,CidadeId,Cep")] Endereco endereco)
        {
            if (id != endereco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await repository.UpdateAsync(endereco.Id, endereco);

                return RedirectToAction(nameof(Index));
            }

            ViewData["CidadeId"] = new SelectList(repositoryCidade.GetAll(), "Id", "Nome", endereco.CidadeId);
            return View(endereco);
        }

        // GET: Enderecoes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var endereco = await repository.GetAsync(id);
            if (endereco == null)
            {
                return NotFound();
            }

            return View(endereco);
        }

        // POST: Enderecoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await repository.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
