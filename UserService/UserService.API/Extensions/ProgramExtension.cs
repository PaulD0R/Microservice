using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using UserService.API.Exceptions;
using UserService.Application.Interfaces.Cachings;
using UserService.Application.Interfaces.Factory;
using UserService.Application.Interfaces.Messages;
using UserService.Application.Interfaces.Repositories;
using UserService.Application.Interfaces.Services;
using UserService.Application.Services;
using UserService.Domain.Data;
using UserService.Domain.Entities;
using UserService.Infrastructure.Caching;
using UserService.Infrastructure.Core;
using UserService.Infrastructure.Factory;
using UserService.Infrastructure.Kafka;
using UserService.Infrastructure.Repositories;
using AuthenticationService = UserService.Application.Services.AuthenticationService;

namespace UserService.API.Extensions;

public static class ProgramExtension
{
    extension(IServiceCollection service)
    {
        public void AddFactories()
        {
            service.AddTransient<ILikeServiceFactory, LikeServiceFactory>();
        }
        
        public void AddProducer<TMessage>(IConfigurationSection configuration)
        {
            service.Configure<KafkaSettings>(configuration);
            service.AddSingleton<IMessageProducer<TMessage>, KafkaMessageProducer<TMessage>>();
        }

        public void AddConsumer<TMessage, THandler>
            (IConfigurationSection configuration)
            where THandler : class, IMessageHandler<TMessage>
        {
            service.Configure<KafkaSettings>(configuration);
            service.AddHostedService<KafkaConsumer<TMessage>>();
            service.AddSingleton<IMessageHandler<TMessage>, THandler>();
        }

        public void AddDataBasses(IConfiguration configuration)
        {   
            service.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Psql")));
            service.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
                options.InstanceName = "micro_user";
            });
            service.AddScoped<IDatabase>(options =>
                options.GetRequiredService<IConnectionMultiplexer>().GetDatabase());
        }

        public void AddPersonAuthentication()
        {
            service.AddIdentity<Person, IdentityRole>( options =>
            {   
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
            }).AddEntityFrameworkStores<ApplicationDbContext>();
            service.AddAuthentication( options =>
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
                    ValidIssuer = StaticData.ISSUER,
                    ValidateAudience = true,
                    ValidAudience = StaticData.AUDIENCE,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(StaticData.KEY)),
                    ValidateIssuerSigningKey = true
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["jwt"];
                        return Task.CompletedTask;
                    }
                };
            });
            service.AddAuthorization().AddControllers();
        }

        public void AddRepositories()
        {
            service.AddScoped<IPersonRepository, PersonRepository>();
            service.AddScoped<ILikeRepository, LikeRepository>();
            service.AddScoped<IJwtRepository, JwtRepository>();
            service.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        }

        public void AddCaching()
        {
            service.AddScoped<IHashCachingService, HashCachingService>();
        }

        public void AddServices()
        {
            service.AddScoped<ILikeService, LikeService>();
            service.AddScoped<IAuthenticationService, AuthenticationService>();
            service.AddScoped<IPersonService, PersonService>();
            service.AddScoped<ITokenService, TokenService>();
        }

        public void AddExceptionsHandlers()
        {
            service.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            service.AddProblemDetails();
            service.AddExceptionHandler<ExceptionHandler>();
        }

        public void AddPersonCors()
        {
            service.AddCors(options =>
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

        public void AddHelthCheck()
        {
            //services.AddHealthChecks().AddInMemoryStorage();
        }
    }
}