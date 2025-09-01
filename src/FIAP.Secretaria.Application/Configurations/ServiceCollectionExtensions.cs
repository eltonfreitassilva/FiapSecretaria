using FIAP.Secretaria.Application.Commands;
using FIAP.Secretaria.Application.Commands.Alunos;
using FIAP.Secretaria.Application.CommandsHandlers;
using FIAP.Secretaria.Shared.Common.Models;
using FIAP.Secretaria.Shared.Common.Results;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FIAP.Secretaria.Application.Configurations;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        services.AddValidatorsFromAssembly(assembly);

        services.AddAutoMapper(config =>
        {
            config.AddMaps(assembly);
        });

        //Commands
        services.AddScoped<IRequestHandler<LoginCommand, Result<LoginResponseModel>>, LoginCommandHandler>();
        services.AddScoped<IRequestHandler<CadastrarAlunoCommand, Result<bool>>, CadastrarAlunoCommandHandler>();
        services.AddScoped<IRequestHandler<EditarAlunoCommand, Result<bool>>, EditarAlunoCommandHandler>();
        services.AddScoped<IRequestHandler<DeletarAlunoCommand, Result<bool>>, DeletarAlunoCommandHandler>();

        //Queries


        return services;
    }
}