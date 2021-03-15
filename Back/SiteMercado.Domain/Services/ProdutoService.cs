using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteMercado.Domain
{
    public class ProdutoService : IProdutoService
    {
        protected readonly IProdutoRepository _produtoRepository;
        private readonly IValidator<Produto> _validator;
        private readonly ILogger<ProdutoService> _logger;
        private readonly IImagemService _imagemService;
        private readonly IConfiguration _config;
        private string _urlHostFiles;

        public ProdutoService(IProdutoRepository produtoRepository,
                                IValidator<Produto> validator,
                                IImagemService imagemService,
                                ILogger<ProdutoService> logger,
                                IConfiguration config)
        {
            _produtoRepository = produtoRepository;
            _validator = validator;
            _logger = logger;
            _imagemService = imagemService;
            _config = config;

            _urlHostFiles = ((IConfigurationSection)_config.GetSection("HostFiles")).Value;
        }

        public async Task<Produto> Get(int id)
        {
            if (id <= 0)
                return null;

            _logger.LogInformation($"INICIANDO BUSCA DE PRODUTO PELO ID {id}");

            var result = await _produtoRepository.Get(id);

            if (result == null)
                return null;

            setUrlImage(result);

            _logger.LogInformation($"RETORNO DA BUSCA DO PRODUTO {result.Nome}");

            return result;
        }

        public async Task<List<Produto>> GetList()
        {
            _logger.LogInformation($"INICIANDO BUSCA DE PRODUTOS");

            var result = await _produtoRepository.GetList();

            if (result == null)
                return null;

            result.ForEach(p => setUrlImage(p));

            _logger.LogInformation($"RETORNO DA BUSCA DE PRODUTOS -> TOTAL {result.Count}");

            return result;
        }

        public async Task<List<Produto>> GetList(int page, int limit)
        {
            if (page <= 0)
                return null;

            if (limit <= 0)
                return null;

            _logger.LogInformation($"INICIANDO BUSCA DE PRODUTOS");

            var result = await _produtoRepository.GetList(page, limit);

            if (result == null)
                return null;

            result.ForEach(p => setUrlImage(p));

            _logger.LogInformation($"RETORNO DA BUSCA DE PRODUTOS -> TOTAL {result.Count}");

            return result;
        }

        public async Task<List<Produto>> GetSearch(string nome, int page, int limit)
        {
            if (page <= 0)
                return null;

            if (limit <= 0)
                return null;

            _logger.LogInformation($"INICIANDO BUSCA DE PRODUTOS PELO NOME DO PRODUTO {nome}");

            var result = await _produtoRepository.GetSearch(x => x.Nome.Contains(nome), page, limit);

            if (result == null)
                return null;

            result.ForEach(p => setUrlImage(p));

            _logger.LogInformation($"RETORNO DA BUSCA DE PRODUTOS -> TOTAL {result.Count}");

            return result;
        }

        public async Task<Produto> Update(Produto produto)
        {
            _logger.LogInformation($"INICIANDO ATUALIZAÇÃO DO PRODUTO ID {produto.Id}");
               
            if (!this._imagemService.IsUrlValidaIfExists(produto.Imagem))
            {
                _logger.LogInformation($"INICIANDO IMAGEM");

                produto.Imagem = this._imagemService.GerarUrl(produto.Imagem);
            }

            var result = await _produtoRepository.Update(produto);

            _logger.LogInformation($"RETORNO DA ATUALIZAÇÃO DE PRODUTO");

            return result;
        }

        public async Task<Produto> Save(Produto produto)
        {
            _logger.LogInformation($"INICIANDO SALVAR O PRODUTO {produto.Nome}");

            await _validator.ValidateAndThrowAsync(produto);

            _logger.LogInformation($"PRODUTO VALIDADO");

            if (!this._imagemService.IsUrlValidaIfExists(produto.Imagem))
            {
                _logger.LogInformation($"INICIANDO IMAGEM");
                produto.Imagem = this._imagemService.GerarUrl(produto.Imagem);
            }

            var id = await _produtoRepository.Save(produto);
            produto.Id = id;

            _logger.LogInformation($"RETORNO PRODUTO SALVO");

            return produto;
        }

        public async Task<bool> Delete(int id)
        {
            _logger.LogInformation($"INICIANDO EXCLUSÃO DO PRODUTO ID {id}");

            var result = await _produtoRepository.Delete(id);

            _logger.LogInformation($"RETORNO PRODUTO EXCLUÍDO");

            return result;
        }
        private void setUrlImage(Produto result)
        {
            if (string.IsNullOrEmpty(result.Imagem)) return;
            if(!this._imagemService.IsUrlValidaIfExists(result.Imagem))
             result.Imagem = string.IsNullOrEmpty(result.Imagem) ? "" : $"{_urlHostFiles}{result.Imagem}";
        }
    }
}
