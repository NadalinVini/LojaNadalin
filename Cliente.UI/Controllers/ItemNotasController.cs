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
    public class ItemNotasController : Controller
    {
        private readonly Services.IRepositoryGeneric<ItemNota> repositoryItemNota;
        private readonly Services.IRepositoryGeneric<NotaFiscal> repositoryNota;
        private readonly Services.IRepositoryGeneric<Produto> repositoryProduto;

        public ItemNotasController(Services.IRepositoryGeneric<ItemNota> repoItemNota,
                                    Services.IRepositoryGeneric<NotaFiscal> repoNota,
                                    Services.IRepositoryGeneric<Produto> repoProduto)
        {
            repositoryItemNota = repoItemNota;
            repositoryNota = repoNota;
            repositoryProduto = repoProduto;
        }

        // GET: ItemNotas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = await repositoryItemNota
                .GetAllAsync(x=>true, x => x.NotaFiscal, x=>x.Produto);
            return View(applicationDbContext);
        }

        // GET: ItemNotas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemNota = await repositoryItemNota.GetAsync(id.Value);
            if (itemNota == null)
            {
                return NotFound();
            }

            return View(itemNota);
        }

        // GET: ItemNotas/Create
        public IActionResult Create()
        {
            ViewData["NotaFiscalId"] = new SelectList(repositoryNota.GetAll(), "Id", "Id");
            ViewData["ProdutoId"] = new SelectList(repositoryProduto.GetAll(), "Id", "Nome");
            return View();
        }

        // POST: ItemNotas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProdutoId,NotaFiscalId,ValorUnitario,Quantidade,PercentualDesconto")] ItemNota itemNota)
        {
            if (ModelState.IsValid)
            {
                await repositoryItemNota.InsertAsync(itemNota);
                return RedirectToAction(nameof(Index));
            }
            ViewData["NotaFiscalId"] = new SelectList(repositoryNota.GetAll(), "Id", "Id", itemNota.NotaFiscalId);
            ViewData["ProdutoId"] = new SelectList(repositoryProduto.GetAll(), "Id", "Nome", itemNota.ProdutoId);
            return View(itemNota);
        }

        // GET: ItemNotas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemNota = await repositoryItemNota.GetAsync(id.Value);

            if (itemNota == null)
            {
                return NotFound();
            }
            ViewData["NotaFiscalId"] = new SelectList(repositoryNota.GetAll(), "Id", "Id", itemNota.NotaFiscalId);
            ViewData["ProdutoId"] = new SelectList(repositoryProduto.GetAll(), "Id", "Nome", itemNota.ProdutoId);
            return View(itemNota);
        }

        // POST: ItemNotas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProdutoId,NotaFiscalId,ValorUnitario,Quantidade,PercentualDesconto")] ItemNota itemNota)
        {
            if (id != itemNota.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await repositoryItemNota.UpdateAsync(itemNota.Id, itemNota);
                return RedirectToAction(nameof(Index));
            }
            ViewData["NotaFiscalId"] = new SelectList(repositoryNota.GetAll(), "Id", "Id", itemNota.NotaFiscalId);
            ViewData["ProdutoId"] = new SelectList(repositoryProduto.GetAll(), "Id", "Nome", itemNota.ProdutoId);
            return View(itemNota);
        }

        // GET: ItemNotas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemNota = await repositoryItemNota.GetAsync(id);
            if (itemNota == null)
            {
                return NotFound();
            }

            return View(itemNota);
        }

        // POST: ItemNotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemNota = await repositoryItemNota.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
