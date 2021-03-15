using Microsoft.EntityFrameworkCore;
using SiteMercado.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SiteMercado.Infra
{
    public class ProdutoRepository : IProdutoRepository
    {

        protected readonly ApplicationDbContext _dbContext;
        public ProdutoRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Produto> Get(int id)
        {
            return await _dbContext.Produtos.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Produto>> GetList()
        {
            return await _dbContext.Produtos                
                .OrderBy(x => x.Nome)
                .ToListAsync();
        }

        public async Task<List<Produto>> GetList(int page, int limit)
        {
            return await _dbContext.Produtos
                .Skip(limit * (page -1))
                .Take(limit)
                .OrderBy(x => x.Nome)
                .ToListAsync();
        }

        public async Task<List<Produto>> GetSearch(Expression<Func<Produto, bool>> predicate, int page, int limit)
        {
            return await _dbContext.Produtos.Where(predicate)
                                            .Skip(limit * (page - 1))
                                            .Take(limit)
                                            .OrderBy(x => x.Nome)
                                            .ToListAsync();
        }

        public async Task<Produto> Update(Produto produto)
        {
            if (produto.Id <= 0)
                return null;

            var entity = await _dbContext.Produtos.FindAsync(produto.Id);

            if (entity == null)
                return entity;

            entity.Nome = produto.Nome;
            entity.Valor = produto.Valor;
            entity.Imagem = produto.Imagem;

            try
            {
                _dbContext.Produtos.Update(entity);
                await _dbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<int> Save(Produto produto)
        {
            if (produto.Id > 0)
                return 0;

            var entity = new Produto()
            {
                Nome = produto.Nome,
                Valor = produto.Valor,
                Imagem = produto.Imagem
            };

            try
            {
                await _dbContext.Produtos.AddAsync(entity);
                await _dbContext.SaveChangesAsync();

                return entity.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<bool> Delete(int id)
        {
            if (id <= 0)
                return false;

            var entity = await _dbContext.Produtos.FindAsync(id);

            if (entity == null)
                return false;

            try
            {
                _dbContext.Produtos.Remove(entity);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
