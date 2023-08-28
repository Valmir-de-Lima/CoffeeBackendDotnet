using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Coffee.Domain.Models.User;

namespace Coffee.Infra.Mappings;

public class RefreshLoginUserMap : IEntityTypeConfiguration<RefreshLoginUser>
{
    public void Configure(EntityTypeBuilder<RefreshLoginUser> builder)
    {
        // Tabela
        builder.ToTable("RefreshLoginUser");

        // É necessário ignorar a propriedade Notifications para nao ser mapeada
        builder.Ignore(x => x.Notifications);

        // Chave Primária
        builder.HasKey(x => x.Id);

        // Propriedades
        builder.Property(x => x.UserName)
            .IsRequired()
            .HasColumnName("UserName")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);

        builder.Property(x => x.RefreshToken)
            .IsRequired()
            .HasColumnName("RefreshToken")
            .HasColumnType("VARCHAR")
            .HasMaxLength(50);

        // Índices
        builder
            .HasIndex(x => x.UserName, "IX_User_Name")
            .IsUnique();

    }
}