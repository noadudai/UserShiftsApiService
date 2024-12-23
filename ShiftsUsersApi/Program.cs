using ShiftsUsersApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IGreetingService, GreetingService>();
builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
