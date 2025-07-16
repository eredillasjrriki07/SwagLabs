
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;

namespace SwagLabs.Api.ApiVersionSupport;

public static class ApiVersionConfiguration
{
    public static IServiceCollection AddSwagLabsApiVersionConfiguration(this IServiceCollection services, ApiVersion defaultVersion = null)
    {
        defaultVersion ??= ApiVersion.Default;
        services.AddApiVersioning(options =>
            {
                // Set default version
                options.DefaultApiVersion = defaultVersion;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.UseApiBehavior = true;
                // reporting api versions will return the headers "api-supported-versions"
                // and "api-deprecated-versions"
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new QueryStringApiVersionReader(), //defaults to "api-version"
                    new QueryStringApiVersionReader("v"),
                    new HeaderApiVersionReader("api-version"),
                    new HeaderApiVersionReader("v"),
                    new MediaTypeApiVersionReader(), //defaults to "v"
                    new MediaTypeApiVersionReader("api-version")

                );
            }
        );

        services.AddVersionedApiExplorer(options =>
            {
                options.DefaultApiVersion = defaultVersion;
                options.AssumeDefaultVersionWhenUnspecified = true;
                // note: the specified format code will format the version as "'v'major[.minor][-status]"
                options.GroupNameFormat = "'v'VVV";
                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
                options.SubstituteApiVersionInUrl = true;
            }
        );

        return services;
    }
}
