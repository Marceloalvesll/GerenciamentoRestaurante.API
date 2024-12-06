using Microsoft.AspNetCore.Mvc;
using Eventos.API.Entities;
using Eventos.API.Persistence;

namespace Restaurante.API.Controllers
{
    [Route("api/itens-pedido")]
    [ApiController]
    public class ItemPedidoController : ControllerBase
    {
        private readonly RestauranteDbContext _context;

        public ItemPedidoController(RestauranteDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Criar um item de pedido.
        /// </summary>
        /// <remarks>
        /// Retorno:
        ///     POST /api/itens-pedido
        ///     {
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "nome": "string",
        ///         "preco": 10.0,
        ///         "quantidade": 2
        ///     }
        /// </remarks>
        /// <response code="201">Retorna o item de pedido recém-criado</response>
        /// <response code="400">Se houver erro na criação do item de pedido</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CriarItemPedido(ItemPedido itemPedido)
        {
            itemPedido.Id = Guid.NewGuid();
            _context.ItensPedido.Add(itemPedido);
            return CreatedAtAction(nameof(GetItemPedidoById), new { id = itemPedido.Id }, itemPedido);
        }

        /// <summary>
        /// Obter item de pedido por ID.
        /// </summary>
        /// <param name="id">ID do item de pedido</param>
        /// <response code="200">Retorna o item de pedido</response>
        /// <response code="404">Se o item de pedido não for encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetItemPedidoById(Guid id)
        {
            var itemPedido = _context.ItensPedido.SingleOrDefault(i => i.Id == id);
            if (itemPedido == null)
            {
                return NotFound("Item de pedido não encontrado.");
            }
            return Ok(itemPedido);
        }

        /// <summary>
        /// Atualizar item de pedido.
        /// </summary>
        /// <param name="id">ID do item de pedido a ser atualizado</param>
        /// <param name="itemPedido">Dados atualizados do item de pedido</param>
        /// <response code="204">Atualização bem-sucedida</response>
        /// <response code="404">Se o item de pedido não for encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AtualizarItemPedido(Guid id, ItemPedido itemPedido)
        {
            var itemExistente = _context.ItensPedido.SingleOrDefault(i => i.Id == id);
            if (itemExistente == null)
            {
                return NotFound("Item de pedido não encontrado.");
            }
            itemExistente.Nome = itemPedido.Nome;
            itemExistente.Preco = itemPedido.Preco;
            itemExistente.Quantidade = itemPedido.Quantidade;
            return NoContent();
        }

        /// <summary>
        /// Listar todos os itens de pedido.
        /// </summary>
        /// <response code="200">Retorna todos os itens de pedido cadastrados</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetItensDePedido()
        {
            return Ok(_context.ItensPedido);
        }

        /// <summary>
        /// Deletar item de pedido.
        /// </summary>
        /// <param name="id">ID do item de pedido a ser deletado</param>
        /// <response code="204">Item de pedido excluído com sucesso</response>
        /// <response code="404">Se o item de pedido não for encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeletarItemPedido(Guid id)
        {
            var itemPedido = _context.ItensPedido.SingleOrDefault(i => i.Id == id);
            if (itemPedido == null)
            {
                return NotFound("Item de pedido não encontrado.");
            }
            _context.ItensPedido.Remove(itemPedido);
            return NoContent();
        }
    }
}
