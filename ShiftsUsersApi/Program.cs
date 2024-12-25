using ShiftsUsersApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IOutputDataService, OutputDataService>();
builder.Services.AddScoped<IGreetingService, GreetingService>();
builder.Services.AddControllers();

var configuration = builder.Configuration;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = configuration["Auth0DomainAuthority"];
    options.Audience = configuration["Auth0Audience"];
});

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFront", policy =>
    {
        policy.WithOrigins(configuration["FrontUrl"]).AllowAnyHeader().AllowCredentials();
    });
});

var app = builder.Build();

app.UseCors("AllowFront");
app.UseAuthorization();

app.MapControllers();

app.Run();
