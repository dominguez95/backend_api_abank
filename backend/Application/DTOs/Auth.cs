 

namespace Application.DTOs
{
    public class LoginDto
    {
        public string Telefono { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class AuthResponseDto
    {
        public Domain.Entities.User User { get; set; } = null!;
        public string AccessToken { get; set; } = null!;
        public string TokenType { get; set; } = "bearer";
    }
}
