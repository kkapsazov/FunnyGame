using System.Text.Json.Serialization;
using FunnyGame.Api.Utils;
using FunnyGame.Application.Services;
using FunnyGame.Data;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GameDbContext>(options => options.UseInMemoryDatabase("database"));
builder.Services.AddScoped<IRandomizerService, RandomizerService>();
builder.Services.AddScoped<GameService>();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using IServiceScope scope = app.Services.CreateScope();
GameDbContext context = scope.ServiceProvider.GetRequiredService<GameDbContext>();
context.Database.EnsureCreated();

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run("http://*:80");
