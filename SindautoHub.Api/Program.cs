using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SindautoHub.Application;
using SindautoHub.Application.Interface;
using SindautoHub.Application.Service;
using SindautoHub.Domain.Interface;
using SindautoHub.Domain.Interfaces;

using SindautoHub.Infrastructure.Persistance.Database;
using SindautoHub.Infrastructure.Persistance.Repository;
using SindautoHub.Infrastructure.Persistence.Repository;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// --- Adicionar serviços ao container ---

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins(
            "http://localhost:5173",
            "https://sind-hub.vercel.app",
            "https://sindautohubbackend.onrender.com"
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});


// Configuração do Banco de Dados
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SindautoHubContext>(options =>
    options.UseNpgsql(connectionString)
);

// Configuração do AutoMapper (lê todos os Profiles do projeto Application)
builder.Services.AddAutoMapper(typeof(AssemblyReference).Assembly);

// Configuração do FluentValidation (lê todos os Validators do projeto Application)
builder.Services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);


// --- REGISTRO DAS SUAS INTERFACES E CLASSES ---
builder.Services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
builder.Services.AddScoped<IunitOfwork, UnitOfWork>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPositionRepository, PositionRepository>();
builder.Services.AddScoped<ISectorRepository, SectorRepository>();
builder.Services.AddScoped<IAnnouncementsRepository, AnnouncementsRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserServices, UserService>();
builder.Services.AddScoped<IPositionServices, PositionServices>();builder.Services.AddScoped<ISectorService, SectorService>();
builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();
// Em Api/Program.cs

// Configuração do Cache Distribuído (Redis)
builder.Services.AddStackExchangeRedisCache(options =>
{
    // A Connection String do seu serviço de Redis (pode vir do appsettings ou Render)
    options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
    options.InstanceName = "SindautoHub_";
});

builder.Services.AddScoped<ICacheService, RedisCacheService>();

// --- CONFIGURAÇÃO DA AUTENTICAÇÃO JWT ---
var jwtKey = builder.Configuration["Jwt:Key"];
if (string.IsNullOrWhiteSpace(jwtKey))
    throw new InvalidOperationException("Chave JWT (Jwt:Key) não configurada.");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ClockSkew = TimeSpan.Zero
        };
    });
// ======================================================

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SindautoHub API V1");
    c.RoutePrefix = ""; // <- Deixa o Swagger acessível na raiz "/"
});
app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();