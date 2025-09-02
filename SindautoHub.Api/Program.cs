
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using SindautoHub.Application;
using SindautoHub.Infrastructure.Persistance.Database;


var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// --- Adicionar servi�os ao container ---

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura��o do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("*"); // Em produ��o, mudar para a URL do  front-end
                      });
});

// Configura��o do Banco de Dados
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SindautoHubContext>(options =>
    options.UseNpgsql(connectionString)
);

// Configura��o do FluentValidation 
builder.Services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);


// --- Fim da configura��o dos servi�os ---

var app = builder.Build();

// --- Configurar o pipeline de requisi��es HTTP ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwagger();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();