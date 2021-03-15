using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SiteMercado.Domain
{
    public interface IProdutoRepository : IBaseRepository
    {
        Task<List<Produto>> GetSearch(Expression<Func<Produto, bool>> predicate, int page, int limit);    
    }
}
