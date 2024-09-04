using ControleInadimplentesAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ControleInadimplentesAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        // Endpoint de login
        [AllowAnonymous] // Permite acesso sem autenticação
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioModel login)
        {
            var user = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == login.Email && u.Senha == login.Senha);

            if (user == null)
            {
                return Unauthorized("E-mail ou senha inválidos");
            }

            // Gerando o token JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("seu_segredo_super_secreto"); // Substitua pela mesma chave usada no Program.cs
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }

        // GET: api/Usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioModel>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuario/5
        [HttpGet("{idusuario}")]
        public async Task<ActionResult<UsuarioModel>> GetUsuario(int IdUsuario)
        {
            var user = await _context.Usuarios.FindAsync(IdUsuario);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST: api/Usuario
        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> CreateUsuario([FromBody] UsuarioModel CriarUsuario)
        {
            _context.Usuarios.Add(CriarUsuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuario), new { idusuario = CriarUsuario.IdUsuario }, CriarUsuario);
        }

        // PUT: api/Usuario/5
        [HttpPut("{idusuario}")]
        public async Task<IActionResult> UpdateUsuario(int idusuario, [FromBody] UsuarioModel AtualizUsuario)
        {
            if (idusuario != AtualizUsuario.IdUsuario)
            {
                return BadRequest();
            }

            _context.Entry(AtualizUsuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(idusuario))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Usuario/5
        [HttpDelete("{idusuario}")]
        public async Task<IActionResult> DeleteUsuario(int idusuario)
        {
            var usuario = await _context.Usuarios.FindAsync(idusuario);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int idusuario)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == idusuario);
        }
    }
}