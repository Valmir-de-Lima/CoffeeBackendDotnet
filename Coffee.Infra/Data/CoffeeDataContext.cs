using Microsoft.EntityFrameworkCore;
using Coffee.Domain.Models.User;
using Coffee.Domain.Models.Product.Pastry;
using Coffee.Domain.Models.Product.PersonalizedCoffee;
using Coffee.Domain.Models.Product.PersonalizedCoffee.Ingredients;
using Coffee.Domain.Models.Product.PersonalizedCoffee.Coffe;
using Coffee.Infra.Mappings.Users;
using Coffee.Infra.Mappings.Products.Pastrys;
using Coffee.Infra.Mappings.Products.PersonalizedCoffees;
using Coffee.Infra.Mappings.Products.PersonalizedCoffees.Ingredients;
using Coffee.Infra.Mappings.Products.PersonalizedCoffees.Coffes;

namespace Coffee.Infra.Data;

public class CoffeeDataContext : DbContext
{
    public CoffeeDataContext(DbContextOptions<CoffeeDataContext> options)
            : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<RefreshLoginUser> RefreshLoginUsers { get; set; } = null!;
    public DbSet<Ingredient> Ingredients { get; set; } = null!;
    public DbSet<Pastry> Pastrys { get; set; } = null!;
    public DbSet<Coffe> Coffes { get; set; } = null!;
    public DbSet<PersonalizedCoffee> PersonalizedCoffees { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new RefreshLoginUserMap());
        modelBuilder.ApplyConfiguration(new IngredientMap());
        modelBuilder.ApplyConfiguration(new PastryMap());
        modelBuilder.ApplyConfiguration(new CoffeMap());
        modelBuilder.ApplyConfiguration(new PersonalizedCoffeeMap());
    }
}