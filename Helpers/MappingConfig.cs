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
                config.CreateMap<ClienteDTO, Cliente>();

                config.CreateMap<Cliente, ClienteDTO>();

			});

            return mappingConfig;
            
        }
    }
}
