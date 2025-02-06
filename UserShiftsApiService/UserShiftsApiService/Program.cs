using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserShiftsApiService.ActionFilters;
using UserShiftsApiService.Middlewares;
using UserShiftsApiService.Models;
using UserShiftsApiService.Services;
using UserShiftsApiService.UserContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ShiftsSchedulingContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IAuth0UserManagementService, Auth0UserManagementService>();
builder.Services.AddScoped<IAddNewUserScheduleRequestService, AddNewUserScheduleRequestService>();
builder.Services.AddScoped<RequireHmacSignatureFilter>();
builder.Services.AddScoped<IUserContextProvider, UserContextProvider>();
builder.Services.AddControllers();

builder.Services.AddTransient<UserContextProviderMiddleware>();

builder.Services.AddOpenApi();

var configuration = builder.Configuration;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["Auth0:Domain"];
    options.Audience = builder.Configuration["Auth0:Audience"];
    options.RequireHttpsMetadata = false;
});

builder.Services.AddAuthorization();

// Restrict the code paths that will run when the apps entry point is being invoked from build-time document generation.
if (Assembly.GetEntryAssembly()?.GetName().Name != "GetDocument.Insider")
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowFront", policy =>
        {
            policy.WithOrigins(configuration["FrontUrl"])
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
    });
}

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

if (Assembly.GetEntryAssembly()?.GetName().Name != "GetDocument.Insider")
{
    app.UseCors("AllowFront");
}


app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.MapOpenApi();

app.Run();
