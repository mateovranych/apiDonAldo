using ApiDonAldo.Context;
using ApiDonAldo.Helpers;
using ApiDonAldo.Models;
using ApiDonAldo.Models.Entities;
using ApiDonAldo.Repo;
using ApiDonAldo.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//Hago una cadena de conexión que va a servir para enlazarse con el appsettings.json.
string connectionString = builder.Configuration.GetConnectionString("defaultConnetcion");
//Hago una variable llamada ServerVersion.
var ServerVersion = new MySqlServerVersion(new Version(8,0,33));
//Realizo la conexión.
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion));
//Añado identity para usar las tablas de asp.net.

builder.Services.AddIdentity<Users, IdentityRole>(x => x.Password.RequireNonAlphanumeric = false)
.AddEntityFrameworkStores<AppDbContext>();

//Añado el automapper
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//builder.Services.AddScoped<IClienteRepo, ClienteRepo>(); //Sirve para no usar el context directamente.

builder.Services.AddScoped<SAdministradores>();
builder.Services.AddScoped<SToken>();



builder.Services.AddControllers();
builder.Services.AddSwaggerGen();


//Configuración del jwt





var app = builder.Build();


// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	
}



app.UseHttpsRedirection();

//Uso la autenticación.
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
