namespace Eventos.API.Entities
{
    public class Reserva
    {
        public Guid Id { get; set; }
        public DateTime DataReserva { get; set; }  // Data e hora da reserva
        public int NumeroPessoas { get; set; }  // Número de pessoas na reserva
        public Guid MesaId { get; set; }  // Id da mesa associada
        public bool Confirmada { get; set; }  // Se a reserva foi confirmada
    }

    public class ReservaDTO
    {
        public int NumeroPessoas { get; set; }  // Número de pessoas na reserva
        public Guid MesaId { get; set; }  // Id da mesa associada
        public bool Confirmada { get; set; }
    }

    public class Mesa
    {
        public Guid Id { get; set; }
        public int Capacidade { get; set; }  // Número de cadeiras da mesa
    }

    public class MesaDTO
    {
        public int Capacidade { get; set; }  // Número de cadeiras da mesa
    }
}
