﻿using ApiDonAldo.Models.DTOs.ProductoDTO;
using ApiDonAldo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiDonAldo.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductosController : ControllerBase
	{
        private readonly SProductos _sproductos;
        public ProductosController(SProductos sProductos)
        {
            _sproductos = sProductos;
            
        }


		[HttpPost]
		public async Task<IActionResult> CrearProducto([FromForm] ProductoCreacionDTO productoCreacionDTO)
		{
			var producto = await _sproductos.CrearProductosAsync(productoCreacionDTO);
			return Ok(producto);
		}


		
	}
}
