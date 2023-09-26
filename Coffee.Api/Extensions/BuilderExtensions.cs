using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Coffee.Api.Services;

using Coffee.Domain.Handlers.UserHandlers;
using Coffee.Domain.Handlers.BasketHandlers;
using Coffee.Domain.Handlers.ProductHandlers;
using Coffee.Domain.Handlers.ProductHandlers.PastryHandlers;
using Coffee.Domain.Handlers.ProductHandlers.PersonalizedCoffeeHandlers.IngredientHandlers;
using Coffee.Domain.Handlers.ProductHandlers.PersonalizedCoffeeHandlers.CoffeHandlers;

using Coffee.Domain.Repositories.Interfaces;
using Coffee.Domain.Services;
using Coffee.Infra.Data;
using Coffee.Infra.Repositories.UsersRepository;
using Coffee.Infra.Repositories.BasketsRepository;
using Coffee.Infra.Repositories.ProductsRepository;
using Coffee.Infra.Repositories.ProductsRepository.PastryRepository;
using Coffee.Infra.Repositories.ProductsRepository.PersonalizedCoffeesRepository;
using Coffee.Infra.Repositories.ProductsRepository.PersonalizedCoffeesRepository.IngredientRepository;
using Coffee.Infra.Repositories.ProductsRepository.PersonalizedCoffeesRepository.CoffeRepository;
using Coffee.Domain.Handlers.ProductHandlers.PersonalizedCoffeeHandlers;

namespace Coffee.Api.Extensions;

public static class BuilderExtensions
{
    public static void LoadConfiguration(this WebApplicationBuilder builder)
    {
        Configuration.JwtKey = builder.Configuration.GetValue<string>("JwtKey")!;
        var smtp = new Configuration.SmtpConfiguration();
        builder.Configuration.GetSection("Smtp").Bind(smtp);
        Configuration.Smtp = smtp;
    }

    public static void ConfigureAuthentication(this WebApplicationBuilder builder)
    {
        var key = Encoding.UTF8.GetBytes(Configuration.JwtKey);
        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
    }

    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

        builder.Services.AddDbContext<CoffeeDataContext>(opt => opt.UseInMemoryDatabase(connectionString));

        builder.Services.AddTransient<IUserRepository, UserRepository>();
        builder.Services.AddTransient<IRefreshLoginUserRepository, RefreshLoginUserRepository>();
        builder.Services.AddTransient<IIngredientRepository, IngredientRepository>();
        builder.Services.AddTransient<IPastryRepository, PastryRepository>();
        builder.Services.AddTransient<ICoffeRepository, CoffeRepository>();
        builder.Services.AddTransient<IPersonalizedCoffeeRepository, PersonalizedCoffeeRepository>();
        builder.Services.AddTransient<IProductRepository, ProductRepository>();
        builder.Services.AddTransient<IBasketRepository, BasketRepository>();

        builder.Services.AddTransient<UserHandler, UserHandler>();
        builder.Services.AddTransient<PastryHandler, PastryHandler>();
        builder.Services.AddTransient<IngredientHandler, IngredientHandler>();
        builder.Services.AddTransient<CoffeHandler, CoffeHandler>();
        builder.Services.AddTransient<PersonalizedCoffeeHandler, PersonalizedCoffeeHandler>();
        builder.Services.AddTransient<ProductHandler, ProductHandler>();
        builder.Services.AddTransient<BasketHandler, BasketHandler>();

        builder.Services.AddTransient<ITokenService, TokenService>();
        builder.Services.AddTransient<IEmailService, EmailService>();
    }

    public static void ConfigureOthers(this WebApplicationBuilder builder)
    {
        builder
            .Services
            .AddControllers()
            .ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; })
            .AddJsonOptions(x =>
            {
                // Para evitar que na serializacao do objeto gere um circulo infinito
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

                // Nao serializa objetos que sao nulos, ou que possuem o valor zero, 
                // ou seja, objetos nulos nao sao renderizados na tela
                //x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
            });
    }
}