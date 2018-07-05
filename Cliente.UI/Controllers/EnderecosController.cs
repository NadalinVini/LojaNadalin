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
    public class EnderecosController : Controller
    {
        private readonly Services.IRepositoryGeneric<Endereco> repositoryEndereco;
        private readonly Services.IRepositoryGeneric<Cidade> repositoryCidade;
        private readonly Services.IRepositoryGeneric<Estado> repositoryEstado;

        public EnderecosController(
            Services.IRepositoryGeneric<Endereco> repoEndereco,
            Services.IRepositoryGeneric<Cidade> repoCidade,
            Services.IRepositoryGeneric<Estado> repoEstado)
        {
            repositoryEndereco = repoEndereco;
            repositoryCidade = repoCidade;
            repositoryEstado = repoEstado;
        }

        // GET: Enderecoes
        public async Task<IActionResult> Index()
        {
            var list = await repositoryEndereco
                .GetAllAsync(null, e => e.Cidade);
            return View(list);
        }

        // GET: Enderecoes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var endereco = await repositoryEndereco.GetAsync(id.Value);
            ViewData["CidadeId"] = new SelectList(repositoryCidade.GetAll(), "CidadeId", "Nome");
            if (endereco == null)
            {
                return NotFound();
            }

            return View(endereco);
        }

        public List<Cidade> GetCidades(long? id)
        {
            if (id == null)
                return null;

            return repositoryCidade
                .Filter(c => c.EstadoId == id.Value);
        }

        // GET: Enderecoes/Create
        public IActionResult Create()
        {
            ViewData["CidadeId"] = new SelectList(new List<Cidade>
            { new Cidade { Nome = "Selecione o Estado" } }, "Id", "Nome");

            var estadoList = repositoryEstado.GetAll();
            estadoList.Insert(0, new Estado { Nome = "Selecione" });

            ViewData["EstadoId"] = new SelectList(estadoList, "Id", "Nome");
            return View();
        }

        // POST: Enderecoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CidadeId,Cep")] Endereco endereco)
        {
            
            if (ModelState.IsValid && endereco.CidadeId > 0)
            {
                await repositoryEndereco.InsertAsync(endereco);
                return RedirectToAction(nameof(Index));
            }

            ViewData["CidadeId"] = new SelectList(new List<Cidade>
            { new Cidade { Nome = "Selecione o Estado" } }, "Id", "Nome", endereco.CidadeId);

            var estadoList = repositoryEstado.GetAll();
            estadoList.Insert(0, new Estado { Nome = "Selecione" });

            ViewData["EstadoId"] = new SelectList(estadoList, "Id", "Nome");
            return View(endereco);
        }

        // GET: Enderecoes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var endereco = await repositoryEndereco
                .GetByAsync(e => e.Id == id, e => e.Cidade);
            if (endereco == null)
            {
                return NotFound();
            }

            ViewData["CidadeId"] = new SelectList(new List<Cidade>
            { new Cidade { Nome = "Selecione o Estado", Id = -1} }, "Id", "Nome", endereco.CidadeId);
            ViewData["EstadoId"] = new SelectList(repositoryEstado.GetAll(), "Id", "Nome", endereco.Cidade.EstadoId);
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
                await repositoryEndereco.UpdateAsync(endereco.Id, endereco);

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

            var endereco = await repositoryEndereco.GetAsync(id);
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
            await repositoryEndereco.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
