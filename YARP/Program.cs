var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
    
    logger.LogInformation("Request: {Method} {Path}", 
        context.Request.Method, context.Request.Path);
    
    var originalResponseBody = context.Response.Body;
    using var responseBody = new MemoryStream();
    context.Response.Body = responseBody;
    
    await next();
    
    responseBody.Seek(0, SeekOrigin.Begin);
    await responseBody.CopyToAsync(originalResponseBody);
    
    logger.LogInformation("Response: {StatusCode}", context.Response.StatusCode);
});

app.MapReverseProxy();
app.Run();
