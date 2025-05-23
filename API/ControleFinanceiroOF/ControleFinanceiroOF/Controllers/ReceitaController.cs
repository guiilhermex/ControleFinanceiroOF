using ControleFinanceiroOF.Data;
using ControleFinanceiroOF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiroOF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceitaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReceitaController(AppDbContext context)
        {
            _context = context;
        }

        // Criar receita
        [HttpPost]
        public IActionResult CriarReceita(ReceitaModel receita)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Receitas.Add(receita);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterReceita), new { id = receita.Id }, receita);
        }

        // Obter receita por ID
        [HttpGet("{id}")]
        public IActionResult ObterReceita(int id)
        {
            var receita = _context.Receitas
                .Include(r => r.Usuario)
                .FirstOrDefault(r => r.Id == id);

            if (receita == null)
                return NotFound("Receita não encontrada.");

            return Ok(receita);
        }

        // Listar todas as receitas
        [HttpGet]
        public IActionResult ListarReceitas()
        {
            var receitas = _context.Receitas
                .Include(r => r.Usuario)
                .ToList();

            return Ok(receitas);
        }

        // Listar receitas por usuário
        [HttpGet("usuario/{usuarioId}")]
        public IActionResult ListarReceitasPorUsuario(int usuarioId)
        {
            var receitas = _context.Receitas
                .Where(r => r.UsuarioId == usuarioId)
                .Include(r => r.Usuario)
                .ToList();

            return Ok(receitas);
        }

        // Listar receitas por período
        [HttpGet("periodo")]
        public IActionResult ListarReceitasPorPeriodo(DateTime inicio, DateTime fim)
        {
            var receitas = _context.Receitas
                .Where(r => r.Data >= inicio && r.Data <= fim)
                .Include(r => r.Usuario)
                .ToList();

            return Ok(receitas);
        }

        // Atualizar receita
        [HttpPut("{id}")]
        public IActionResult EditarReceita(int id, ReceitaModel receitaAtualizada)
        {
            var receitaExistente = _context.Receitas.Find(id);
            if (receitaExistente == null)
                return NotFound("Receita não encontrada.");

            receitaExistente.Valor = receitaAtualizada.Valor;
            receitaExistente.Data = receitaAtualizada.Data;
            receitaExistente.Descricao = receitaAtualizada.Descricao;
            receitaExistente.Extra = receitaAtualizada.Extra;
            receitaExistente.UsuarioId = receitaAtualizada.UsuarioId;

            _context.SaveChanges();

            return Ok(receitaExistente);
        }

        // Deletar receita
        [HttpDelete("{id}")]
        public IActionResult ExcluirReceita(int id)
        {
            var receita = _context.Receitas.Find(id);
            if (receita == null)
                return NotFound("Receita não encontrada.");

            _context.Receitas.Remove(receita);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
