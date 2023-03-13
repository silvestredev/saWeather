using CatalogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Context;
public class CatalogAPIDbContext : DbContext
{
	//configurando contexto no entityframework, para podermos criar e acessar a base de dados -> essa config é feita na classe base (DbContext)
	public CatalogAPIDbContext(DbContextOptions<CatalogAPIDbContext> options)
		: base(options)
	{

	}

	//mapeando as entidades
	public DbSet<Categoria> Categorias { get; set; }
	public DbSet<Produto> Produtos { get; set; }
}
