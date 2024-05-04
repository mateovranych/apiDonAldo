using ApiDonAldo.Models;
using ApiDonAldo.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiDonAldo.Context
{
	public class AppDbContext : IdentityDbContext<Users>
	{
        public AppDbContext(DbContextOptions options):base(options)
        {

            
        }

        public DbSet<Cliente> Clientes { get; set; }

	

	}
}
