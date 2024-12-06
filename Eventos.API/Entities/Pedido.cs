namespace Eventos.API.Entities
{
    public class Pedido
    {
 
            public Guid Id { get; set; }
            public List<ItemPedido> Itens { get; set; }  // Relacionamento com os itens do pedido
            public decimal Total { get; set; }  // Valor total do pedido
            public DateTime DataHora { get; set; }  // Data e hora do pedido
            public bool Confirmado { get; set; }  // Indica se o pedido foi confirmado
            public bool Cancelado { get; set; }  // Indica se o pedido foi cancelado
        }

        public class ItemPedido
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }  // Quantidade de itens no pedido
    }

    public class ItemPedidoDTO
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
    }
}
