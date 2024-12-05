using System.ComponentModel.DataAnnotations;

namespace Asseti_Fi.Dto
{
	public class AddUserDto
	{
		public required string UserType { get; set; }
		[Required(ErrorMessage = "Name is required."), MaxLength(20)]
		public required string Name { get; set; }

		public string Email { get; set; }
		[Required(ErrorMessage = "Password is required.")]
		[DataType(DataType.Password)]
		[StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
		public required string Password { get; set; }
		public string? ContactNumber { get; set; }
		public string? Address { get; set; }
		public DateTime DateCreated { get; set; } = DateTime.UtcNow;
	}
}
