using ControleFinanceiroOF.Data;
using ControleFinanceiroOF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiroOF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DespesaController(AppDbContext context)
        {
            _context = context;
        }

        // Criar despesa
        [HttpPost]
        public IActionResult CriarDespesa(DespesaModel despesa)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Despesas.Add(despesa);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterDespesa), new { id = despesa.Id }, despesa);
        }

        // Obter despesa por ID
        [HttpGet("{id}")]
        public IActionResult ObterDespesa(int id)
        {
            var despesa = _context.Despesas
                .Include(d => d.Usuario)
                .FirstOrDefault(d => d.Id == id);

            if (despesa == null)
                return NotFound("Despesa não encontrada.");

            return Ok(despesa);
        }

        // Listar todas as despesas
        [HttpGet]
        public IActionResult ListarDespesas()
        {
            var despesas = _context.Despesas
                .Include(d => d.Usuario)
                .ToList();

            return Ok(despesas);
        }

        // Listar despesas por usuário
        [HttpGet("usuario/{usuarioId}")]
        public IActionResult ListarDespesasPorUsuario(int usuarioId)
        {
            var despesas = _context.Despesas
                .Where(d => d.UsuarioId == usuarioId)
                .Include(d => d.Usuario)
                .ToList();

            return Ok(despesas);
        }

        // Listar despesas por período (ex: /periodo?inicio=2025-01-01&fim=2025-04-30)
        [HttpGet("periodo")]
        public IActionResult ListarDespesasPorPeriodo(DateTime inicio, DateTime fim)
        {
            var despesas = _context.Despesas
                .Where(d => d.Data >= inicio && d.Data <= fim)
                .Include(d => d.Usuario)
                .ToList();

            return Ok(despesas);
        }

        // Atualizar despesa
        [HttpPut("{id}")]
        public IActionResult EditarDespesa(int id, DespesaModel despesaAtualizada)
        {
            var despesaExistente = _context.Despesas.Find(id);
            if (despesaExistente == null)
                return NotFound("Despesa não encontrada.");

            despesaExistente.Nome = despesaAtualizada.Nome;
            despesaExistente.Valor = despesaAtualizada.Valor;
            despesaExistente.Categoria = despesaAtualizada.Categoria;
            despesaExistente.Data = despesaAtualizada.Data;
            despesaExistente.Descricao = despesaAtualizada.Descricao;
            despesaExistente.UsuarioId = despesaAtualizada.UsuarioId;

            _context.SaveChanges();

            return Ok(despesaExistente);
        }

        // Deletar despesa
        [HttpDelete("{id}")]
        public IActionResult ExcluirDespesa(int id)
        {
            var despesa = _context.Despesas.Find(id);
            if (despesa == null)
                return NotFound("Despesa não encontrada.");

            _context.Despesas.Remove(despesa);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
