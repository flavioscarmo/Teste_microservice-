using Goooapp.Vendas.Teste.API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Goooapp.Vendas.Teste.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VendasController : ControllerBase
    {
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult GetVendas()
        {
            return Ok(LstVendas());
        }

        [Authorize(Roles = "admin, user")]
        [HttpGet("/Produtos")]
        public IActionResult GetProdutos()
        {
            return Ok(LstProdutos());
        }

        private List<Produto> LstProdutos()
        {
            List<Produto> produtos = new List<Produto>();

            produtos.Add(new Produto()
            {
                ProdutoId = 1,
                Preço = 6.99M,
                Descricao = "Arroz",
                Unidade = "Kilo"
            });

            produtos.Add(new Produto()
            {
                ProdutoId = 2,
                Preço = 4.35M,
                Descricao = "Feijão",
                Unidade = "Kilo"
            });

            produtos.Add(new Produto()
            {
                ProdutoId = 2,
                Preço = 7.99M,
                Descricao = "Coca Cola",
                Unidade = "Litro"
            });


            produtos.Add(new Produto()
            {
                ProdutoId = 3,
                Preço = 5.99M,
                Descricao = "Guaraná Antartica",
                Unidade = "Litro"
            });

            return produtos;

        }
        private List<Venda> LstVendas()
        {
            var lstVendas = new List<Venda>();

            lstVendas.Add(
            new Venda()
            {
                Data = new DateTime(2022, 02, 01, 11, 21, 00),
                Valor = 292.69M,
                Vendedor = new Vendedor()
                {
                    IdVendedor = 1,
                    Nome = "Antônio"
                }
            });
            lstVendas.Add(
             new Venda()
             {
                 Data = new DateTime(2022, 02, 01, 11, 20, 00),
                 Valor = 892.69M,
                 Vendedor = new Vendedor()
                 {
                     IdVendedor = 1,
                     Nome = "Maria"
                 }
             }
            );
            lstVendas.Add(
            new Venda()
            {
                Data = new DateTime(2022, 02, 01, 11, 20, 00),
                Valor = 892.69M,
                Vendedor = new Vendedor()
                {
                    IdVendedor = 3,
                    Nome = "Pedro"
                }
            }
           );

            return lstVendas;


        }
    }
}
