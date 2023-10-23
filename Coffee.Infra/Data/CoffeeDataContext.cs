using Microsoft.EntityFrameworkCore;
using Coffee.Domain.Models.User;
using Coffee.Domain.Models.Payments;
using Coffee.Domain.Models.Orders;
using Coffee.Domain.Models.Orders.Items;
using Coffee.Domain.Models.Orders.Items.Ingredients;
using Coffee.Domain.Models.Baskets;
using Coffee.Domain.Models.Product;
using Coffee.Domain.Models.Product.Pastry;
using Coffee.Domain.Models.Product.PersonalizedCoffee;
using Coffee.Domain.Models.Product.PersonalizedCoffee.Ingredients;
using Coffee.Domain.Models.Product.PersonalizedCoffee.Coffe;
using Coffee.Infra.Mappings.Users;
using Coffee.Infra.Mappings.Payments;
using Coffee.Infra.Mappings.Orders;
using Coffee.Infra.Mappings.Items;
using Coffee.Infra.Mappings.Items.Ingredients;
using Coffee.Infra.Mappings.Baskets;
using Coffee.Infra.Mappings.Products;
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
    public DbSet<Domain.Models.Product.PersonalizedCoffee.Ingredients.Ingredient> Ingredients { get; set; } = null!;
    public DbSet<Pastry> Pastrys { get; set; } = null!;
    public DbSet<Coffe> Coffes { get; set; } = null!;
    public DbSet<PersonalizedCoffee> PersonalizedCoffees { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Basket> Baskets { get; set; } = null!;
    public DbSet<Payment> Payments { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Item> Items { get; set; } = null!;
    public DbSet<Domain.Models.Orders.Items.Ingredients.Ingredient> ItemIngredientes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new RefreshLoginUserMap());
        modelBuilder.ApplyConfiguration(new IngredientMap());
        modelBuilder.ApplyConfiguration(new PastryMap());
        modelBuilder.ApplyConfiguration(new CoffeMap());
        modelBuilder.ApplyConfiguration(new PersonalizedCoffeeMap());
        modelBuilder.ApplyConfiguration(new ProductMap());
        modelBuilder.ApplyConfiguration(new BasketMap());
        modelBuilder.ApplyConfiguration(new PaymentMap());
        modelBuilder.ApplyConfiguration(new OrderMap());
        modelBuilder.ApplyConfiguration(new ItemMap());
        modelBuilder.ApplyConfiguration(new ItemIngredientMap());
    }
}