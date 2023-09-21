using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Coffee.Domain.Models.Product.PersonalizedCoffee.IngredientsSelected;

namespace Coffee.Infra.Mappings.Products.PersonalizedCoffees.IngredientsSelected;

public class IngredientSelectedMap : IEntityTypeConfiguration<IngredientSelected>
{
    public void Configure(EntityTypeBuilder<IngredientSelected> builder)
    {
        // Tabela
        builder.ToTable("IngredientSelected");

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
            .HasColumnName("Price")
            .HasColumnType("decimal(18, 2)");

        // Relacionamento um para muitos
        builder
            .HasOne(x => x.PersonalizedCoffee)
                .WithMany(x => x.Ingredients)
            .HasConstraintName("FK_PersonalizedCoffee_IngredientSelected")
            .OnDelete(DeleteBehavior.Cascade);

        // Índices
        builder
            .HasIndex(x => x.Description, "IX_Ingredient_Description")
            .IsUnique();
    }
}