using ApiDonAldo.Context;
using ApiDonAldo.Helpers;
using ApiDonAldo.Models;
using ApiDonAldo.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using MercadoPago.Config;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
MercadoPagoConfig.AccessToken = "TEST-1929079393514220-071121-5425069061a738f7407f88929c88c3c1-206326705";
//Hago una cadena de conexión que va a servir para enlazarse con el appsettings.json.
string connectionString = builder.Configuration.GetConnectionString("defaultConnection");
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
//Inyecto los servicios
builder.Services.AddScoped<SToken>();
builder.Services.AddScoped<SAdministradores>();
builder.Services.AddScoped<SClientes>();
builder.Services.AddScoped<SProductos>();
builder.Services.AddScoped<SCuentas>();
builder.Services.AddScoped<dataSeeder>();
//Configuración de cors
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(builder =>
	{
		builder.AllowAnyOrigin()
			   .AllowAnyMethod()
			   .AllowAnyHeader();
	});
});

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;

	try
	{
		var dataSeeder = services.GetRequiredService<dataSeeder>();
		await dataSeeder.CrearRoles();
		await dataSeeder.CrearAdmin();
		
	}
	catch (Exception ex)
	{
		Console.WriteLine("Error desconocido: " + ex.Message);

	}
}

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions
{
	FileProvider = new PhysicalFileProvider(@"C:\Users\Mateo\Desktop\test\images"),
	RequestPath = "/images"
});

app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.Run();
