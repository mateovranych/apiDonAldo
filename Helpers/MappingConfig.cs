using ApiDonAldo.Models;
using ApiDonAldo.Models.DTOs.AdministradorDTO;
using ApiDonAldo.Models.DTOs.ClienteDTO;
using ApiDonAldo.Models.Entities;
using AutoMapper;

namespace ApiDonAldo.Helpers
{
	public class MappingConfig
	{
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                //Clientes
                config.CreateMap<ClienteDTO, Cliente>().ReverseMap();
                config.CreateMap<Cliente, ClienteDTO>();

                //Administradores
                config.CreateMap<Users, AdministradorDTO>().ReverseMap();
                config.CreateMap<AdministradorCreacionDTO, Users>();

			});

            return mappingConfig;
            
        }
    }
}
