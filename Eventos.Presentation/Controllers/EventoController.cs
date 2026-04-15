using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class EventoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] EventoCreateDto dto)
        {
            var nuevoEvento = new Evento
            {
                Id = Guid.NewGuid(),
                Nombre = dto.Nombre,
                Fecha = dto.Fecha,
                Lugar = dto.Lugar
            };

            await _unitOfWork.Eventos.AddAsync(nuevoEvento);
            await _unitOfWork.SaveChangesAsync();

            return Ok(nuevoEvento.Id);
        }

    }
}
