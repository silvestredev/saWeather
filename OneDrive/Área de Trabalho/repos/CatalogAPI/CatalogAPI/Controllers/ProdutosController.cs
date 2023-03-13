using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CatalogAPI.Context;
using CatalogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class ProdutosController : ControllerBase
	{
		//instancia da classe context
		private readonly CatalogAPIDbContext _context; 

		//construtor injetando a classe de contexto
		public ProdutosController(CatalogAPIDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public ActionResult<IEnumerable<Produto>> Get() //actionresult é um recurso da api que possibilita diferentes retornos
		{
			//acessando produtos com a variavel de contexto
			var produtos = _context.Produtos.AsNoTracking().ToList(); //AsNoTracking não armazena o retorno no cache, otimizando // só é usado em casos de não alteração, somente leitura
			if (produtos is null)
			{
				return NotFound("Produtos não encontrados...");
			}
			return produtos;
		}

		[HttpGet("{id:int}", Name="ObterProduto")]
		public ActionResult<Produto> GetById(int id) 
		{
			var produto = _context.Produtos.FirstOrDefault(x => x.ProdutoId == id);
			if (produto is null)
			{
				return NotFound("Não há um produto registrado por esse id");
			}
			return produto;
		}

		[HttpPost]
		public ActionResult Post(Produto produto)
		{
			if (produto is null)
			{
				return BadRequest();
			}

			//adicionando produto no contexto e salvando mudanças
			_context.Produtos.Add(produto);
			_context.SaveChanges();

			//informando novo produto e retornando o http 201
			return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
		}

		//fazendo atualização completa do produto
		[HttpPut("{id:int}")]
		public ActionResult Put(int id, Produto produto)
		{
			if (id != produto.ProdutoId)
			{
				return BadRequest();
			}

			_context.Entry(produto).State = EntityState.Modified; //informando que a entidade produto será modificado
			_context.SaveChanges();

			return Ok(produto);
		}

		[HttpDelete("{id:int}")]
		public ActionResult Delete(int id) 
		{
			var produto = _context.Produtos.FirstOrDefault(x => x.ProdutoId == id);

			if (produto is null)
			{
				return NotFound("Produto não encontrado!");
			}

			_context.Produtos.Remove(produto);
			_context.SaveChanges();

			return Ok("Produto removido!");
		}
	}
}
