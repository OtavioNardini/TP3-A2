using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PlataformaB2B_A2_TP3.DTOs;
using PlataformaB2B_A2_TP3.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PlataformaB2B_A2_TP3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthController(
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        /// <summary>
        /// Registra um novo usuário
        /// </summary>
        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register(RegisterDto dto)
        {
            var usuario = new Usuario
            {
                UserName = dto.Email,
                Email = dto.Email,
                Nome = dto.Nome,
                OrgId = dto.OrgId,
                FornecedorId = dto.FornecedorId
            };

            var result = await _userManager.CreateAsync(usuario, dto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // Atribuir role padrão
            await _userManager.AddToRoleAsync(usuario, "Comprador");

            var token = await GenerateJwtToken(usuario);
            var roles = await _userManager.GetRolesAsync(usuario);

            return Ok(new AuthResponseDto
            {
                Token = token,
                Email = usuario.Email,
                Nome = usuario.Nome,
                Roles = roles.ToList()
            });
        }

        /// <summary>
        /// Realiza login
        /// </summary>
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginDto dto)
        {
            var usuario = await _userManager.FindByEmailAsync(dto.Email);
            if (usuario == null)
                return Unauthorized("Email ou senha inválidos");

            var result = await _signInManager.CheckPasswordSignInAsync(usuario, dto.Password, false);
            if (!result.Succeeded)
                return Unauthorized("Email ou senha inválidos");

            // Atualizar último login
            usuario.LastLogin = DateTime.UtcNow;
            await _userManager.UpdateAsync(usuario);

            var token = await GenerateJwtToken(usuario);
            var roles = await _userManager.GetRolesAsync(usuario);

            return Ok(new AuthResponseDto
            {
                Token = token,
                Email = usuario.Email,
                Nome = usuario.Nome,
                Roles = roles.ToList()
            });
        }

        private async Task<string> GenerateJwtToken(Usuario usuario)
        {
            var roles = await _userManager.GetRolesAsync(usuario);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim("nome", usuario.Nome),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
