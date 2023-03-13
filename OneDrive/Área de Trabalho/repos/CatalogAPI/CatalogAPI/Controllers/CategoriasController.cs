using CatalogAPI.Context;
using CatalogAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class CategoriasController : Controller
	{
		private readonly CatalogAPIDbContext _context;

		public CategoriasController(CatalogAPIDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public ActionResult<IEnumerable<Categoria>> Get()
		{
			var categorias = _context.Categorias.AsNoTracking();
			if(categorias is null)
			{
				return NotFound("Não há categorias cadastradas!");
			}

			try
			{
				return Ok(categorias);
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Ocorreu um problema ao tratar a sua solicitação!");
			}
		}

		[HttpGet("{id:int}", Name = "ObterCategoria")]
		public ActionResult GetById(int id) 
		{
			var categoria = _context.Categorias.Find(id);
			if (categoria is null)
			{
				return NotFound("Categoria não encontrada!");
			}

			try
			{
				return Ok(categoria);
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Ocorreu um problema ao tratar a sua solicitação!");
			}
		}

		[HttpGet("Produtos")]
		public ActionResult<IEnumerable<Categoria>> GetCategoriaProdutos() 
		{
			try
			{
				return _context.Categorias.Include(p => p.Produtos).Where(c => c.CategoriaId <= 5).ToList(); //nunca retornar todo o conteúdo livremente, sempre aplicar um filtro
			}
			catch (Exception) 
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Ocorreu um problema ao tratar a sua solicitação!");
			}
		}

		[HttpPost]
		public ActionResult Post (Categoria categoria)
		{
			if (categoria is null)
			{
				return BadRequest();
			}

			try
			{
				_context.Categorias.Add(categoria);
				_context.SaveChanges();

				return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
			}
			catch(Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Ocorreu um problema ao tratar a sua solicitação!");
			}
		}

		[HttpPut("{id:int}")]
		public ActionResult Put(int id, Categoria categoria)
		{
			if (id != categoria.CategoriaId)
			{
				return BadRequest();
			}

			try
			{
				_context.Entry(categoria).State = EntityState.Modified;
				_context.SaveChanges();

				return Ok(categoria);
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Ocorreu um problema ao tratar a sua solicitação!");
			}
		}

		[HttpDelete("{id:int}")]
		public ActionResult Delete(int id) 
		{
			var categoria = _context.Categorias.Find(id);
			if (categoria == null)
			{
				return NotFound("O produto não existe!");
			}

			try
			{
				_context.Categorias.Remove(categoria);
				_context.SaveChanges();

				return Ok("Categoria removida!");
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Ocorreu um problema ao tratar a sua solicitação!");
			}
		}
	}
}
