using Microsoft.AspNetCore.Mvc;
using Eventos.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eventos.API.Controllers
{
    [Route("api/pedidos")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private static List<Pedido> _pedidos = new List<Pedido>(); // Lista em memória

        /// <summary>
        /// Criar um novo pedido com itens.
        /// </summary>
        /// <remarks>
        /// Retorno:
        ///     POST /api/pedidos
        ///     {
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "itens": [
        ///             {
        ///                 "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "nome": "string",
        ///                 "preco": 10.0,
        ///                 "quantidade": 2
        ///             }
        ///         ],
        ///         "total": 20.0
        ///     }
        /// </remarks>
        /// <response code="201">Retorna o pedido recém-criado</response>
        /// <response code="400">Se houver erro na criação do pedido</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CriarPedido(Pedido pedido)
        {
            pedido.Id = Guid.NewGuid();

            // Calcular o total
            decimal total = 0;
            foreach (var item in pedido.Itens)
            {
                total += item.Preco * item.Quantidade;  // Preço * Quantidade de cada item
                item.Id = Guid.NewGuid();  // Gerando ID único para cada item
            }

            pedido.Total = total;  // Atribuindo o valor total ao pedido

            _pedidos.Add(pedido); // Adiciona o pedido à lista em memória
            return CreatedAtAction(nameof(GetPedidoById), new { id = pedido.Id }, pedido);
        }

        /// <summary>
        /// Listar todos os pedidos.
        /// </summary>
        /// <remarks>
        /// Retorno:
        ///     GET /api/pedidos
        ///     [
        ///         {
        ///             "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///             "total": 20.0,
        ///             "itens": [...]
        ///         }
        ///     ]
        /// </remarks>
        /// <response code="200">Retorna todos os pedidos cadastrados</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetPedidos()
        {
            return Ok(_pedidos);
        }

        /// <summary>
        /// Obter pedido por ID.
        /// </summary>
        /// <param name="id">ID do pedido</param>
        /// <remarks>
        /// Retorno:
        ///     GET /api/pedidos/{id}
        ///     {
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "total": 20.0,
        ///         "itens": [...]
        ///     }
        /// </remarks>
        /// <response code="200">Retorna o pedido solicitado</response>
        /// <response code="404">Se o pedido não for encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPedidoById(Guid id)
        {
            var pedido = _pedidos.SingleOrDefault(p => p.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }

        /// <summary>
        /// Cancelar pedido.
        /// </summary>
        /// <param name="id">ID do pedido a ser cancelado</param>
        /// <remarks>
        /// Retorno:
        ///     POST /api/pedidos/{id}/cancelar
        ///     Status 204 No Content
        /// </remarks>
        /// <response code="204">Pedido cancelado com sucesso</response>
        /// <response code="404">Se o pedido não for encontrado</response>
        [HttpPost("{id}/cancelar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult CancelarPedido(Guid id)
        {
            var pedido = _pedidos.SingleOrDefault(p => p.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }
            pedido.Cancelado = true;
            return NoContent();  // Não há necessidade de SaveChanges, já foi alterado na lista
        }

        /// <summary>
        /// Atualizar pedido.
        /// </summary>
        /// <param name="id">ID do pedido a ser atualizado</param>
        /// <param name="pedidoAtualizado">Dados atualizados do pedido</param>
        /// <remarks>
        /// Retorno:
        ///     PUT /api/pedidos/{id}
        ///     {
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "itens": [...],
        ///         "total": 30.0
        ///     }
        /// </remarks>
        /// <response code="200">Retorna o pedido atualizado</response>
        /// <response code="404">Se o pedido não for encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AtualizarPedido(Guid id, Pedido pedidoAtualizado)
        {
            var pedidoExistente = _pedidos.SingleOrDefault(p => p.Id == id);
            if (pedidoExistente == null)
            {
                return NotFound("Pedido não encontrado.");
            }

            // Atualizar os campos do pedido
            pedidoExistente.DataHora = pedidoAtualizado.DataHora;
            pedidoExistente.Confirmado = pedidoAtualizado.Confirmado;
            pedidoExistente.Cancelado = pedidoAtualizado.Cancelado;

            // Atualizar os itens do pedido
            foreach (var itemAtualizado in pedidoAtualizado.Itens)
            {
                var itemExistente = pedidoExistente.Itens.SingleOrDefault(i => i.Id == itemAtualizado.Id);
                if (itemExistente != null)
                {
                    // Atualizar os itens
                    itemExistente.Nome = itemAtualizado.Nome;
                    itemExistente.Preco = itemAtualizado.Preco;
                    itemExistente.Quantidade = itemAtualizado.Quantidade;
                }
                else
                {
                    // Adicionar novos itens se não existirem
                    itemAtualizado.Id = Guid.NewGuid();
                    pedidoExistente.Itens.Add(itemAtualizado);
                }
            }

            // Recalcular o total
            pedidoExistente.Total = pedidoExistente.Itens.Sum(i => i.Preco * i.Quantidade);

            return Ok(pedidoExistente);  // Retorna o pedido atualizado
        }

        /// <summary>
        /// Excluir pedido.
        /// </summary>
        /// <param name="id">ID do pedido a ser excluído</param>
        /// <remarks>
        /// Retorno:
        ///     DELETE /api/pedidos/{id}
        ///     Status 204 No Content
        /// </remarks>
        /// <response code="204">Pedido excluído com sucesso</response>
        /// <response code="404">Se o pedido não for encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeletarPedido(Guid id)
        {
            var pedido = _pedidos.SingleOrDefault(p => p.Id == id);
            if (pedido == null)
            {
                return NotFound("Pedido não encontrado.");
            }

            _pedidos.Remove(pedido); // Remove o pedido da lista em memória
            return NoContent();  // Retorna 204 No Content
        }
    }
}
