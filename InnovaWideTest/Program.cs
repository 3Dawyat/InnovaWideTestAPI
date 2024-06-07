using InnovaWideTest.API;
using InnovaWideTest.API.Middlewares;
using InnovaWideTest.Application;
using InnovaWideTest.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddInfrastructureServices()
    .AddApplicationServices()
    .AddWebServices(builder.Configuration);


Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Host.UseSerilog();
var app = builder.Build();

app.UseSwagger();
if (app.Environment.IsDevelopment())
    app.UseSwaggerUI();
else
{
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
    app.UseMiddleware<ExceptionHandlingMiddleware>();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseCors(a => a.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
