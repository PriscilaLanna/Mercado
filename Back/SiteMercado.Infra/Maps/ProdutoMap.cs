using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiteMercado.Domain;

namespace SiteMercado.Infra
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos");
          
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").ValueGeneratedOnAdd();

            builder.Property(x => x.Nome).HasColumnName("Nome").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Valor).HasColumnName("Valor").HasPrecision(17,2).IsRequired();
            builder.Property(x => x.Imagem).HasColumnName("Imagem").HasMaxLength(255);
        }
    }
}