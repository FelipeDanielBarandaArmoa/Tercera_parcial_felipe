using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Infraestructura.Datos; 
using Infraestructura.Modelos;

namespace OptativoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UsuarioDatos usuarioDatos;
        private readonly string claveSecreta = "ClaveSecreta123"; 

        public AuthController(string cadenaConexion)
        {
            usuarioDatos = new UsuarioDatos(cadenaConexion);
        }

        [HttpPost("getToken")]
        public IActionResult ObtenerToken([FromBody] LoginModel loginModel)
        {
            var usuario = usuarioDatos.ObtenerUsuarioPorCredenciales(loginModel.NombreUsuario, loginModel.Contraseña);

            if (usuario == null)
                return Unauthorized();

            var token = GenerarToken(usuario);
            return Ok(new { Token = token });
        }

        private string GenerarToken(UsuarioModel usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, $"{usuario.NombreUsuario} {usuario.Apellido}"),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Nivel)
                
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveSecreta));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "fbaranda",
                audience: "fabaranda",
                claims: claims,
                expires: DateTime.Now.AddHours(1), 
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
