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
    public class PessoasController : Controller
    {
        private readonly Services.IRepositoryGeneric<Pessoa> repositoryPessoa;
        private readonly Services.IRepositoryGeneric<Endereco> repositoryEndereco;

        public PessoasController(Services.IRepositoryGeneric<Pessoa> repoPessoa,
                                Services.IRepositoryGeneric<Endereco> repoEndereco)
        {
            repositoryPessoa = repoPessoa;
            repositoryEndereco = repoEndereco;
        }

        // GET: Pessoas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = await repositoryPessoa.GetAllAsync();
            return View(applicationDbContext);
        }

        // GET: Pessoas/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await repositoryPessoa.GetAsync(id.Value);
            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        // GET: Pessoas/Create
        public IActionResult Create()
        {
            ViewData["EnderecoId"] = new SelectList(repositoryEndereco.GetAll(), "Id", "Cep");
            return View();
        }

        // POST: Pessoas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Cpf,EnderecoId,Numero,Complemento,Password")] Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                await repositoryPessoa.InsertAsync(pessoa);
                return RedirectToAction(nameof(Index));
            }
            ViewData["EnderecoId"] = new SelectList(await repositoryEndereco.GetAllAsync(), "Id", "Cep", pessoa.EnderecoId);
            return View(pessoa);
        }

        // GET: Pessoas/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await repositoryPessoa.GetAsync(id.Value);
            if (pessoa == null)
            {
                return NotFound();
            }
            ViewData["EnderecoId"] = new SelectList(repositoryEndereco.GetAll(), "Id", "Cep", pessoa.EnderecoId);
            return View(pessoa);
        }

        // POST: Pessoas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nome,Cpf,EnderecoId,Numero,Complemento,Password")] Pessoa pessoa)
        {
            if (id != pessoa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await repositoryPessoa.UpdateAsync(pessoa.Id, pessoa);
                return RedirectToAction(nameof(Index));
            }
            ViewData["EnderecoId"] = new SelectList(repositoryEndereco.GetAll(), "Id", "Cep");
            return View(pessoa);
        }

        // GET: Pessoas/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await repositoryPessoa.GetAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        // POST: Pessoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await repositoryPessoa.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
