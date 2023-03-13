using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogAPI.Models;

[Table("Categorias")]
public class Categoria
{
	[Key]
	public int CategoriaId { get; set; }
	[Required]
	[StringLength(80)]
	public string? Nome { get; set; }
	[Required]
	[StringLength(300)]
	public string? ImagemUrl { get; set; }

	//propriedade de navegação
	public ICollection<Produto> Produtos { get; set; } //uma categoria possui uma coleção de produtos

	public Categoria()
	{
		Produtos = new Collection<Produto>(); 
	}

}

