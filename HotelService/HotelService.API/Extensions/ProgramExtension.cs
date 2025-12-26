using System.Text;
using Amazon.S3;
using HotelService.API.Exceptions;
using HotelService.Application.Interfaces.Caches;
using HotelService.Application.Interfaces.Factories;
using HotelService.Application.Interfaces.Helpers;
using HotelService.Application.Interfaces.Messages;
using HotelService.Application.Interfaces.Repositories;
using HotelService.Application.Interfaces.Services;
using HotelService.Application.Services;
using HotelService.Domain.Data;
using HotelService.Domain.Events;
using HotelService.Domain.Interfaces.Services;
using HotelService.Domain.Services;
using HotelService.Infrastructure.Caches;
using HotelService.Infrastructure.Core;
using HotelService.Infrastructure.Factories;
using HotelService.Infrastructure.Handlers;
using HotelService.Infrastructure.Helpers;
using HotelService.Infrastructure.Kafka;
using HotelService.Infrastructure.Options;
using HotelService.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace HotelService.API.Extensions;

public static class ProgramExtension
{
    extension(IServiceCollection service)
    {
        public void AddDb(IConfiguration configuration)
        {
            service.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Postgres")));

            service.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
                options.InstanceName = "hotel_service";
            });
            
            service.AddScoped<IAmazonS3>(_ =>
            {
                var config = new AmazonS3Config
                {
                    ServiceURL = configuration["AWS:ServiceURL"],
                    ForcePathStyle = true 
                };
    
                var credentials = new Amazon.Runtime.BasicAWSCredentials(
                    configuration["AWS:AccessKey"], 
                    configuration["AWS:SecretKey"]
                );
    
                return new AmazonS3Client(credentials, config);
            });
        }

        private void AddConsumer<TMessage, THandler>(IConfigurationSection configuration)
        where THandler : class, IMessageHandler<TMessage>
        {
            service.Configure<KafkaOptions>(configuration);
            service.AddHostedService<KafkaConsumer<TMessage>>();
            service.AddSingleton<IMessageHandler<TMessage>, THandler>();
        }

        public void AddConsumers(IConfiguration configuration)
        {
            service.AddConsumer<PersonCreateEvent, PersonCreateEventHandlers>
                (configuration.GetSection("Kafka:PersonCreated"));
            // service.AddConsumer<PersonUpdateEvent, PersonUpdateEventHandlers>
            //     (configuration.GetSection("Kafka:PersonUpdated"));
            // service.AddConsumer<PersonDeleteEvent, PersonDeleteEventHandlers>
            //     (configuration.GetSection("Kafka:PersonDeleted"));
        }

        public void AddExceptionHandlers()
        {
            service.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            service.AddProblemDetails();
            service.AddExceptionHandler<ExceptionHandler>();
        }

        public void AddPersonAuthentication()
        {
            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
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

            service.AddAuthorization();
        }

        public void AddPersonCors()
        {
            service.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod()
                        .AllowAnyOrigin().WithExposedHeaders("Authorization");
                });
            });
        }

        public void AddDomainServices()
        {
            service.AddSingleton<IRatingCalculateService, AmazonRatingCalculateService>();
        }
        
        public void AddRepositories()
        {
            service.AddScoped<ICloudRepository, CloudRepository>();
            service.AddScoped<IHotelCommentRepository, HotelCommentRepository>();
            service.AddScoped<IHotelRepository, HotelRepository>();
            service.AddScoped<IHotelRoomRepository, HotelRoomRepository>();
            service.AddScoped<IHotelPhotoRepository, HotelPhotoRepository>();
            service.AddScoped<IHotelRatingRepository, HotelRatingRepository>();
            service.AddScoped<IPersonRepository, PersonRepository>();
            service.AddScoped<IRoomPhotoRepository, RoomPhotoRepository>();
            service.AddScoped<IRoomStateRepository, RoomStateRepository>();
        }

        public void AddHelpers()
        {
            service.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public void AddCacheServices()
        {
            service.AddScoped<ICacheService, RedisCacheService>();
        }
        
        public void AddServices()
        {
            service.AddScoped<IPersonService, PersonService>();
            service.AddScoped<IHotelPhotoService, HotelPhotoService>();
            service.AddScoped<IHotelCommentService, HotelCommentService>();
            service.AddScoped<IHotelRatingService, HotelRatingService>();
            service.AddScoped<IRoomPhotoService, RoomPhotoService>();
            service.AddScoped<IRoomStateService, RoomStateService>();
            service.AddScoped<IHotelRoomService, HotelRoomService>();
            service.AddScoped<IHotelService, Application.Services.HotelService>();
        }

        public void AddFactories()
        {
            service.AddTransient<IPersonServiceFactory, PersonServiceFactory>();
        }
    }
}