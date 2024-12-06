using Eventos.API.Entities;
using Eventos.API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eventos.API.Controllers
{
    [Route("api/dev-events")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class DevEventsController : ControllerBase
    {
        private readonly DevEventsDbContext _context;

        public DevEventsController(DevEventsDbContext context)
        {

            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Retorno:
        ///     
        /// 
        ///     GET
        ///     {
        ///             {       
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "title": "string",
        ///         "description": "string",
        ///         "startDate": "2024-11-29T12:19:04.123Z",
        ///         "endDate": "2024-11-29T12:19:04.123Z",
        ///         "speakers": [
        ///         {
        ///             "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///             "nome": "string",
        ///             "talkTitle": "string",
        ///             "talkDescription": "string",
        ///             "linkedinProfile": "string"
        ///     }
        ///     ],
        ///     "isDeleted": true
        ///          }
        ///     }
        /// 
        /// </remarks>>       
        /// <response code="200"> Retorna um IEnumerable de eventos cadastrados</response>
        /// <response code="400"> Se encontrar um erro.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAll()
        {
            var devEvents = _context.DevEvents.Where(d => !d.IsDeleted).ToList();
            return Ok(devEvents);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var devEvent = _context.DevEvents.SingleOrDefault(d => d.Id == id);
            if (devEvent == null)
            {
                return NotFound();
            }
            return Ok(devEvent);
        }

        [HttpPost("{id}/speakers")]
        public IActionResult PostSpeaker(Guid id, DevEventSpeakers speaker)
        {
            var devEvent = _context.DevEvents.SingleOrDefault(d => d.Id == id);
            if (devEvent == null)
            {
                return NotFound();
            }

            devEvent.Speakers.Add(speaker);

            return NoContent();
        }



        [HttpPost]
        public IActionResult Post(DevEvent devEvent)
        {

            _context.DevEvents.Add(devEvent);
            return CreatedAtAction(nameof(GetById), new { id = devEvent.Id }, devEvent);
        }




    }
}