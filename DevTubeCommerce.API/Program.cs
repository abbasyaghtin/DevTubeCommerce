using DevTubeCommerce.API.Configurations;
using DevTubeCommerce.Application;
using DevTubeCommerce.Framework.Extensions;
using DevTubeCommerce.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMapperSetup();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDatabaseSetup(builder.Configuration);
builder.Services.AddInfrastructureRepositories();
builder.Services.AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.ConfigureExceptionHandler(!app.Environment.IsProduction());



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
