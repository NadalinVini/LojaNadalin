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
    public class TipoPagamentosController : Controller
    {
        private readonly Services.IRepositoryGeneric<TipoPagamento> repositoryTipoPagamento;
        private readonly Services.IRepositoryGeneric<FormaPagamento> repositoryFormaPagamento;

        public TipoPagamentosController(Services.IRepositoryGeneric<TipoPagamento> repoTipopagamento,
                                        Services.IRepositoryGeneric<FormaPagamento> repoFormapagamento)
        {
            repositoryTipoPagamento = repoTipopagamento;
            repositoryFormaPagamento = repoFormapagamento;
        }

        // GET: TipoPagamentos
        public async Task<IActionResult> Index(int? id)
        {
            var applicationDbContext = await repositoryTipoPagamento.GetAllAsync(c => id == null || c.FormaPagamentoId == id, c => c.FormaPagamento);
            return View(applicationDbContext);
        }

        // GET: TipoPagamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoPagamento = await repositoryTipoPagamento.GetAsync(id.Value);

            if (tipoPagamento == null)
            {
                return NotFound();
            }

            return View(tipoPagamento);
        }

        // GET: TipoPagamentos/Create
        public IActionResult Create()
        {
            ViewData["FormaPagamentoId"] = new SelectList(repositoryFormaPagamento.GetAll(), "Id", "Id");
            return View();
        }

        // POST: TipoPagamentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Descricao,FormaPagamentoId")] TipoPagamento tipoPagamento)
        {
            if (ModelState.IsValid)
            {
                await repositoryTipoPagamento.InsertAsync(tipoPagamento);
                return RedirectToAction(nameof(Index));
            }
            ViewData["FormaPagamentoId"] = new SelectList(await repositoryTipoPagamento.GetAllAsync(), "FormaPagamentoId", "Nome", tipoPagamento.FormaPagamentoId);
            return View(tipoPagamento);
        }

        // GET: TipoPagamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoPagamento = await repositoryTipoPagamento.GetAsync(id.Value);
            if (tipoPagamento == null)
            {
                return NotFound();
            }
            ViewData["FormaPagamentoId"] = new SelectList(await repositoryTipoPagamento.GetAllAsync(), "FormaPagamentoId", "Nome", tipoPagamento.FormaPagamentoId);
            return View(tipoPagamento);
        }

        // POST: TipoPagamentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao,FormaPagamentoId")] TipoPagamento tipoPagamento)
        {
            if (id != tipoPagamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await repositoryTipoPagamento.UpdateAsync(tipoPagamento.Id, tipoPagamento);
                return RedirectToAction(nameof(Index));
            }
            ViewData["FormaPagamentoId"] = new SelectList(await repositoryTipoPagamento.GetAllAsync(), "FormaPagamentoId", "Nome", tipoPagamento.FormaPagamentoId);
            return View(tipoPagamento);
        }

        // GET: TipoPagamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoPagamento = await repositoryTipoPagamento.GetAsync(id);
            if (tipoPagamento == null)
            {
                return NotFound();
            }

            return View(tipoPagamento);
        }

        // POST: TipoPagamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await repositoryTipoPagamento.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
       
    }
}
