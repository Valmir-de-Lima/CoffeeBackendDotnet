using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Coffee.Domain.Models.Orders.Items;


namespace Coffee.Infra.Mappings.Items;

public class ItemMap : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        // Tabela
        builder.ToTable("Item");

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

        builder.Property(x => x.UnitPrice)
            .IsRequired()
            .HasColumnName("UnitPrice")
            .HasColumnType("decimal(18, 2)");

        builder.Property(x => x.Quantity)
            .IsRequired()
            .HasColumnName("Quantity")
            .HasColumnType("integer");

        builder.Property(x => x.TotalPrice)
            .IsRequired()
            .HasColumnName("TotalPrice")
            .HasColumnType("decimal(18, 2)");

        builder.Property(x => x.IsCoffee)
            .IsRequired()
            .HasColumnName("IsCoffee")
            .HasColumnType("bit");

        // Relacionamento um (Order) para muitos (Items)
        builder
            .HasOne(x => x.Order)
                .WithMany(x => x.Items)
            .HasConstraintName("FK_Items_Order")
            .OnDelete(DeleteBehavior.Cascade);

        // Relacionamento um (Item) para muitos (Ingredients)
        builder
            .HasMany(x => x.Ingredients);

        // Índices
        builder
            .HasIndex(x => x.Description, "IX_Item_Description");
    }
}