using WebApiCadastro.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using WebApiCadastro.Models.Context;
using WebApiCadastro.Buisness;
using WebApiCadastro.Buisness.Implementations;
using WebApiCadastro.Repository;
using WebApiCadastro.Repository.Implementations;

var builder = WebApplication.CreateBuilder(args);


// Registro do serviço IPersonService
builder.Services.AddScoped<IPersonBuisness, PersonBuisnessImplementation>();
builder.Services.AddScoped<IPersonRepository, PersonRepositoryImplementation>(); // Adiciona o repositório no containe

// Adiciona os controladores ao container
builder.Services.AddControllers();

// Obtém a string de conexão corretamente
var connection = builder.Configuration.GetConnectionString("DefaultConnection");

// Configura o DbContext com a string de conexão
builder.Services.AddDbContext<MySQLContext>(options =>
    options.UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 29)),
        mysqlOptions => mysqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5, // Número máximo de tentativas de reconexão
            maxRetryDelay: TimeSpan.FromSeconds(10), // Tempo máximo de espera entre as tentativas
            errorNumbersToAdd: null)) // Números de erro adicionais para considerar como falhas transitórias
    .EnableSensitiveDataLogging() // Para logs detalhados
    .EnableDetailedErrors());

// Versionamento da API    
builder.Services.AddApiVersioning();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
