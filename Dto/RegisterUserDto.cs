namespace Asseti_Fi.Dto
{
    public class RegisterUserDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // Admin or User
        
        public string Address { get; set; }
        public string ContactNumber { get; set; }
    }
}