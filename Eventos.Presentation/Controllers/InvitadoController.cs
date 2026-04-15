using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvitadoController : ControllerBase
    {
        private readonly InvitadoService _service;

        public InvitadoController(InvitadoService service)
        {
            _service = service;
        }

        [HttpPost("notificar/{id}")]
        public async Task<IActionResult> NotificarInvitado(Guid id)
        {
            var resultado = await _service.EnviarInvitacionWhatsApp(id);
            if (!resultado)
                return BadRequest("Error al procesar el envío del QR");

            return Ok(new {mensaje = "QR enviado exitosament por WhatsApp"});
        }

        [HttpPost("enviar-masivo/{eventoId}")]
        public async Task<IActionResult> EnviarMasivo(Guid eventoId)
        {
            await _service.EnviarTodosPorEvento(eventoId);
            return Ok(new { mensaje = "Envío masivo iniciado. Las invitaciones se enviarán en breve." });
        }

        [HttpGet("evento/{eventoId}")]
        public async Task<IActionResult> ListarInvitados(Guid eventoId)
        {
            var invitados = await _service.ListarInvitadosPorEvento(eventoId);
            return Ok(invitados);
        }

        [HttpPost]
        public async Task<IActionResult> CrearInvitado([FromBody] InvitadoCreateDto dto)
        {
            var resultado = await _service.CrearUsuarioManuel(dto);
            if (!resultado)
                return BadRequest("No se pudo crear el invitado.");
            return Ok("Invitado creado exitosamente.");
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarInvitado([FromBody] InvitadoUpdateDto dto)
        {
            var resultado = await _service.ActualizarDatosPersonales(dto);
            if (!resultado)
                return BadRequest("No se pudo actualizar el invitado.");
            return Ok("Invitado actualizado exitosamente.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarInvitado(Guid id)
        {
            var resultado = await _service.EliminarInvitado(id);
            if (!resultado)
                return BadRequest("No se pudo eliminar el invitado.");
            return Ok("Invitado eliminado exitosamente.");
        }
    }
}
