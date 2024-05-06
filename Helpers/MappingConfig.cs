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
                config.CreateMap<ClienteDTO, Cliente>().ReverseMap();
                config.CreateMap<Cliente, ClienteDTO>();
                config.CreateMap<Users, AdministradorDTO>().ReverseMap();
                config.CreateMap<AdministradorCreacionDTO, Users>();

			});

            return mappingConfig;
            
        }
    }
}
