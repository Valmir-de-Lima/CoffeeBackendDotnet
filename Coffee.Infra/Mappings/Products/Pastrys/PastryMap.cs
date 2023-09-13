using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Coffee.Domain.Models.Product.Pastry;

namespace Coffee.Infra.Mappings.Products.Pastrys;

public class PastryMap : IEntityTypeConfiguration<Pastry>
{
    public void Configure(EntityTypeBuilder<Pastry> builder)
    {
        // Tabela
        builder.ToTable("Ingredient");

        // É necessário ignorar a propriedade Notifications para nao ser mapeada
        builder.Ignore(x => x.Notifications);

        // Chave Primária
        builder.HasKey(x => x.Id);

        // Propriedades
        builder.Property(x => x.Description)
            .IsRequired()
            .HasColumnName("Description")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);

        builder.Property(x => x.Price)
            .IsRequired()
            .HasColumnName("Description")
            .HasColumnType("decimal(18, 2)");

        // Índices
        builder
            .HasIndex(x => x.Description, "IX_Ingredient_Description")
            .IsUnique();
    }
}