using HotelService.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDb(builder.Configuration);

builder.Services.AddDomainServices();
builder.Services.AddRepositories();
builder.Services.AddCacheServices();
builder.Services.AddHelpers();
builder.Services.AddServices();
builder.Services.AddFactories();
builder.Services.AddConsumers(builder.Configuration);

builder.Services.AddResponseCompression();
builder.Services.AddExceptionHandlers();
builder.Services.AddPersonAuthentication();
builder.Services.AddPersonCors();

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