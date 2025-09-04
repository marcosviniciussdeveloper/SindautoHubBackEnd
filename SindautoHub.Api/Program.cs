using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SindautoHub.Application;
using SindautoHub.Application.Interface;
using SindautoHub.Application.Service;
using SindautoHub.Domain.Interface;

using SindautoHub.Infrastructure.Persistance.Database;
using SindautoHub.Infrastructure.Persistance.Repository;


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
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
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
builder.Services.AddScoped<IunitOfwork, UnitOfwork>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IFuncionarioRespository, FuncionarioRepository>();
builder.Services.AddScoped<ICargoRepository, CargosRepository>();
builder.Services.AddScoped<ISetoresRepository, SetorRepository>();
builder.Services.AddScoped<PostagemRepository, PostagemRepository>();
builder.Services.AddScoped<INotificacaoRepository, NotificacaoRepository>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IFuncionarioServices, FuncionarioService>();
builder.Services.AddScoped<ICargoServices, CargoServices>();
builder.Services.AddScoped<ISetorService, SetorService>();
builder.Services.AddScoped<IPostagemService, PostagemService>();
builder.Services.AddScoped<INotificacaoServices, NotificacaoService>();

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();