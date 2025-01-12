using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserShiftsApiService.Models;
using UserShiftsApiService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ShiftsSchedulingContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddSingleton<IGreetingService, GreetingService>();
builder.Services.AddControllers();

builder.Services.AddOpenApi();

var configuration = builder.Configuration;

// Restrict the code paths that will run when the apps entry point is being invoked from build-time document generation.
if (Assembly.GetEntryAssembly()?.GetName().Name != "GetDocument.Insider")
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowFront", policy =>
        {
            policy.WithOrigins(configuration["FrontUrl"]).AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials().
                AllowCredentials();
        });
    });
}

builder.Services.AddSwaggerGen();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

if (Assembly.GetEntryAssembly()?.GetName().Name != "GetDocument.Insider")
{
    app.UseCors("AllowFront");
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.MapOpenApi();

app.Run();
