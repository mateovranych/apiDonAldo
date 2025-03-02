using ApiDonAldo.Models.Entities;

namespace ApiDonAldo.Models
{
	public class Venta
	{
		public int Id { get; set; }
		public DateTime Fecha { get; set; }
		public int ProductoId { get; set; }
		public Producto producto { get; set; }	
		public int Cantidad { get; set; }
		public string Estado { get; set; }
	}
}
