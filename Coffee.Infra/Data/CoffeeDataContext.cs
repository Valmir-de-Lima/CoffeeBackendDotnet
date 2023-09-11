using Microsoft.EntityFrameworkCore;
using Coffee.Domain.Models.User;
using Coffee.Domain.Models.Product.PersonalizedCoffee.Ingredient;
using Coffee.Infra.Mappings.Users;
using Coffee.Infra.Mappings.Products.PersonalizedCoffees;

namespace Coffee.Infra.Data;

public class StoreDataContext : DbContext
{
    public StoreDataContext(DbContextOptions<StoreDataContext> options)
            : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<RefreshLoginUser> RefreshLoginUsers { get; set; } = null!;
    public DbSet<Ingredient> Ingredients { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new RefreshLoginUserMap());
        modelBuilder.ApplyConfiguration(new IngredientMap());
    }
}