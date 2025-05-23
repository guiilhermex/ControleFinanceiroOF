using ControleFinanceiroOF.Data;
using ControleFinanceiroOF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ControleFinanceiroOF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<ProdutoModel>> BuscarProdutos()
        {
            var produtos = _context.Produtos.ToList();
            return Ok(produtos);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<ProdutoModel> BuscarProdutoPorId(int id)
        {
            var produto = _context.Produtos.Find(id);

            if (produto==null)
            {
                return NotFound("Registro não localizado");
            }
            return Ok(produto);
        }
        [HttpPost]
        public ActionResult<ProdutoModel> CriarProduto(ProdutoModel produtoModel)
        {
            if (produtoModel==null)
            {
                return BadRequest("ocorreu um erro na solicitação!");
            }
            _context.Produtos.Add(produtoModel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(BuscarProdutoPorId), new { id = produtoModel.Id }, produtoModel);
        }
        [HttpPut]
        [Route("{id}")]
        public ActionResult<ProdutoModel> EditarProduto(ProdutoModel produtoModel, int id)
        {
            var produto = _context.Produtos.Find(id);
            if (produto==null)
            {
                return NotFound("registro nao encontrado!");
            }

            produto.Nome = produtoModel.Nome;
            produto.Descricao = produtoModel.Descricao;
            produto.CodigoDeBarras= produtoModel.CodigoDeBarras;
            produto.QuantidadeEstoque= produtoModel.QuantidadeEstoque;
            produto.Marca= produtoModel.Marca;
            _context.Produtos.Update(produto);
            _context.SaveChanges();

            return NoContent();
        }
        [HttpDelete]
        [Route("{id}")]
        public ActionResult<ProdutoModel> DeletarProduto( int id)
        {
            var produto = _context.Produtos.Find(id);
            if (produto==null)
            {
                return NotFound("registro nao encontrado");
            }
            _context.Produtos.Remove(produto);
            _context.SaveChanges();
            return NoContent();
        }
    }
}

