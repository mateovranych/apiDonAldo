using ApiDonAldo.Models.DTOs.PagoDTO;
using ApiDonAldo.Models;
using MercadoPago.Client.Payment;
using MercadoPago.Resource.Payment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiDonAldo.Context;

namespace ApiDonAldo.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VentaController : ControllerBase
	{
		private readonly AppDbContext _context;

		public VentaController(AppDbContext context)
		{
			_context = context;
		}

		[HttpPost]
		public async Task<IActionResult> CrearVenta([FromBody] PagoDTO pagoDto)
		{
			var producto = await _context.Productos.FindAsync(pagoDto.ProductoId);
			if (producto == null || producto.Stock < pagoDto.Cantidad)
			{
				return BadRequest("Producto no disponible o stock insuficiente.");
			}
			
			var venta = new Venta
			{
				Fecha = DateTime.Now,
				ProductoId = pagoDto.ProductoId,
				Cantidad = pagoDto.Cantidad,
				Estado = "Pendiente"
			};

			await _context.SaveChangesAsync();
			
			var client = new PaymentClient();

			var paymentCreateRequest = new PaymentCreateRequest
			{
				TransactionAmount = producto.Precio * pagoDto.Cantidad,
				Token = pagoDto.Token,
				Description = $"Compra de {producto.Nombre}",
				PaymentMethodId = pagoDto.MetodoPago,
				Payer = new PaymentPayerRequest
				{
					Email = pagoDto.Email
				}
			};

			Payment payment = await client.CreateAsync(paymentCreateRequest);

			if (payment.Status == "approved")
			{
				// Actualizar stock y estado de la venta
				producto.Stock -= pagoDto.Cantidad;
				venta.Estado = "Aprobada";
				await _context.SaveChangesAsync();

				return Ok(payment);
			}
			
			return BadRequest(payment);
		}
	}
}
