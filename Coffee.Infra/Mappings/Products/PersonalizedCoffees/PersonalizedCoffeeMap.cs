using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Coffee.Domain.Models.Product.PersonalizedCoffee;
using Coffee.Domain.Models.Product.PersonalizedCoffee.Ingredient;


namespace Coffee.Infra.Mappings.Products.PersonalizedCoffees;

public class PersonalizedCoffeeMap : IEntityTypeConfiguration<PersonalizedCoffee>
{
    public void Configure(EntityTypeBuilder<PersonalizedCoffee> builder)
    {
        // Tabela
        builder.ToTable("PersonalizedCoffee");

        // É necessário ignorar a propriedade Notifications para nao ser mapeada
        builder.Ignore(x => x.Notifications);

        // Chave Primária
        builder.HasKey(x => x.Id);

        // Configurar índice composto para CustomerId e CoffeId
        //builder.HasIndex(x => new { x.CustomerId, x.CoffeId }).IsUnique();

        // Propriedades
        builder.Property(x => x.DescriptionCoffe)
            .IsRequired()
            .HasColumnName("DescriptionCoffe")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);

        builder.Property(x => x.PriceCoffe)
            .IsRequired()
            .HasColumnName("PriceCoffe")
            .HasColumnType("decimal(18, 2)");

        builder.Property(x => x.QuantityIngredient)
            .IsRequired()
            .HasColumnName("QuantityIngredient")
            .HasColumnType("integer");

        builder.Property(x => x.TotalPrice)
            .IsRequired()
            .HasColumnName("TotalPrice")
            .HasColumnType("decimal(18, 2)");

        // Índices
        builder
            .HasIndex(x => x.DescriptionCoffe, "IX_Ingredient_Description")
            .IsUnique();
    }
}