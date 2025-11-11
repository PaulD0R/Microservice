using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using UserService.API.Exceptions;
using UserService.Application.Interfaces.Cachings;
using UserService.Application.Interfaces.Messages;
using UserService.Application.Interfaces.Repositories;
using UserService.Application.Interfaces.Services;
using UserService.Application.Services;
using UserService.Domain.Data;
using UserService.Domain.Entities;
using UserService.Infrastructure.Core;
using UserService.Infrastructure.Kafka;
using UserService.Infrastructure.Repositories;
using AuthenticationService = UserService.Application.Services.AuthenticationService;

namespace UserService.API.Extensions;

public static class ProgramExtension
{
    public static void AddProducer<TMessage>(this IServiceCollection service, IConfigurationSection configuration)
    {
        service.Configure<KafkaSettings>(configuration);
        service.AddSingleton<IMessageProducer<TMessage>, KafkaMessageProducer<TMessage>>();
    }

    public static void AddConsumer<TMessage, THandler>
        (this IServiceCollection service, IConfigurationSection configuration)
    where THandler : class, IMessageHandler<TMessage>
    {
        service.Configure<KafkaSettings>(configuration);
        service.AddHostedService<KafkaConsumer<TMessage>>();
        service.AddScoped<IMessageHandler<TMessage>, THandler>();
    }

    public static void AddDataBasses(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Psql")));
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
            options.InstanceName = "micro_user";
        });
        services.AddScoped<IDatabase>(options =>
            options.GetRequiredService<IConnectionMultiplexer>().GetDatabase());
    }

    public static void AddAuthentication(this IServiceCollection services)
    {
        services.AddIdentity<Person, IdentityRole>( options =>
        {   
            options.Password.RequireDigit = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 8;
        }).AddEntityFrameworkStores<ApplicationDbContext>();
        services.AddAuthentication( options =>
        {
            options.DefaultAuthenticateScheme =
                options.DefaultChallengeScheme =
                    options.DefaultForbidScheme =
                        options.DefaultScheme =
                            options.DefaultSignInScheme =
                                options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = StaticData.ISSURE,
                ValidateAudience = true,
                ValidAudience = StaticData.AUDIENCE,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(StaticData.KEY)),
                ValidateIssuerSigningKey = true
            };
        });
        services.AddAuthorization().AddControllers();
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<ILikeRepository, LikeRepository>();
        services.AddScoped<IJwtRepository, JwtRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
    }

    public static void AddCaching(this IServiceCollection services)
    {
        services.AddScoped<IHashCachingService, IHashCachingService>();
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IPersonService, PersonService>();
        services.AddScoped<ITokenService, TokenService>();
    }

    public static void AddExceptionsHandlers(this IServiceCollection services)
    {
        services.AddControllers().ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
        services.AddProblemDetails();
        services.AddExceptionHandler<ExceptionHandler>();
    }

    public static void AddPersonCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("Authorization");
            });
        });
    }
}