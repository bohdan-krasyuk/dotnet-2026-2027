using Domain.Products;
using Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ProductsConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(x => x.Value, x => new ProductId(x));

        builder.Property(x => x.Title)
            .HasColumnType("varchar(255)")
            .IsRequired();

        builder.Property(x => x.Status)
            .HasColumnType("varchar(32)")
            .HasConversion(x => x.ToString(), x => ProductStatus.From(x));

        builder.Property(x => x.CreatedAt)
            .HasConversion(new DateTimeUtcConverter())
            .HasDefaultValueSql("timezone('utc', now())")
            .IsRequired();

        builder.OwnsMany<ProductSpecification>(x => x.Specifications, specificationBuilder =>
        {
            specificationBuilder.ToJson("specifications");

            specificationBuilder.Property(x => x.Title).HasJsonPropertyName("title");
            specificationBuilder.Property(x => x.Value).HasJsonPropertyName("value");

            // specificationBuilder.OwnsOne<SubSpecification>(x => x.SubSpecification, subSpecificationBuilder =>
            // {
            //     subSpecificationBuilder.Property(x => x.OrderIndex).HasColumnName("order_index");
            // });
        });
    }
}