using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using SiteMercado.Domain;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SiteMercado.Api.Filters;
namespace SiteMercado.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        public readonly IProdutoService _produtoService;
       
        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "Retorna o produto pelo Id", typeof(Produto))]
        [SwaggerOperation(Summary = "Retorna o produto")]
        public async Task<ActionResult> GetProduto(
            [SwaggerParameter("Id do produto")][BindRequired] int id)            
        {
            var result = await _produtoService.Get(id);

            return Ok(result);
        }

        [HttpGet()]
        [SwaggerResponse(200, "Retorna lista de produtos", typeof(List<Produto>))]
        [SwaggerOperation(Summary = "Retorna lista de produtos")]
        public async Task<ActionResult> GetProdutosAll()
        {
            var result = await _produtoService.GetList();

            return Ok(result);
        }

        [HttpGet("{page?}/{limit?}")]
        [SwaggerResponse(200, "Retorna lista de produtos paginados", typeof(List<Produto>))]
        [SwaggerOperation(Summary = "Retorna lista de produtos paginados")]
        public async Task<ActionResult> GetProdutos(
            [SwaggerParameter("Pagina")][BindRequired] int page = 1,
            [SwaggerParameter("Quantidade por pagina")] int limit = 10)
        {
            var result = await _produtoService.GetList(page, limit);

            return Ok(result);
        }

        [HttpGet("{nome}/{page?}/{limit?}")]
        [SwaggerResponse(200, "Retorna lista de produtos paginados pesquisados pelo nome", typeof(List<Produto>))]
        [SwaggerOperation(Summary = "Retorna lista de produtos paginados pesquisados pelo nome")]
        public async Task<ActionResult> GetProdutos(
            [SwaggerParameter("Nome")][BindRequired] string nome,
            [SwaggerParameter("Pagina")][BindRequired] int page = 1,
            [SwaggerParameter("Quantidade por pagina")] int limit = 10)
        {
            var result = await _produtoService.GetSearch(nome, page, limit);

            return Ok(result);
        }

        [HttpPut()]
        [SwaggerResponse(200, "Altera o produto", typeof(Produto))]
        [SwaggerOperation(Summary = "Altera o produto")]
        public async Task<ActionResult> PutProduto(
            [SwaggerParameter("Produto")][FromBody] Produto produto)
        {
            var result = await _produtoService.Update(produto);

            return Ok(result);
        }

        [HttpPost()]
        [SwaggerResponse(201, "Salva o produto", typeof(Produto))]
        [SwaggerOperation(Summary = "Salva o produto")]
       // [ValidatePhotosFilter]
        public async Task<ActionResult> PostProduto(
            [SwaggerParameter("Produto")][FromBody] Produto produto)
        {

            var result = await _produtoService.Save(produto);
            return Created(new Uri($"{Request.Path}/{result.Id}", UriKind.Relative), result);
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(204, "Deleta o produto")]
        [SwaggerOperation(Summary = "Deletar o produto")]
        public async Task<ActionResult> DeleteProduto(
            [SwaggerParameter("Id do produto")][BindRequired] int id)
        {
            await _produtoService.Delete(id);
            return NoContent();
        }
    }
}
