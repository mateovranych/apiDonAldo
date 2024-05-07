using ApiDonAldo.Models.Auth;
using ApiDonAldo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiDonAldo.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CuentasController : ControllerBase
	{
        private readonly SCuentas sCuentas;
        private readonly SToken sToken;

        public CuentasController(SCuentas sCuentas, SToken sToken)
        {      
            this.sCuentas = sCuentas;
            this.sToken = sToken;            
        }

        [HttpPost("login")]
        public async Task<ActionResult<RtaAuth>>Login(Credentials credentials)
        {
            var result = await sCuentas.LoginAsync(credentials);
            if(!result)
            return NotFound("Credenciales inválidas");
            var generarTokenLogin = await sToken.GenerateToken(email: credentials.Email, diasExp: 1);
            return Ok(generarTokenLogin);
        }
    }
}
