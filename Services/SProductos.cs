using ApiDonAldo.Context;
using ApiDonAldo.Models.DTOs.ProductoDTO;
using ApiDonAldo.Models.Entities;
using AutoMapper;

namespace ApiDonAldo.Services
{
	public class SProductos
	{
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        

        public SProductos(IMapper mapper, AppDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductoDTO> CrearProductosAsync(ProductoCreacionDTO productoCreacionDTO)
        {
            var producto = _mapper.Map<Producto>(productoCreacionDTO);
            if(productoCreacionDTO.Imagen != null)
            {
                var imagenUrl = await GuardarImagenAsync(productoCreacionDTO.Imagen);
                producto.ImagenUrl = imagenUrl;
            }

            //_context.Productos.Add(producto);  
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductoDTO>(producto);

        }

        public async Task<string> GuardarImagenAsync(IFormFile imagen)
        {
			// Definir la nueva ruta donde se guardará la imagen
			var directoryPath = Path.Combine("C:\\Users\\Mateo\\Desktop\\test", "images");

			// Verificar si el directorio existe, si no, crearlo
			if (!Directory.Exists(directoryPath))
			{
				Directory.CreateDirectory(directoryPath);
			}

			// Definir la ruta completa del archivo
			var imagePath = Path.Combine(directoryPath, imagen.FileName);

			// Guardar el archivo en la ruta especificada
			using (var stream = new FileStream(imagePath, FileMode.Create))
			{
				await imagen.CopyToAsync(stream);
			}

			// Devolver la URL relativa del archivo
			return $"/images/{imagen.FileName}";

			//var imagenPath = Path.Combine("wwwroot/images",imagen.FileName);

			//using (var stream = new FileStream(imagenPath, FileMode.Create))
			//{
			//    await imagen.CopyToAsync(stream);
			//}



			// return $"/images/{imagen.FileName}";
		}
                


	}
}
