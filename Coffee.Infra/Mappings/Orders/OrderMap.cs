using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Coffee.Domain.Models.Orders;

namespace Coffee.Infra.Mappings.Orders;

public class OrderMap : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        // Tabela
        builder.ToTable("Order");

        // É necessário ignorar a propriedade Notifications para nao ser mapeada
        builder.Ignore(x => x.Notifications);

        // Chave Primária
        builder.HasKey(x => x.Id);

        // Propriedades
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
            .HasIndex(x => x.CustomerId, "IX_Order_CustomerId")
            .IsUnique();
    }
}