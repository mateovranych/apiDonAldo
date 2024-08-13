namespace ApiDonAldo.Models.DTOs.PagoDTO
{
	public class PagoDTO
	{
		public int ProductoId { get; set; }
		public int Cantidad { get; set; }
		public string Token { get; set; }
		public string MetodoPago { get; set; }
		public string Email { get; set; }
	}
}
