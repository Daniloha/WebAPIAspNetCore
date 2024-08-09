using WebApiCadastro.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using WebApiCadastro.Models.Context;
using WebApiCadastro.Buisness;
using WebApiCadastro.Buisness.Implementations;
using WebApiCadastro.Repository;
using WebApiCadastro.Repository.Implementations;

var builder = WebApplication.CreateBuilder(args);


// Registro do servi�o IPersonService
builder.Services.AddScoped<IPersonBuisness, PersonBuisnessImplementation>();
builder.Services.AddScoped<IPersonRepository, PersonRepositoryImplementation>(); // Adiciona o reposit�rio no containe

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

// Versionamento da API    
builder.Services.AddApiVersioning();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
