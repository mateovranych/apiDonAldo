using ApiDonAldo.Context;
using ApiDonAldo.Models;
using ApiDonAldo.Models.Auth;
using ApiDonAldo.Models.DTOs.ClienteDTO;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiDonAldo.Services
{
	public class SClientes
	{
        private readonly IMapper mapper;
        private readonly UserManager<Users> _userManager;
        private readonly AppDbContext _context;
        private readonly SToken _sToken;


        public SClientes(IMapper mapper, UserManager<Users> userManager, AppDbContext context, SToken sToken)
        {
            this.mapper = mapper;
            this._userManager = userManager;
            this._context = context;
            this._sToken = sToken;              
        }

        public async Task<RtaAuth> CreateClientes ( ClienteCreacionDTO clienteCreacionDTO)
        {
            try{
                var nuevoCliente = mapper.Map<Users>(clienteCreacionDTO);
                nuevoCliente.UserName = clienteCreacionDTO.Email;
                nuevoCliente.FechaCreacion = DateTime.UtcNow;
                nuevoCliente.EsAdmin = false;

                var resultado = await _userManager.CreateAsync(nuevoCliente, clienteCreacionDTO.Password);

                if(!resultado.Succeeded)
                {
                    throw new Exception("El cliente no se pudo crear");
                }

                var resultadoRol = await _userManager.AddToRoleAsync(nuevoCliente, "cliente");

                var respuestaAutenticacion = await _sToken.GenerateToken(nuevoCliente.Email, 1);

                return respuestaAutenticacion;

            }
            catch (Exception)
            {

                throw;
            }
        }

		public async Task<List<ClienteDTO>> GetClientesActivosAsync()
		{
			try
			{
				var clientes = await _context.Users.Where(user => user.EsAdmin == false && user.Activo == true).ToListAsync();

				return mapper.Map<List<ClienteDTO>>(clientes);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task DeleteClient(string email)
        {
            try
            {
                var cliente = await _userManager.FindByEmailAsync(email);
                if (cliente == null || cliente.EsAdmin) throw new Exception();

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
            

        }

    }
}
