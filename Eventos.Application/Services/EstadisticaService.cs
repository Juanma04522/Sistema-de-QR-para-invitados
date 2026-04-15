using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Enums; 

namespace Application.Services
{
    public class EstadisticaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EstadisticaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<EstadisticasDto> ObtenerResumenEvento(Guid eventoId)
        {
            var totalEsperado = await _unitOfWork.Invitados.SumarTotalEsperado(eventoId);

            int totalIngresado = await _unitOfWork.IngresosLogs.SumarIngresosPorEventoAsync(eventoId);

            int pendientes = totalEsperado - totalIngresado;

            return new EstadisticasDto
            {
                TotalEsperado = totalEsperado,
                TotalIngresado = totalIngresado,
                Pendientes = pendientes < 0 ? 0 : pendientes
            };

        }
    }
}
