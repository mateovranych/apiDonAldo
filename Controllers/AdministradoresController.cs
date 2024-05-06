using ApiDonAldo.Models;
using ApiDonAldo.Models.DTOs.AdministradorDTO;
using ApiDonAldo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiDonAldo.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AdministradoresController : ControllerBase
	{
		private readonly UserManager<Users> userManager;
		private readonly SAdministradores sadministradores;
        public AdministradoresController(UserManager<Users> userManager, SAdministradores sAdministradores)
        {
			this.sadministradores = sAdministradores;
			this.userManager = userManager;
            
        }


		[HttpPost]
		public async Task<ActionResult<Users>> PostAdministrador([FromBody] AdministradorCreacionDTO administradorCreacionDTO)
		{
			try
			{
				var nuevoAdministrador = await sadministradores.CreateAdministradorAsync(administradorCreacionDTO);

				return Ok(nuevoAdministrador);
			}
			catch 
			{
				return BadRequest();
			}
			
		}
	}
}
