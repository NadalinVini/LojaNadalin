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
    public class NotasFiscaisController : Controller
    {
        private readonly Services.IRepositoryGeneric<NotaFiscal> repositoryNota;
        private readonly Services.IRepositoryGeneric<TipoPagamento> repositoryPagamento;

        public NotasFiscaisController(Services.IRepositoryGeneric<NotaFiscal> repoNota,
                                      Services.IRepositoryGeneric<TipoPagamento> repoPagamento)
        {
            repositoryNota = repoNota;
            repositoryPagamento = repoPagamento;
        }

        // GET: NotasFiscais
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = await repositoryNota
                .GetAllAsync(x=>true, x=>x.TipoPagamento);
            return View(applicationDbContext);
        }

        // GET: NotasFiscais/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notaFiscal = await repositoryNota.GetAsync(id);
            if (notaFiscal == null)
            {
                return NotFound();
            }

            return View(notaFiscal);
        }

        // GET: NotasFiscais/Create
        public IActionResult Create()
        {
            ViewData["TipoPagamentoId"] = new SelectList(repositoryPagamento.GetAll(), "Id", "Nome");
            return View();
        }

        // POST: NotasFiscais/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataEmissao,TipoPagamentoId,ClienteId")] NotaFiscal notaFiscal)
        {
            if (ModelState.IsValid)
            {
                await repositoryNota.InsertAsync(notaFiscal);
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoPagamentoId"] = new SelectList(repositoryPagamento.GetAll(), "Id", "Nome", notaFiscal.TipoPagamentoId);
            return View(notaFiscal);
        }

        // GET: NotasFiscais/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notaFiscal = await repositoryNota.GetAsync(id);

            if (notaFiscal == null)
            {
                return NotFound();
            }
            var list = await repositoryPagamento.GetAllAsync();

            ViewData["TipoPagamentoId"] = new SelectList(repositoryPagamento.GetAll(), "Id", "Nome", notaFiscal.TipoPagamentoId);
            return View(notaFiscal);
        }

        // POST: NotasFiscais/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DataEmissao,TipoPagamentoId,ClienteId")] NotaFiscal notaFiscal)
        {
            if (id != notaFiscal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await repositoryNota.UpdateAsync(notaFiscal.Id, notaFiscal);
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoPagamentoId"] = new SelectList(repositoryPagamento.GetAll(), "Id", "Nome"); 
            return View(notaFiscal);
        }

        // GET: NotasFiscais/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notaFiscal = await repositoryNota.GetAsync(id);

;            if (notaFiscal == null)
            {
                return NotFound();
            }

            return View(notaFiscal);
        }

        // POST: NotasFiscais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var notaFiscal = await repositoryNota.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
       
    }
}
