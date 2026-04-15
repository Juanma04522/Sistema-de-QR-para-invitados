using Domain.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Application.DTOs;
using Domain.Enums;

namespace Infrastructure.Repositories
{
    public class InvitadoRespository : IInvitadoRespoitory
    {
        private readonly AppDbContext _context;

        public InvitadoRespository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Invitado>> ListarInvitados(Guid eventoId)
        {
            return await _context.Invitados
                .Where(i => i.EventoId == eventoId && i.Estado != EstadoInvitado.Cancelado)
                .ToListAsync();
        }

        public async Task<Invitado?> ObtenerInvitadoConTicket(string code)
        {
            return await _context.Invitados
                .Include(i => i.Ticket)
                .Include(i => i.Mesa)
                .Include(i => i.Acompañantes)
                .FirstOrDefaultAsync(i => i.Ticket.CodigoUnico == code);
        }

        public async Task<Invitado> ObtenerId(Guid id)
        {
            return await _context.Invitados
                .Include(i => i.Ticket)
                .Include(i => i.Mesa)
                .FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<Invitado> Created(Invitado invitado)
        {
            await _context.Invitados.AddAsync(invitado);
            return invitado;
        }

        public async Task Update(Invitado invitado)
        {
            _context.Invitados.Update(invitado);
        }

        public async Task Delete(Invitado invitado)
        {
            _context.Invitados.Remove(invitado);
        }

        public async Task<List<Invitado>> ObtenerInvitadoPorNombre(string nombre)
        {
            return await _context.Invitados
                .Include(i => i.Ticket)
                .Include(i => i.Mesa)
                .Include(i => i.Acompañantes)
                .Where(i => i.Nombre.ToLower().Contains(nombre.ToLower())
                            || i.Acompañantes.Any(a => a.Nombre.ToLower().Contains(nombre.ToLower())))
                .ToListAsync();

        }

        public async Task<int> SumarTotalEsperado(Guid eventoId)
        {
            return await _context.Invitados
                .Where(i => i.EventoId == eventoId && i.Estado != Domain.Enums.EstadoInvitado.Cancelado)
                .SumAsync(i => i.TotalPersonas);
        }
    }
}
