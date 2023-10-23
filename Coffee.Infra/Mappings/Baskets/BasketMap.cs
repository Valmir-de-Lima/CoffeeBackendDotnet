using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Coffee.Domain.Models.Baskets;

namespace Coffee.Infra.Mappings.Baskets;

public class BasketMap : IEntityTypeConfiguration<Basket>
{
    public void Configure(EntityTypeBuilder<Basket> builder)
    {
        // Tabela
        builder.ToTable("Basket");

        // É necessário ignorar a propriedade Notifications para nao ser mapeada
        builder.Ignore(x => x.Notifications);

        // Chave Primária
        builder.HasKey(x => x.Id);

        // Propriedades
        builder.Property(x => x.CustomerId)
            .IsRequired()
            .HasColumnName("CustumerId")
            .HasColumnType("uniqueidentifier");

        builder.Property(x => x.Price)
            .IsRequired()
            .HasColumnName("UnitPrice")
            .HasColumnType("decimal(18, 2)");

        builder.Property(x => x.Quantity)
            .IsRequired()
            .HasColumnName("Quantity")
            .HasColumnType("integer");

        // Índices
        builder
            .HasIndex(x => x.CustomerId, "IX_Basket_CustomerId")
            .IsUnique();
    }
}