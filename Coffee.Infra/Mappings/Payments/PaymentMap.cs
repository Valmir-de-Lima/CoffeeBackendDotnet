using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Coffee.Domain.Models.Payments;

namespace Coffee.Infra.Mappings.Payments;

public class PaymentMap : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        // Tabela
        builder.ToTable("Payment");

        // É necessário ignorar a propriedade Notifications para nao ser mapeada
        builder.Ignore(x => x.Notifications);

        // Chave Primária
        builder.HasKey(x => x.Id);

        // Propriedades
        builder.Property(x => x.CustomerId)
            .IsRequired()
            .HasColumnName("CustumerId")
            .HasColumnType("uniqueidentifier");

        builder.Property(x => x.Type)
            .IsRequired()
            .HasColumnName("Type")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);

        builder.Property(x => x.Price)
            .IsRequired()
            .HasColumnName("UnitPrice")
            .HasColumnType("decimal(18, 2)");

        builder.Property(x => x.Date)
            .IsRequired()
            .HasColumnName("Date")
            .HasColumnType("datetime2");

        builder.Property(x => x.OrderId)
            .IsRequired()
            .HasColumnName("OrderId")
            .HasColumnType("uniqueidentifier");

        // Relacionamento um (Order) para um (Payment)
        builder
            .HasOne(x => x.Order)
                .WithOne(x => x.Payment)
            .HasConstraintName("FK_Order_Payment");

        // Índices
        builder
            .HasIndex(x => x.CustomerId, "IX_Order_CustomerId")
            .IsUnique();

        builder
            .HasIndex(x => x.OrderId, "IX_Order_OrderId")
            .IsUnique();

    }
}