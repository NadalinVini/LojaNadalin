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

namespace Cliente.UI.Controllers
{
    [Authorize]
    public class ProdutosController : Controller
    {
        private readonly Services.IRepositoryGeneric<Produto> repositoryProduto;
        private readonly Services.IRepositoryGeneric<Marca> repositoryMarca;

        public ProdutosController(Services.IRepositoryGeneric<Produto> repoProduto,
                                  Services.IRepositoryGeneric<Marca> repoMarca  )
        {
            repositoryProduto = repoProduto;
            repositoryMarca = repoMarca;
        }

        // GET: Produtos
        public async Task<IActionResult> Index(int? id)
        {
            var applicationDbContext = await repositoryProduto.GetAllAsync(c => id == null || c.MarcaId == id, c => c.Marca);
            return View(applicationDbContext);
        }

        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await repositoryProduto.GetAsync(id.Value);
            ViewData["MarcaId"] = new SelectList(repositoryMarca.GetAll(), "MarcaId", "Nome");

            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // GET: Produtos/Create
        public IActionResult Create()
        {
            ViewData["MarcaId"] = new SelectList(repositoryMarca.GetAll(), "Id", "Nome");
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,MarcaId")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                await repositoryProduto.InsertAsync(produto);
                return RedirectToAction(nameof(Index));
            }
            ViewData["MarcaId"] = new SelectList(await repositoryProduto.GetAllAsync(), "MarcaId", "Nome", produto.MarcaId);
            return View(produto);
        }

        // GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await repositoryProduto.GetAsync(id.Value);

            if (produto == null)
            {
                return NotFound();
            }

            var list = await repositoryMarca.GetAllAsync();

            ViewData["MarcaId"] = new SelectList(list, "Id", "Nome", produto.MarcaId);
            return View(produto);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nome,MarcaId")] Produto produto)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await repositoryProduto.UpdateAsync(produto.Id, produto);
                return RedirectToAction(nameof(Index));
            } 

            ViewData["MarcaId"] = new SelectList(repositoryMarca.GetAll(), "Id", "Id", produto.MarcaId);
            return View(produto);
        }

        // GET: Produtos/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await repositoryProduto.GetAsync(id);

            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await repositoryProduto.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
