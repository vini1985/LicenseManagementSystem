using IntervalJob.Models.Interfaces;
using IntervalJob.Models.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IJobTask, JobServices>();
builder.Services.AddHostedService<Scheduler>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
