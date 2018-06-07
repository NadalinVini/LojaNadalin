using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cliente.UI.Data;
using Cliente.UI.Models;
using Microsoft.EntityFrameworkCore;

namespace Cliente.UI.Services
{
    public class EnderecoRepository : RepositoryGeneric<Models.Endereco>
    {
        public EnderecoRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public override List<Endereco> GetAll()
        {
            return DbSet
                .Include(e => e.Cidade)
                .ToList();
        }
    }
}
