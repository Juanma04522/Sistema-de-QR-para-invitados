using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;
using Domain.Interfaces;
using System.Runtime.CompilerServices;
using QRCoder;
using System.Security.Principal;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;

namespace Application.Services
{
    public class InvitadoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;

        public InvitadoService(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
        }

        public byte[] GenerarImagenFisicaQR(string contenido)
        {
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(contenido, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new PngByteQRCode(qrCodeData);

            return qrCode.GetGraphic(20);
        }

        public async Task<bool> EnviarInvitacionWhatsApp(Guid invitadoId)
        {
            var invitado = await _unitOfWork.Invitados.ObtenerId(invitadoId);
            if (invitado == null) return false;

            byte[] qrImage = GenerarImagenFisicaQR(invitado.Ticket.CodigoUnico);

            var acc = new Account(
                _config["CloudinarySettings:CloudName"],
                _config["CloudinarySettings:ApiKey"],
                _config["CloudinarySettings:ApiSecret"]
            );

            var cloudinary = new Cloudinary(acc);

            using var stream = new MemoryStream(qrImage);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(invitado.Ticket.CodigoUnico, stream),
                PublicId = $"tiendon/qr_{invitado.Ticket.CodigoUnico}",
                Overwrite = true
            };

            var uploadResult = await cloudinary.UploadAsync(uploadParams);
            string urlPublica = uploadResult.SecureUrl.ToString();

            invitado.Ticket.QrUrl = urlPublica;
            _unitOfWork.Invitados.Update(invitado);
            await _unitOfWork.SaveChangesAsync();

            TwilioClient.Init(_config["TwilioSettings:AccountSid"], _config["TwilioSettings:AuthToken"]);
            await MessageResource.CreateAsync(
                body: $"¡Hola {invitado.Nombre}! 🎉 Presenta este QR en la entrada de TiendOn.",
                mediaUrl: new List<Uri> { new Uri(urlPublica) },
                from: new Twilio.Types.PhoneNumber(_config["TwilioSettings:FromWhatsApp"]),
                to: new Twilio.Types.PhoneNumber($"whatsapp:{invitado.Telefono}")
            );

            return true;
        }

        public async Task EnviarTodosPorEvento(Guid eventoId)
        {
            var invitados = await _unitOfWork.Invitados.ListarInvitados(eventoId);
            
            var invitadosPendientes = invitados.Where(i => i.Estado == EstadoInvitado.Pendiente).ToList();

            foreach (var inv in invitadosPendientes)
            {
                try
                {
                    await EnviarInvitacionWhatsApp(inv.Id);

                    await Task.Delay(1000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al enviar invitación a {inv.Nombre}: {ex.Message}");
                }
            }
        }

        public async Task<bool> CrearUsuarioManuel(InvitadoCreateDto dto)
        {
            string codigo = Guid.NewGuid().ToString().Substring(0,6).ToUpper();

            var nuevoTicket = new Ticket
            {
                CodigoUnico = codigo,
                QrUrl = ""
            };

            var nuevoInvitado = new Invitado
            {
                Nombre = dto.Nombre,
                Telefono = dto.Telefono,
                TotalPersonas = dto.TotalPersonas,
                EventoId = dto.EventoId,
                MesaId = dto.MesaId,
                Estado = EstadoInvitado.Pendiente,
                Ticket = nuevoTicket
            };

            await _unitOfWork.Invitados.Created(nuevoInvitado);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> ActualizarDatosPersonales(InvitadoUpdateDto dto)
        {
            var invitado = await _unitOfWork.Invitados.ObtenerId(dto.Id);
            if (invitado == null) return false;

            if(dto.TotalPersonas < invitado.CuposIngresado)
                return false;

            invitado.Nombre = dto.Nombre;
            invitado.Telefono = dto.Telefone;
            invitado.MesaId = dto.MesaId;
            invitado.TotalPersonas = dto.TotalPersonas;
            invitado.CuposIngresado = dto.CuposIngresado;

            if (invitado.CuposIngresado >= invitado.TotalPersonas)
                invitado.Estado = EstadoInvitado.Completado;
            else if (invitado.CuposIngresado > 0)
                invitado.Estado = EstadoInvitado.Parcial;
            else
                invitado.Estado = EstadoInvitado.Pendiente;

            _unitOfWork.Invitados.Update(invitado);
            return await _unitOfWork.SaveChangesAsync() > 0;

        }

        public async Task<bool> EliminarInvitado(Guid id)
        {
            var invitado = await _unitOfWork.Invitados.ObtenerId(id);
            if(invitado == null) return false;

            await _unitOfWork.Invitados.Delete(invitado);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<List<InvitadoBusquedaDto>> ListarInvitadosPorEvento(Guid eventoId)
        {
            var invitados = await _unitOfWork.Invitados.ListarInvitados(eventoId);

            return invitados.Select(i => new InvitadoBusquedaDto
            {
                Id = i.Id,
                Nombre = i.Nombre,
                Telefono = i.Telefono,
                NumeroMesa = i.Mesa?.NumeroMesa ?? 0,
                TotalPersonas = i.TotalPersonas,
                CuposIngresado = i.CuposIngresado,
                CodigoTicket = i.Ticket.CodigoUnico,
                Estado = i.Estado.ToString(),
                Acompañantes = i.Acompañantes.Select(a => a.Nombre).ToList()
            }).ToList();
        }

        public async Task<List<InvitadoBusquedaDto>> BuscarInvitadosPorNombre(string nombre)
        {
            var invitados = await _unitOfWork.Invitados.ObtenerInvitadoPorNombre(nombre);

            return invitados.Select(i => new InvitadoBusquedaDto
            {
                Id = i.Id,
                Nombre = i.Nombre,
                Telefono = i.Telefono,
                NumeroMesa = i.Mesa?.NumeroMesa ?? 0,
                TotalPersonas = i.TotalPersonas,
                CuposIngresado = i.CuposIngresado,
                CodigoTicket = i.Ticket.CodigoUnico,
                Estado = i.Estado.ToString(),
                Acompañantes = i.Acompañantes.Select(a => a.Nombre).ToList()
            }).ToList();
        }

        public async Task<Invitado> ObtenerPorCodigoTicket(string codigo)
        {
            return await _unitOfWork.Invitados.ObtenerInvitadoConTicket(codigo);
        }

    }
}
