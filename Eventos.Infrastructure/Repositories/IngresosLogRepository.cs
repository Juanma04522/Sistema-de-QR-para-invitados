using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class IngresosLogRepository : IIngresosLogRepository
    {
        private readonly AppDbContext _context;

        public IngresosLogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(IngresosLog log)
        {
            await _context.IngresosLogs.AddAsync(log);
        }

        public async Task<List<IngresosLog>> GetAllAsync() 
        {
            return await _context.IngresosLogs.ToListAsync();
        }

        public async Task<int> SumarIngresosPorEventoAsync(Guid eventoId)
        {
            return await _context.IngresosLogs
                .Where(l => l.Invitado.EventoId == eventoId)
                .SumAsync(l => l.CantidadPersonas); 
        }
    }
}
