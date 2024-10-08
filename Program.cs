using Microsoft.EntityFrameworkCore;
using WebApiCadastro.Models.Context;
using EvolveDb;
using MySqlConnector;
using Serilog;
using WebApiCadastro.Business;
using WebApiCadastro.Business.Implementations;
using WebApiCadastro.Repository.Generic;
using Microsoft.Net.Http.Headers;
using WebApiCadastro.HyperMedia.Filters;
using WebApiCadastro.HyperMedia.Enricher;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Rewrite;





var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
}
));
// Inje��o de depend�ncias
builder.Services.AddScoped<IPersonBusiness, PersonBuisnessImplementation>();
builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>(); // Adiciona o reposit�rio no containe
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>)); // Adiciona o reposit�rio no containe (GenericRepo);

builder.Services.AddRouting();
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

builder.Services.AddMvc(options =>
{
    options.RespectBrowserAcceptHeader = true;

    options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("aplication/xml"));
    options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("aplication/json"));

}
).AddXmlSerializerFormatters();

var filterOptions = new HyperMediaFilterOptions();
filterOptions.ContentResponseEnricherList.Add(new PessoaEnricher());
filterOptions.ContentResponseEnricherList.Add(new LivrosEnricher());

builder.Services.AddSingleton(filterOptions); // Adiciona o reposit�rio no container

// Versionamento da API    
builder.Services.AddApiVersioning();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo { Title = "WebApiCadastro",
                          Version = "v1",
                          Description = "API para cadastro de pessoas e livros",
                          Contact = new OpenApiContact
                          {
                              Name = "Danilo",
                              Email = "danilo.h.araujo@gmail.com",
                              Url = new Uri("https://github.com/Daniloha/WebAPIAspNetCore")
                          }
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json",
        "WebApiCadastro v1");
});
var option = new RewriteOptions().AddRedirect("^$", "/swagger");

app.UseRewriter(option);

app.UseAuthorization();

app.MapControllers();
app.MapControllerRoute("DefaultApi", "{controller=values}/v{version=apiVersion}/{id?}");

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
