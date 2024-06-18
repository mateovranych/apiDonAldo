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
		private readonly string rutaAlmacenamiento;
		private readonly string rutaServidor;


		public SProductos(IMapper mapper, AppDbContext context, IConfiguration configuration)
		{
			_context = context;
			_mapper = mapper;
			this.rutaAlmacenamiento = configuration["rutaImagenes"]!;
			this.rutaServidor = configuration["rutaServidor"]!;
		}

		public async Task<ProductoDTO> CrearProductosAsync(ProductoCreacionDTO productoCreacionDTO)
		{
			var producto = _mapper.Map<Producto>(productoCreacionDTO);
			if (productoCreacionDTO.Imagen != null)
			{
				var imagenUrl = await GuardarImagenAsync(productoCreacionDTO.Imagen);
				producto.ImagenUrl = imagenUrl;
			}

			string nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(productoCreacionDTO.Imagen.FileName);
			string rutaCompleta = Path.Combine(rutaAlmacenamiento, nombreArchivo);

			using (var stream = new FileStream(rutaCompleta, FileMode.Create))
			{
				await productoCreacionDTO.Imagen.CopyToAsync(stream);
			}

			producto.NombreArchivoImagen = nombreArchivo;
			producto.Imagen = Path.Combine(rutaServidor, nombreArchivo);

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
			// Generar un nombre de archivo único
			string nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(imagen.FileName);
			// Ruta completa para almacenar la imagen
			string rutaCompleta = Path.Combine(rutaAlmacenamiento, nombreArchivo);
			// Guardar la imagen en el sistema de archivos
			using (var stream = new FileStream(rutaCompleta, FileMode.Create))
			{
				await imagen.CopyToAsync(stream);
			}
			// Devolver la URL absoluta de la imagen
			return $"{rutaServidor}/images/{nombreArchivo}";

		}
	}
}