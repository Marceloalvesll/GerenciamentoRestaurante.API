using Microsoft.AspNetCore.Mvc;
using Eventos.API.Entities;
using Eventos.API.Persistence;

namespace Restaurante.API.Controllers
{
    [Route("api/mesas")]
    [ApiController]
    public class MesasController : ControllerBase
    {
        private readonly RestauranteDbContext _context;

        public MesasController(RestauranteDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Criar uma nova mesa.
        /// </summary>
        /// <remarks>
        /// Retorno:
        ///     POST /api/mesas
        ///     {
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "capacidade": 4
        ///     }
        /// </remarks>
        /// <response code="201">Retorna a mesa criada</response>
        /// <response code="400">Se houver erro na criação da mesa</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CriarMesa(MesaDTO dto)
        {
            var mesa = new Mesa();
            mesa.Capacidade = dto.Capacidade;
            mesa.Id = Guid.NewGuid();
            _context.Mesas.Add(mesa);
            return CreatedAtAction(nameof(GetMesaById), new { id = mesa.Id }, mesa);
        }

        /// <summary>
        /// Obter mesa por ID.
        /// </summary>
        /// <param name="id">ID da mesa</param>
        /// <response code="200">Retorna a mesa</response>
        /// <response code="404">Se a mesa não for encontrada</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetMesaById(Guid id)
        {
            var mesa = _context.Mesas.SingleOrDefault(m => m.Id == id);
            if (mesa == null)
            {
                return NotFound("Mesa não encontrada.");
            }
            return Ok(mesa);
        }

        /// <summary>
        /// Atualizar mesa.
        /// </summary>
        /// <param name="id">ID da mesa a ser atualizada</param>
        /// <param name="mesa">Dados atualizados da mesa</param>
        /// <response code="204">Atualização bem-sucedida</response>
        /// <response code="404">Se a mesa não for encontrada</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AtualizarMesa(Guid id, Mesa mesa)
        {
            var mesaExistente = _context.Mesas.SingleOrDefault(m => m.Id == id);
            if (mesaExistente == null)
            {
                return NotFound("Mesa não encontrada.");
            }
            mesaExistente.Capacidade = mesa.Capacidade;  // Atualiza os dados
            return NoContent();
        }

        /// <summary>
        /// Listar todas as mesas.
        /// </summary>
        /// <response code="200">Retorna todas as mesas cadastradas</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetMesas()
        {
            return Ok(_context.Mesas);
        }

        /// <summary>
        /// Deletar mesa.
        /// </summary>
        /// <param name="id">ID da mesa a ser deletada</param>
        /// <response code="204">Mesa excluída com sucesso</response>
        /// <response code="404">Se a mesa não for encontrada</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeletarMesa(Guid id)
        {
            var mesa = _context.Mesas.SingleOrDefault(m => m.Id == id);
            if (mesa == null)
            {
                return NotFound("Mesa não encontrada.");
            }
            _context.Mesas.Remove(mesa);
            return NoContent();
        }
    }
}
