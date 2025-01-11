using ShiftsUsersApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShiftsUsersApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ShiftsSchedulingContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddSingleton<IOutputDataService, OutputDataService>();
builder.Services.AddSingleton<IGreetingService, GreetingService>();
builder.Services.AddScoped<ISaveNewEmployeeService, SaveNewEmployeeService>();
builder.Services.AddControllers();

builder.Services.AddOpenApi();

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
        policy.WithOrigins(configuration["FrontUrl"]).AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials().
            AllowCredentials();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowFront");
app.UseSwagger();

app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();
app.MapOpenApi();


app.Run();
