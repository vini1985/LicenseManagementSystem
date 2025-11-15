using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
// Injected DbContext to connect to SQL Server
builder.Services.AddDbContext<DocumentServiceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DocumentServiceContext") ?? throw new InvalidOperationException("Connection string 'DocumentServiceContext' not found.")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddAuthorization();
// Swagger configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Document Service", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();

app.MapControllers();


app.Run();