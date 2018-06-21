using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Cliente.UI.Data;
using Cliente.UI.Models;
using Microsoft.EntityFrameworkCore;

namespace Cliente.UI.Services
{
    public class EnderecoRepository : RepositoryGeneric<Models.Endereco>
    {
        public EnderecoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override List<Endereco> GetAll(Expression<Func<Endereco, bool>> where, params Expression<Func<Endereco, object>>[] includeProperties)
        {
            IQueryable<Models.Endereco> queryable = DbSet;
            var list = queryable
                .Include(e => e.Cidade)
                    .ThenInclude(c => c.Estado)
                .ToList();

            return list;
        }

        public async override Task<List<Endereco>> GetAllAsync(Expression<Func<Endereco, bool>> where, params Expression<Func<Endereco, object>>[] includeProperties)
        {
            IQueryable<Models.Endereco> queryable = DbSet;

            var list = await queryable
                .Include(e => e.Cidade)
                    .ThenInclude(c => c.Estado)
                .ToListAsync();

            return list;
        }
    }
}
