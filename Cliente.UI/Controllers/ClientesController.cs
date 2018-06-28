using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cliente.UI.Data;
using Cliente.UI.Models;
using Microsoft.AspNetCore.Identity;

namespace Cliente.UI.Controllers
{
    public class ClientesController : Controller
    {
        private readonly Services.IRepositoryGeneric<Endereco> repositoryEndereco;
        private readonly Services.IRepositoryGeneric<Cliente.UI.Models.Cliente> repositoryCliente;
        private readonly UserManager<Models.Cliente> _userManager;


        public ClientesController(Services.IRepositoryGeneric<Cliente.UI.Models.Cliente> repoCliente,
                                  Services.IRepositoryGeneric<Endereco> repoEndereco,
                                  UserManager<Models.Cliente> userManager)
            
        {
            repositoryCliente = repoCliente;
            repositoryEndereco = repoEndereco;            
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {            
            var applicationDbContext = await repositoryCliente
                .GetAllAsync(x=>true, x=>x.Endereco);
            return View(applicationDbContext);
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            var cliente = await repositoryCliente.GetAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            ViewData["EnderecoId"] = new SelectList(repositoryEndereco.GetAll(), "Id", "Cep");
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Cpf,EnderecoId,Numero,Complemento,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] Cliente.UI.Models.Cliente cliente)
        {
            if (ModelState.IsValid)
           {                                
                await repositoryCliente.InsertAsync(cliente);
                return RedirectToAction(nameof(Index));
            }
            ViewData["EnderecoId"] = new SelectList(repositoryEndereco.GetAll(), "Id", "Cep", cliente.EnderecoId);
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            var cliente = await repositoryCliente.GetAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            var list = await repositoryEndereco.GetAllAsync();

            ViewData["EnderecoId"] = new SelectList(repositoryEndereco.GetAll(), "Id", "Cep", cliente.EnderecoId);
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Nome,Cpf,EnderecoId,Numero,Complemento,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] Cliente.UI.Models.Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await repositoryCliente.UpdateAsync(cliente.Id, cliente);

                return RedirectToAction(nameof(Index));
            }

            ViewData["EnderecoId"] = new SelectList(repositoryEndereco.GetAll(), "Id", "Cep");
            return View(cliente);
            
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await repositoryCliente.GetAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cliente = await repositoryCliente.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
