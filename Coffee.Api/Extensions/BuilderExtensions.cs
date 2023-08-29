using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Coffee.Api.Services;
using Coffee.Domain.Handlers.UserHandlers;
using Coffee.Domain.Repositories.Interfaces;
using Coffee.Domain.Services;
using Coffee.Infra.Data;
using Coffee.Infra.Repositories.UsersRepository;

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

        builder.Services.AddDbContext<StoreDataContext>(opt => opt.UseInMemoryDatabase(connectionString));
        builder.Services.AddTransient<IUserRepository, UserRepository>();
        builder.Services.AddTransient<IRefreshLoginUserRepository, RefreshLoginUserRepository>();
        builder.Services.AddTransient<UserHandler, UserHandler>();
        builder.Services.AddTransient<ITokenService, TokenService>();
        builder.Services.AddTransient<IEmailService, EmailService>();
    }
}