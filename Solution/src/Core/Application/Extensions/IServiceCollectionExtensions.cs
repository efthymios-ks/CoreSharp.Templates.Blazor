using AutoMapper;
using AutoMapper.Internal;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CoreSharp.Templates.Blazor.Application.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        ArgumentNullException.ThrowIfNull(serviceCollection);

        serviceCollection.AddAppAutoMapper();
        serviceCollection.AddAppMediatR();

        return serviceCollection;
    }

    private static IServiceCollection AddAppAutoMapper(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddAutoMapper(MapperConfig, Assembly.GetExecutingAssembly());

        static void MapperConfig(IMapperConfigurationExpression mapper)
             => mapper.Internal()
                      .ForAllMaps(MappingConfig);

        static void MappingConfig(TypeMap _, IMappingExpression mapping)
             => mapping.PreserveReferences();
    }

    private static IServiceCollection AddAppMediatR(this IServiceCollection serviceCollection)
    {
        var assembly = Assembly.GetExecutingAssembly();
        serviceCollection.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
        serviceCollection.AddValidatorsFromAssembly(assembly);

        foreach (var pipelineBehaviourType in GetPipelineBehavioursTypes(assembly))
        {
            serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), pipelineBehaviourType);
        }

        return serviceCollection;

        static IEnumerable<Type> GetPipelineBehavioursTypes(Assembly assembly)
            => assembly
                  .GetTypes()
                  .Where(type =>
                  {
                      if (!type.IsClass || type.IsAbstract)
                      {
                          return false;
                      }

                      return type
                        .GetInterfaces()
                        .Any(interfaceType =>
                            interfaceType.IsGenericType
                                && interfaceType.GetGenericTypeDefinition() == typeof(IPipelineBehavior<,>));
                  });
    }
}
