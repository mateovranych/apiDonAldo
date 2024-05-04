using System.ComponentModel.DataAnnotations;

namespace ApiDonAldo.Models.Auth
{
	public class Credentials
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
