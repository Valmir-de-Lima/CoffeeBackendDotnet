using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Coffee.Domain.Models.Product.PersonalizedCoffee;
using Coffee.Domain.Models.Product.PersonalizedCoffee.Ingredients;


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

        // Relacionamento muitos para muitos
        builder
            .HasMany(x => x.Ingredients)       // Tem muitos
                .WithMany(x => x.PersonalizedCoffees) // Com muitos. Esta instrucao se refere a Tag, o tipo de x é de Tag.
                                                      // Criando uma entidade virtual para ser uma tabela associativa
            .UsingEntity<Dictionary<string, object>>(
                "PersonalizedCoffeeIngredient",
                personalizedCoffee => personalizedCoffee
                    .HasOne<Ingredient>()
                    .WithMany()
                    .HasForeignKey("PersonalizedCoffeeId")
                    .HasConstraintName("FK_PersonalizedCoffeeIngredient_PersonalizedCoffeeId")
                    .OnDelete(DeleteBehavior.NoAction),
                ingredient => ingredient
                    .HasOne<PersonalizedCoffee>()
                    .WithMany()
                    .HasForeignKey("IngredientId")
                    .HasConstraintName("FK_PersonalizedCoffeeIngredient_IngredientId")
                    .OnDelete(DeleteBehavior.NoAction));

        // Índices
        builder
            .HasIndex(x => x.DescriptionCoffe, "IX_Ingredient_Description")
            .IsUnique();
    }
}