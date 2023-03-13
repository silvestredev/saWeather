using CatalogAPI.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddJsonOptions(options =>
	options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles //durante a serialização do json, cycles serão ignorados
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//string de conexao com o bando de dados
string? conn = builder.Configuration.GetConnectionString("DefaultConnection");

//configurando o contexto, informando o provedor e a string de conexao
builder.Services.AddDbContext<CatalogAPIDbContext>(options =>
	options.UseMySql(conn, ServerVersion.AutoDetect(conn)));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
