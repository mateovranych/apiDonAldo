using ApiDonAldo.Context;
using ApiDonAldo.Models.DTOs.ProductoDTO;
using ApiDonAldo.Models.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductoDTO>(producto);

        }

        public async Task<List<ProductoDTO>> GetProductosAsync()
        {
			try
			{
            var productos = await _context.Productos.ToListAsync();

            return _mapper.Map<List<ProductoDTO>>(productos);

			}
			catch (Exception)
			{

				throw;
			}
        }

        public async Task<string> GuardarImagenAsync(IFormFile imagen)
        {
			
			var directoryPath = Path.Combine("C:\\Users\\Mateo\\Desktop\\test", "images");

			
			if (!Directory.Exists(directoryPath))
			{
				Directory.CreateDirectory(directoryPath);
			}

			
			var imagePath = Path.Combine(directoryPath, imagen.FileName);

			
			using (var stream = new FileStream(imagePath, FileMode.Create))
			{
				await imagen.CopyToAsync(stream);
			}

			
			return $"/images/{imagen.FileName}";

			
		}
                


	}
}
