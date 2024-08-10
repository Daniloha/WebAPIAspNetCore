using WebApiCadastro.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using WebApiCadastro.Models.Context;
using WebApiCadastro.Buisness;
using WebApiCadastro.Buisness.Implementations;
using WebApiCadastro.Repository;
using WebApiCadastro.Repository.Implementations;
using EvolveDb;
using MySqlConnector;
using Serilog;



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

if (builder.Environment.IsDevelopment())
{
    MigrateDatabase(connection!);
}

// Versionamento da API    
builder.Services.AddApiVersioning();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void MigrateDatabase(string connection)
{
    try
    {
        var evolveConnection = new MySqlConnection(connection);
        var evolve = new Evolve(evolveConnection, Log.Information)
        {
            Locations = new List<string> { "db/migration", "db/dataset" },
            IsEraseDisabled = true
        };
        evolve.Migrate();

    }
    catch (Exception ex)
    {
        Log.Error("Database migration failed", ex);
        throw;
    };

}
