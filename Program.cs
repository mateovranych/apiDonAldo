using ApiDonAldo.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//Hago una cadena de conexión que va a servir para enlazarse con el appsettings.json.
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//Hago una variable llamada ServerVersion.
var ServerVersion = new MySqlServerVersion(new Version(8,0,33));
//Realizo la conexión.
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion));





builder.Services.AddControllers();
builder.Services.AddSwaggerGen();




//Configuración del jwt
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = builder.Configuration["Jwt:Issuer"],
		ValidAudience = builder.Configuration["Jwt:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]))

	};
});




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
