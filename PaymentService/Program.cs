using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
// Injected DbContext to connect to SQL Server
builder.Services.AddDbContext<PaymentServiceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PaymentServiceContext") ?? throw new InvalidOperationException("Connection string 'PaymentServiceContext' not found.")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddAuthorization();
// Swagger configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Payment Service", Version = "v1" });
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