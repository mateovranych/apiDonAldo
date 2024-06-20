using ApiDonAldo.Models;
using ApiDonAldo.Models.DTOs.AdministradorDTO;
using ApiDonAldo.Models.DTOs.ClienteDTO;
using ApiDonAldo.Models.DTOs.ProductoDTO;
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
                config.CreateMap<Users, ClienteDTO>().ReverseMap();
                config.CreateMap<ClienteCreacionDTO, Users>();

                //Administradores
                config.CreateMap<Users, AdministradorDTO>().ReverseMap();
                config.CreateMap<AdministradorCreacionDTO, Users>();

                //productos
                config.CreateMap<ProductoCreacionDTO, Producto>().ReverseMap();
                config.CreateMap<Producto, ProductoDTO>();
                config.CreateMap<ProductoEdicionDTO, ProductoDTO>().ReverseMap();
                config.CreateMap<Producto, ProductoEdicionDTO>().ReverseMap();
                config.CreateMap<ProductoEdicionDTO, Producto>();
                


			});

            return mappingConfig;
            
        }
    }
}
