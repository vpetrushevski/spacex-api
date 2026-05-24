using SpaceX.Ioc.DependencyInjection;
using SpaceX.WebApi.Extensions;
using SpaceX.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebApiServices();

builder.Services.AddCommonServices(builder.Configuration);

builder.Logging.AddFile("Logs/ErrorLog.txt", LogLevel.Error);
builder.Logging.AddFile("Logs/InformationLog.txt", LogLevel.Information);
builder.Logging.AddFile("Logs/DebugLog.txt", LogLevel.Debug);

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors("AllowAll");

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();