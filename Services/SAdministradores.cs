using ApiDonAldo.Context;
using ApiDonAldo.Models;
using ApiDonAldo.Models.Auth;
using ApiDonAldo.Models.DTOs.AdministradorDTO;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Construction;

namespace ApiDonAldo.Services
{
	public class SAdministradores
	{
		private readonly IMapper mapper;
		private readonly UserManager<Users> userManager;
		private readonly AppDbContext context;
		private readonly SToken sToken;

        public SAdministradores(IMapper mapper, UserManager<Users> userManager, SToken sToken, AppDbContext context)
        {
			this.sToken = sToken;
			this.mapper = mapper;	
			this.userManager = userManager;
			this.context = context;
            
        }

		public async Task<RtaAuth> CreateAdministradorAsync (AdministradorCreacionDTO administradorCreacionDTO)
		{
			try
			{
			var newAdmin = mapper.Map<Users>(administradorCreacionDTO);
			newAdmin.UserName = administradorCreacionDTO.Email;
			newAdmin.FechaCreacion = DateTime.Now;
			newAdmin.EsAdmin = true;

			var result = await userManager.CreateAsync(newAdmin, administradorCreacionDTO.Password);
			if (!result.Succeeded)
				throw new Exception();

			var resultrole = await userManager.AddToRoleAsync(newAdmin, "admin");
			var rtaAuth = await sToken.GenerateToken(newAdmin.Email, 1);

			return rtaAuth;

			}

			catch (Exception ex)
			{

				Console.WriteLine( "Error" + ex.Message);
				return null;
			}

		}

    }
}
