using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogAPI.Migrations
{
    /// <inheritdoc />
    public partial class PopulandoProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
			mb.Sql(
                "INSERT INTO produtos(Nome, Descricao, Preco, ImageUrl, Estoque, DataCadastro, CategoriaId)" +
                " VALUES('Coca-Cola', 'Refrigerante sabor cola 600ml', 5.99, 'cocacola.png', 50, now(), 1)"
                );
			mb.Sql(
				"INSERT INTO produtos(Nome, Descricao, Preco, ImageUrl, Estoque, DataCadastro, CategoriaId)" +
				" VALUES('Sanduíche natural', 'Ingredientes: pão, salame, alface, queijo, tomate', 9.99, 'sanduichenatural.png', 50, now(), 2)"
				);
			mb.Sql(
				"INSERT INTO produtos(Nome, Descricao, Preco, ImageUrl, Estoque, DataCadastro, CategoriaId)" +
				" VALUES('Sorvete Magnum', 'Sabor: chocolante branco', 9.99, 'magnumbranco.png', 50, now(), 3)"
				);
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE * FROM produtos");
        }
    }
}
