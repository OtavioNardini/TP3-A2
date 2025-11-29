using System.ComponentModel.DataAnnotations;

namespace PlataformaB2B_A2_TP3.DTOs
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        public string Nome { get; set; }

        public int? OrgId { get; set; }
        public int? FornecedorId { get; set; }
    }

    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class AuthResponseDto
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public List<string> Roles { get; set; }
    }
}
