namespace Spg.SpengerShop.Domain.Dtos
{
    public class LoginDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; }
    }
}
