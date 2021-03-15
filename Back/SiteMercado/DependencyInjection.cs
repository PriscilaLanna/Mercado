using Microsoft.Extensions.DependencyInjection;
using SiteMercado.Domain;
using SiteMercado.Infra;

namespace SiteMercado
{

    public static class DependencyInjection
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            //Repository
            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            //Service
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<IImagemService, ImagemService>();

            
        }
    }
}
