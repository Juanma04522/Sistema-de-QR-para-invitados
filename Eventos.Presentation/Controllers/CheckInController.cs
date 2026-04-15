using Microsoft.AspNetCore.Mvc;
using Application.Services;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckInController : ControllerBase
    {
        private readonly CheckInServices _service;
        private readonly InvitadoService _invitadoService;

        public CheckInController(CheckInServices service, InvitadoService invitadoService)
        {
            _service = service;
            _invitadoService = invitadoService;
        }

        [HttpPost("registrar-codigo")]
        public async Task<IActionResult> PorCodigo([FromBody] ScanRequest request)
        {
            var res = await _service.RegistrarPorCodigo(request.QrCode, request.Cantidad);
            return res.Exitoso ? Ok(res) : BadRequest(res);
        }

        [HttpPost("confirmar-manual")]
        public async Task<IActionResult> PorId([FromBody] ManualRequest request)
        {
            var res = await _service.RegistrarPorId(request.InvitadoId, request.Cantidad);
            return res.Exitoso ? Ok(res) : BadRequest(res);
        }

        [HttpGet("buscar-manual")]
        public async Task<IActionResult> Buscar([FromQuery] string nombre)
        {
            var resultados = await _invitadoService.BuscarInvitadosPorNombre(nombre);
            return Ok(resultados);
        }

    }
}

public class ScanRequest
{
    public string QrCode { get; set; } = string.Empty;
    public int Cantidad { get; set; }
}

public class ManualRequest
{
    public Guid InvitadoId { get; set; }
    public int Cantidad { get; set; }
}
