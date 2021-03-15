using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteMercado.Domain
{
    public interface IProdutoService : IBaseService
    {
        Task<List<Produto>> GetSearch(string nome, int page, int limit);    
    }
}
