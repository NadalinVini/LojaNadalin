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
    public class FormaPagamentoesController : Controller
    {
        private readonly Services.IRepositoryGeneric<FormaPagamento> repositoryFormaPagamento;
               

        public FormaPagamentoesController(Services.IRepositoryGeneric<FormaPagamento> repoFormaPagamento)
        {
            repositoryFormaPagamento = repoFormaPagamento;
        }

        // GET: FormaPagamentoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = await repositoryFormaPagamento.GetAllAsync();
            return View(applicationDbContext);
        }

        // GET: FormaPagamentoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formaPagamento = await repositoryFormaPagamento.GetAsync(id.Value);
            if (formaPagamento == null)
            {
                return NotFound();
            }

            return View(formaPagamento);
        }

        // GET: FormaPagamentoes/Create
        public IActionResult Create()
        {
            ViewData["FormaPagamentoId"] = new SelectList(repositoryFormaPagamento.GetAll(), "FormaPagamentoId", "Nome");
            return View();
        }

        // POST: FormaPagamentoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Descricao")] FormaPagamento formaPagamento)
        {
            if (ModelState.IsValid)
            {                
                await repositoryFormaPagamento.InsertAsync(formaPagamento);
                return RedirectToAction(nameof(Index));
            }
            ViewData["FormaPagamentoId"] = new SelectList(repositoryFormaPagamento.GetAll(), "FormaPagamentoId", "Nome");
            return View(formaPagamento);
        }

        // GET: FormaPagamentoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formaPagamento = await repositoryFormaPagamento.GetAsync(id.Value);

            if (formaPagamento == null)
            {
                return NotFound();
            }
            ViewData["FormaPagamentoId"] = new SelectList(repositoryFormaPagamento.GetAll(), "FormaPagamentoId", "Nome");
            return View(formaPagamento);
        }

        // POST: FormaPagamentoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao")] FormaPagamento formaPagamento)
        {
            if (id != formaPagamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await repositoryFormaPagamento.UpdateAsync(id, formaPagamento);

                return RedirectToAction(nameof(Index));
            }
            ViewData["FormaPagamentoId"] = new SelectList(repositoryFormaPagamento.GetAll(), "FormaPagamentoId", "Nome");
            return View(formaPagamento);
        }

        // GET: FormaPagamentoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formaPagamento = await repositoryFormaPagamento.GetAsync(id);
            if (formaPagamento == null)
            {
                return NotFound();
            }

            return View(formaPagamento);
        }

        // POST: FormaPagamentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await repositoryFormaPagamento.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }        
    }
}
