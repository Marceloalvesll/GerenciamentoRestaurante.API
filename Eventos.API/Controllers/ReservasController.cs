using Microsoft.AspNetCore.Mvc;
using Eventos.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eventos.API.Controllers
{
    [Route("api/reservas")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private static List<Reserva> _reservas = new List<Reserva>(); // Lista em memória
        private static List<Mesa> _mesas = new List<Mesa>(); // Lista em memória de mesas

        public ReservasController()
        {
            // Inicializando algumas mesas fictícias
            if (!_mesas.Any())
            {
                _mesas.Add(new Mesa { Id = Guid.NewGuid(), Capacidade = 4 });
                _mesas.Add(new Mesa { Id = Guid.NewGuid(), Capacidade = 2 });
            }
        }

        /// <summary>
        /// Criar uma nova reserva.
        /// </summary>
        /// <remarks>
        /// Retorno:
        ///     POST /api/reservas
        ///     {
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "numeroPessoas": 4,
        ///         "mesaId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "confirmada": true,
        ///         "dataReserva": "2024-11-29T12:19:04.123Z"
        ///     }
        /// </remarks>
        /// <response code="201">Retorna a reserva criada</response>
        /// <response code="400">Se a reserva não for válida (mesa não encontrada, capacidade excedida, etc.)</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CriarReserva(ReservaDTO dto)
        {
            var reserva = new Reserva
            {
                Id = Guid.NewGuid(),
                NumeroPessoas = dto.NumeroPessoas,
                MesaId = dto.MesaId,
                Confirmada = dto.Confirmada,
                DataReserva = DateTime.UtcNow // Definir a data e hora da reserva no momento da criação
            };

            var mesa = _mesas.SingleOrDefault(m => m.Id == reserva.MesaId);
            if (mesa == null)
            {
                return NotFound("Mesa não encontrada.");
            }

            if (reserva.NumeroPessoas > mesa.Capacidade)
            {
                return BadRequest("Número de pessoas excede a capacidade da mesa.");
            }

            // Verificar se já existe uma reserva para a mesma mesa e horário
            var reservaExistente = _reservas
                .Any(r => r.MesaId == reserva.MesaId && r.DataReserva == reserva.DataReserva);
            if (reservaExistente)
            {
                return BadRequest("Mesa já reservada para o horário.");
            }

            _reservas.Add(reserva); // Adiciona a reserva à lista em memória
            return CreatedAtAction(nameof(GetReservaById), new { id = reserva.Id }, reserva);
        }

        /// <summary>
        /// Obter uma reserva por ID.
        /// </summary>
        /// <param name="id">ID da reserva</param>
        /// <remarks>
        /// Retorno:
        ///     GET /api/reservas/{id}
        ///     {
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "numeroPessoas": 4,
        ///         "mesaId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "confirmada": true,
        ///         "dataReserva": "2024-11-29T12:19:04.123Z"
        ///     }
        /// </remarks>
        /// <response code="200">Retorna a reserva encontrada</response>
        /// <response code="404">Se a reserva não for encontrada</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetReservaById(Guid id)
        {
            var reserva = _reservas.SingleOrDefault(r => r.Id == id);
            if (reserva == null)
            {
                return NotFound("Reserva não encontrada.");
            }
            return Ok(reserva);
        }

        /// <summary>
        /// Atualizar uma reserva existente.
        /// </summary>
        /// <param name="id">ID da reserva a ser atualizada</param>
        /// <param name="dto">Objeto com os dados atualizados</param>
        /// <remarks>
        /// Retorno:
        ///     PUT /api/reservas/{id}
        ///     {
        ///         "numeroPessoas": 4,
        ///         "mesaId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "confirmada": true
        ///     }
        /// </remarks>
        /// <response code="204">Reserva atualizada com sucesso</response>
        /// <response code="400">Se a reserva não for válida (mesa não encontrada, capacidade excedida, etc.)</response>
        /// <response code="404">Se a reserva não for encontrada</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AtualizarReserva(Guid id, ReservaDTO dto)
        {
            var reservaExistente = _reservas.SingleOrDefault(r => r.Id == id);
            if (reservaExistente == null)
            {
                return NotFound("Reserva não encontrada.");
            }

            var mesa = _mesas.SingleOrDefault(m => m.Id == dto.MesaId);
            if (mesa == null)
            {
                return NotFound("Mesa não encontrada.");
            }

            if (dto.NumeroPessoas > mesa.Capacidade)
            {
                return BadRequest("Número de pessoas excede a capacidade da mesa.");
            }

            // Atualiza os dados da reserva
            reservaExistente.NumeroPessoas = dto.NumeroPessoas;
            reservaExistente.MesaId = dto.MesaId;
            reservaExistente.Confirmada = dto.Confirmada;

            return NoContent();  // Retorna 204 No Content em caso de sucesso
        }

        /// <summary>
        /// Listar todas as reservas.
        /// </summary>
        /// <remarks>
        /// Retorno:
        ///     GET /api/reservas
        ///     [
        ///         {
        ///             "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///             "numeroPessoas": 4,
        ///             "mesaId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///             "confirmada": true,
        ///             "dataReserva": "2024-11-29T12:19:04.123Z"
        ///         }
        ///     ]
        /// </remarks>
        /// <response code="200">Retorna todas as reservas cadastradas</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetReservas()
        {
            return Ok(_reservas);
        }

        /// <summary>
        /// Deletar uma reserva existente.
        /// </summary>
        /// <param name="id">ID da reserva a ser deletada</param>
        /// <remarks>
        /// Retorno:
        ///     DELETE /api/reservas/{id}
        ///     Status 204 No Content
        /// </remarks>
        /// <response code="204">Reserva deletada com sucesso</response>
        /// <response code="404">Se a reserva não for encontrada</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeletarReserva(Guid id)
        {
            var reservaExistente = _reservas.SingleOrDefault(r => r.Id == id);
            if (reservaExistente == null)
            {
                return NotFound("Reserva não encontrada.");
            }

            _reservas.Remove(reservaExistente); // Remove da lista em memória
            return NoContent();  // Retorna 204 No Content em caso de sucesso
        }
    }
}
