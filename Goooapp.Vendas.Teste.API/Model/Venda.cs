namespace Goooapp.Vendas.Teste.API.Model
{
    public class Venda
    {
        public int IdVendas { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public Vendedor Vendedor { get; set; }
    }
}
