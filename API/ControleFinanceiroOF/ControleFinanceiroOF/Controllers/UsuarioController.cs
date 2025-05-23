using ControleFinanceiroOF.Data;
using ControleFinanceiroOF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiroOF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        // Criar novo usuário
        [HttpPost]
        public IActionResult CriarUsuario(UsuarioModel usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return CreatedAtAction(nameof(ObterUsuario), new { id = usuario.IdUsuario }, usuario);
        }

        // Obter usuário por ID
        [HttpGet("{id}")]
        public IActionResult ObterUsuario(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null) return NotFound();

            return Ok(usuario);
        }

        // Listar todos os usuários
        [HttpGet]
        public IActionResult ListarUsuarios()
        {
            var usuarios = _context.Usuarios.ToList();
            return Ok(usuarios);
        }

        // Atualizar usuário
        [HttpPut("{id}")]
        public IActionResult AtualizarUsuario(int id, UsuarioModel usuarioAtualizado)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null) return NotFound();

            usuario.Nome = usuarioAtualizado.Nome;
            usuario.Email = usuarioAtualizado.Email;
            usuario.Senha = usuarioAtualizado.Senha;

            _context.SaveChanges();
            return Ok(usuario);
        }

        // Deletar usuário
        [HttpDelete("{id}")]
        public IActionResult DeletarUsuario(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null) return NotFound();

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
            return NoContent();
        }

        // Verificar login (email + senha)
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest login)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Email == login.Email && u.Senha == login.Senha);

            if (usuario == null)
                return Unauthorized("Email ou senha inválidos");

            return Ok(new
            {
                usuario.IdUsuario,
                usuario.Nome,
                usuario.Email,
                usuario.TokenID
            });
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}
