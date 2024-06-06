using ApiDonAldo.Models;
using ApiDonAldo.Models.DTOs.ClienteDTO;
using ApiDonAldo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiDonAldo.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ClientesController : ControllerBase
	{
        private readonly SToken sToken;
        private readonly SClientes sClientes;
        private readonly UserManager<Users> userManager;
        public ClientesController(SClientes sClientes, SToken sToken, UserManager<Users> userManager)
        {
            this.userManager = userManager;
            this.sToken = sToken;
            this.sClientes = sClientes;            
        }


        [HttpGet]
        public async Task<ActionResult<List<ClienteDTO>>> GetClientes()
        {
            try{
                var clientes = await sClientes.GetClientesActivosAsync();
                return Ok(clientes);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
    }
}
