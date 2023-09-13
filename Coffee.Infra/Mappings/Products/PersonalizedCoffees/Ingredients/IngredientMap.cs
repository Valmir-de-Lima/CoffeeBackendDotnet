using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Coffee.Domain.Models.Product.PersonalizedCoffee.Ingredient;

namespace Coffee.Infra.Mappings.Products.PersonalizedCoffees.Ingredients;

public class IngredientMap : IEntityTypeConfiguration<Ingredient>
{
    public void Configure(EntityTypeBuilder<Ingredient> builder)
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