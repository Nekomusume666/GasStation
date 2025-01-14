using GasStation.Application.Interfaces;
using GasStation.Application.Services;
using GasStation.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GasStation.API.Hubs;
using Microsoft.AspNetCore.Http.Connections;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:5000");

// Добавление логирования
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.WithOrigins(new[] { "http://:192.168.0.101:5000", "https://192.168.0.101:5001" })
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials()
               .SetIsOriginAllowed((host) => true); //for signalr cors  
    });
});

// Добавляем SignalR
builder.Services.AddSignalR(hubOptions =>
{
    hubOptions.EnableDetailedErrors = true;
    hubOptions.KeepAliveInterval = TimeSpan.FromMinutes(1);
});

// Настройка базы данных
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Регистрация сервисов
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IAdministratorService, AdministratorService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IFuelService, FuelService>();
builder.Services.AddScoped<IFuelTypeService, FuelTypeService>();
builder.Services.AddScoped<IGasStationService, GasStationService>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<IPumpService, PumpService>();
builder.Services.AddScoped<ISupplyService, SupplyService>();
builder.Services.AddScoped<ITransactionsService, TransactionsService>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Добавление контроллеров
builder.Services.AddControllers();

//// JWT Authentication configuration
//var jwtKey = builder.Configuration["Jwt:Key"];
//var jwtIssuer = builder.Configuration["Jwt:Issuer"];
//var jwtAudience = builder.Configuration["Jwt:Audience"];

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = jwtIssuer,
//            ValidAudience = jwtAudience,
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
//        };
//    });

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
//    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
//});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("AllowAll");

// Map SignalR hub
app.MapHub<ClientHub>("/clienthub");

//app.UseHttpsRedirection();
//app.UseAuthentication();
//app.UseAuthorization();


app.MapControllers();

app.Run();