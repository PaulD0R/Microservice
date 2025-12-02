using UserService.API.Extensions;
using UserService.Application.Models.Person;
using UserService.Domain.Events;
using UserService.Infrastructure.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataBasses(builder.Configuration);
builder.Services.AddExceptionsHandlers();
builder.Services.AddPersonAuthentication();
builder.Services.AddCaching();
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddFactories();
builder.Services.AddProducer<PersonDto>(builder.Configuration.GetRequiredSection("Kafka:PersonCreated"));
builder.Services.AddConsumer<LikeEvent, LikeHandler>(builder.Configuration.GetRequiredSection("Kafka:LikeEvent"));
builder.Services.AddPersonCors();
builder.Services.AddResponseCompression();


var app = builder.Build();

app.UseStatusCodePages(async statusCodeContext =>
{
    var response = statusCodeContext.HttpContext.Response;
    response.ContentType = "text/plain, charset UTF-8";

    await response.WriteAsync(response.StatusCode switch
    {
        404 => "Not Found",
        400 => "400",
        401 => "401",
        403 => "403",
        _ => "Fail"
    });
});

app.UseExceptionHandler();
app.UseResponseCompression();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); 

app.Run();