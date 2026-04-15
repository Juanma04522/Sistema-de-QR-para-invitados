
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Enums;

namespace Application.Services
{
    public class CheckInServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public CheckInServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private string LimpiarCodigo(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            return input.Contains("/") ? input.Split('/').Last() : input.Trim();
        }

        public async Task<ChechInResponseDto> RegistrarPorCodigo(string input, int cantidad)
        {
            string codigoLimpio = LimpiarCodigo(input);
            var invitado = await _unitOfWork.Invitados.ObtenerInvitadoConTicket(codigoLimpio);

            if(invitado == null)
                return new ChechInResponseDto { Exitoso = false, Mensaje = "Código no válido." };

            return await ProcesarIngresoFinal(invitado, cantidad);
        }

        public async Task<ChechInResponseDto> RegistrarPorId(Guid id, int cantidad)
        {
            var invitado = await _unitOfWork.Invitados.ObtenerId(id);

            if (invitado == null)
                return new ChechInResponseDto { Exitoso = false, Mensaje = "Invitado no encontrado." };

            return await ProcesarIngresoFinal(invitado, cantidad);
        }

        private async Task<ChechInResponseDto> ProcesarIngresoFinal(Invitado invitado, int cantidad)
        {
            if(invitado.Estado == EstadoInvitado.Completado)
                return new ChechInResponseDto { Exitoso = false, Mensaje = "El cupo ya está completo." };

            int cuposDisponibles = invitado.TotalPersonas - invitado.CuposIngresado;
            if (cantidad > cuposDisponibles)
                return new ChechInResponseDto { Exitoso = false, Mensaje = $"Solo quedan {cuposDisponibles} cupos." };

            invitado.CuposIngresado += cantidad;
            invitado.Estado = (invitado.CuposIngresado == invitado.TotalPersonas) ? EstadoInvitado.Completado : EstadoInvitado.Parcial;

            var log = new IngresosLog
            {
                InvitadoId = invitado.Id,
                CantidadPersonas = cantidad,
                FechaHora = DateTime.UtcNow
            };

            await _unitOfWork.IngresosLogs.AddAsync(log);
            _unitOfWork.Invitados.Update(invitado);
            await _unitOfWork.SaveChangesAsync();

            return new ChechInResponseDto
            {
                Exitoso = true,
                NombreInvitado = invitado.Nombre,
                EstadoActual = invitado.Estado.ToString(),
                CuposRestantes = invitado.TotalPersonas - invitado.CuposIngresado,
                Mensaje = "Ingreso registrado exitosamente."
            };
        }
    }
}
