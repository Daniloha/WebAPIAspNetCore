using WebApiCadastro.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using WebApiCadastro.Models.Context;
using WebApiCadastro.Repository;
using EvolveDb;
using MySqlConnector;
using Serilog;
using WebApiCadastro.Business;
using WebApiCadastro.Business.Implementations;
using WebApiCadastro.Repository.Generic;



var builder = WebApplication.CreateBuilder(args);


// Inje��o de depend�ncias
builder.Services.AddScoped<IPersonBusiness, PersonBuisnessImplementation>();
builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>(); // Adiciona o reposit�rio no containe
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>)); // Adiciona o reposit�rio no containe (GenericRepo);

// Adiciona os controladores ao container
builder.Services.AddControllers();

// Obt�m a string de conex�o corretamente
var connection = builder.Configuration.GetConnectionString("DefaultConnection");


// Configura o DbContext com a string de conex�o
builder.Services.AddDbContext<MySQLContext>(options =>
    options.UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 29)),
        mysqlOptions => mysqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5, // N�mero m�ximo de tentativas de reconex�o
            maxRetryDelay: TimeSpan.FromSeconds(10), // Tempo m�ximo de espera entre as tentativas
            errorNumbersToAdd: null)) // N�meros de erro adicionais para considerar como falhas transit�rias
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
