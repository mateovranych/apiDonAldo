namespace ApiDonAldo.Models.Entities
{
	public class Producto
	{

		public int Id { get; set; }
		public string Nombre { get; set; }
		public string Descripcion { get; set; }
		public decimal Precio { get; set; }		
		public int Stock { get; set; }
		public string ImagenUrl { get; set; } 
		public string Imagen { get; set; }
		public string NombreArchivoImagen { get; set; }
		public DateTime FechaCreacion { get; set; }
		public DateTime? FechaEliminacion { get; set; }
		public bool Eliminado { get; set; }

	}
}
