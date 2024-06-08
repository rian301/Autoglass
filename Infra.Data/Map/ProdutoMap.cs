using Autoglass.Infra.Data.Extensions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Autoglass.Infra.Data.Map
{
    public class ProdutoMap : EntityTypeConfiguration<Produto>
    {
        public override void Map(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produto");
            builder.HasKey(x => x.Id);

            builder.Property(p => p.CodigoProduto).IsRequired();
            builder.Property(p => p.Descricao).IsRequired();
            builder.Property(p => p.Situacao).IsRequired();
            builder.Property(p => p.DataFabricacao).IsRequired();
            builder.Property(p => p.DataValidade).IsRequired();
            builder.Property(p => p.CodigoFornecedor).IsRequired();
            builder.Property(p => p.DescricaoFornecedor).IsRequired();
            builder.Property(p => p.CNPJFornecedor).IsRequired();

            builder.Ignore(x => x.CascadeMode);
            builder.Ignore(x => x.ValidationResult);
        }
    }
}