using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Coffee.Domain.Models.Product;


namespace Coffee.Infra.Mappings.Products;

public class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        // Tabela
        builder.ToTable("Product");

        // É necessário ignorar a propriedade Notifications para nao ser mapeada
        builder.Ignore(x => x.Notifications);

        // Chave Primária
        builder.HasKey(x => x.Id);

        // Configurar índice composto para CustomerId e CoffeId
        //builder.HasIndex(x => new { x.CustomerId, x.CoffeId }).IsUnique();

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

        // Relacionamento um (Basket) para muitos (Produts)
        builder
            .HasOne(x => x.Basket)
                .WithMany(x => x.Products)
            .HasConstraintName("FK_Products_Basket")
            .OnDelete(DeleteBehavior.Cascade);

        // Relacionamento um (Product) para muitos (Ingredients)
        builder
            .HasMany(x => x.Ingredients);

        // Índices
        builder
            .HasIndex(x => x.Description, "IX_Product_Description");
    }
}