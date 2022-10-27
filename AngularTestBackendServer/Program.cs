using AngularTestBackendServer;
using AngularTestBackendServer.Core;
using AngularTestBackendServer.Core.Repositories;
using AngularTestBackendServer.Core.Services;
using AngularTestBackendServer.Core.Services.Interfaces;
using AngularTestBackendServer.Data;
using AngularTestBackendServer.Data.Entities.Mappings.Profiles;
using AngularTestBackendServer.Data.Initializers;
using AngularTestBackendServer.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Serilog;
using Serilog.Events;


Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(new WebApplicationOptions
    {
        ApplicationName = typeof(Program).Assembly.FullName,
        ContentRootPath = Directory.GetCurrentDirectory(),
        EnvironmentName = Environments.Development
    });

    builder.Configuration.AddJsonFile("appsettings.json", optional: false)
                         .AddEnvironmentVariables();

    var appSettings = builder.Configuration.Get<AppSettings>();
    
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(LogLevel.Information);
    builder.Host.UseSerilog();

    
    var services = builder.Services;

    services.AddSingleton(appSettings.DataSources);
    
    services
        .AddControllers()
        .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            options.SerializerSettings.Converters.Add(new StringEnumConverter());
        });
    
    services.AddSwaggerGen();
    services.ConfigureOptions<ConfigureSwaggerOptions>();
    
    services.AddApiVersioning(config =>
    {
        config.AssumeDefaultVersionWhenUnspecified = true;
        config.DefaultApiVersion = new ApiVersion(1, 0);
        config.ReportApiVersions = true;
        config.ApiVersionReader = new HeaderApiVersionReader("api-version");
    });
    
    services.AddVersionedApiExplorer(setup =>
    {
        setup.GroupNameFormat = "'v'VVV";
        setup.SubstituteApiVersionInUrl = true;
    });
    
    services.AddDbContext<NflDbContext>(
        options => options.UseSqlServer(appSettings.DataSources.NflConnectionString));
    
    services.AddApplicationInsightsTelemetry();
    services.AddAutoMapper(typeof(NflProfile));
    
    services.AddTransient<ISearchRepository, AzureSearchRepository>();
    services.AddTransient<INflRepository, NflRepository>();
    
    services.AddScoped<INflService, NflService>();
    services.AddScoped<ISearchService, SearchService>();
    
    
    var app = builder.Build();
    
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
    
    app.UseCors(policy =>
    {
        // note insecure but a side project
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.WithExposedHeaders("Content-Disposition");
    });
    
    app.MapControllers();

    if (appSettings.DataSources.ShouldLoadCsvDataForDatabase)
    {
        using var serviceScope = app.Services.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<NflDbContext>();
        await DatabaseInitializer.InitializeAsync(dbContext);
    }

    if (appSettings.DataSources.AzureSearch.ShouldCreateMultipleSeasonsIndexer)
    {
        AzureIndexerInitializer.InitializeNflSeasonIndexers(appSettings.DataSources);
    }
    
    app.Run();  
}
catch (Exception exception)
{
    Log.Fatal(exception, "Stopped program because of exception");
    throw;
}
finally
{
    Log.CloseAndFlush();
}
