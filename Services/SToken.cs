﻿using ApiDonAldo.Models;
using ApiDonAldo.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiDonAldo.Services
{
	public class SToken
	{
		private readonly IConfiguration configuration;
		private readonly UserManager<Users> userManager;


        public SToken(IConfiguration configuration, UserManager<Users> userManager)
        {
            this.configuration = configuration;
            this.userManager = userManager;
            
        }

        public async Task<RtaAuth> GenerateToken(string email, int diasExp) //Hago una consulta asíncrona con la base de datos,pido como parámetros el string y los dias de expiracion
        {
            var usuario = await userManager.FindByEmailAsync(email); //Los busco por el email
            var roles = await userManager.GetRolesAsync(usuario); //Obtengo los roles de la tabla usuarios del asp
            var claims = new List<Claim>() //Hago una lista llamada Claim.
            {
                new Claim("mail", email)
            };

            foreach (var role in roles) //Hago un foreach para recorrer los roles y si el
            {
                claims.Add(new Claim(ClaimTypes.Role, role)); //Claims.add agrega un nuevo tipo de claims en role. de tipo role 
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Clavesecreta123.!"])); 
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(diasExp);
            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                signingCredentials: creds
                );
            var rtaTkoen = new JwtSecurityTokenHandler().WriteToken(token);
            return new RtaAuth(token: rtaTkoen, esAdmin: usuario.EsAdmin);

        }
    }
}
