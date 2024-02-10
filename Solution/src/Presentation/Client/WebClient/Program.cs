using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebClient;
using WebClient.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.ConfigureLogging();
builder.ConfigureServices();

var app = builder.Build();
await app.RunAsync();

#if DEBUG
await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(5));
#endif
