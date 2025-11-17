using IntervalJob.Models.Interfaces;
using IntervalJob.Models.Services;
using Microsoft.Extensions.Configuration;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
var builder = WebApplication.CreateBuilder(args);
//ocelot configuration created to use ocelot.json file
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

//adding ocelot services to the project
builder.Services.AddOcelot(builder.Configuration).AddCacheManager(settings => settings.WithDictionaryHandle());
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddScoped<IJobTask, JobServices>();     
//builder.Services.AddHostedService<Scheduler>();      
var app = builder.Build();
app.MapGet("/", () => "Hello World!");
await app.UseOcelot();
app.Run();
