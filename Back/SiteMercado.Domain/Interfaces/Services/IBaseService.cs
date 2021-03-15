using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteMercado.Domain
{
    public interface IBaseService
    {
        Task<Produto> Get(int id);
        Task<List<Produto>> GetList();
        Task<List<Produto>> GetList(int page, int limit);
        Task<Produto> Update(Produto produto);
        Task<Produto> Save(Produto produto);
        Task<bool> Delete(int id);
    }
}
