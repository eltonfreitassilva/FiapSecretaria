using FIAP.Secretaria.Shared.Configurations;
using FIAP.Secretaria.Application.Configurations;
using FIAP.Secretaria.Infrastructure.Configurations;
using FIAP.Secretaria.WebApi.Extensions;
using FIAP.Secretaria.WebApi.Configurations;

var builder = WebApplication.CreateBuilder(args);

var environment = builder.Environment;

builder.Services
    .AddShared()
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddWebApiServices()
    .ConfigureSwagger(environment);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwaggerCustomUI(environment);
app.MapControllers();

app.Run();