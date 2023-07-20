using CoreSharp.AspNetCore.Middlewares;
using CoreSharp.Templates.Blazor.Application.Extensions;
using CoreSharp.Templates.Blazor.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApp.Extensions;
using WebApp.Middlewares;

namespace WebApp;

public class Startup
{
    // Properties
    public IConfiguration Configuration { get; }

    // Constructors 
    public Startup(IConfiguration configuration)
        => Configuration = configuration;

    // Methods 
    /// <summary>
    /// This method gets called by the runtime. Use this method to add services to the container.
    /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    /// </summary>
    public void ConfigureServices(IServiceCollection serviceCollection)
    {
        // User 
        serviceCollection.AddApplication();
        serviceCollection.AddInfrastructure(Configuration);
        serviceCollection.AddAppApi(Configuration);
        serviceCollection.AddAppBlazor();
    }

    /// <summary>
    /// This method gets called by the runtime.
    /// Use this method to configure the HTTP request pipeline.
    /// </summary>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
    {
        // Environment dependent
        if (environment.IsDevelopment())
        {
            var apiVersionProvider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
            app.UseAppSwagger(apiVersionProvider);
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
        }

        // System 
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            // Api 
            endpoints.MapControllers();

            // Blazor 
            endpoints.MapBlazorHub();
            endpoints.MapFallbackToPage("/_Host");
        });

        // User 
        app.UseMiddleware<AppRequestLogMiddleware>();
        app.UseMiddleware<ErrorHandleMiddleware>();
        app.UseAppCors();
    }
}