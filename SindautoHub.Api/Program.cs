
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using SindautoHub.Application;
using SindautoHub.Infrastructure.Persistance.Database;


var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// --- Adicionar serviços ao container ---

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("*"); // Em produção, mudar para a URL do  front-end
                      });
});

// Configuração do Banco de Dados
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SindautoHubContext>(options =>
    options.UseNpgsql(connectionString)
);

// Configuração do FluentValidation 
builder.Services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);


// --- Fim da configuração dos serviços ---

var app = builder.Build();

// --- Configurar o pipeline de requisições HTTP ---
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