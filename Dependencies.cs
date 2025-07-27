using FluentValidation;
using Mapster;
using MapsterMapper;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Task.Api.Services;

namespace Task.Api;

public static class Dependencies
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {

        services.AddFluentValidationConfig();
        services.AddMapsterConfig();


        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IBookService, BookService>();

        return services;
    }


    public static IServiceCollection AddMapsterConfig(this IServiceCollection services)
    {


        var mappingConfig = TypeAdapterConfig.GlobalSettings;

        mappingConfig.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton<IMapper>(new Mapper(mappingConfig));
        return services;
    }

    private static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
    {

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
            .AddFluentValidationAutoValidation();

        return services;
    }
}
