﻿namespace ApiDonAldo.Models.Entities
{
	public class Producto
	{

		public int Id { get; set; }
		public string Nombre { get; set; }
		public string Descripcion { get; set; }
		public decimal Precio { get; set; }
		public string ImagenUrl { get; set; } // URL de la imagen

	}
}
