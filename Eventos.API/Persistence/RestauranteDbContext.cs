using Eventos.API.Entities;

namespace Eventos.API.Persistence
{
    public class RestauranteDbContext
    {
        public List<Pedido> Pedidos { get; set; }
        public List<Reserva> Reservas { get; set; }
        public List<Mesa> Mesas { get; set; }
        public List<ItemPedido> ItensPedido { get; set; }  // Adicionando lista de itens

        public RestauranteDbContext()
        {
            Pedidos = new List<Pedido>();
            Reservas = new List<Reserva>();
            Mesas = new List<Mesa>();
            ItensPedido = new List<ItemPedido>();  // Inicializando lista de itens
        }
    }
}
