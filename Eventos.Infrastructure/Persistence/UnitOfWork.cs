using Domain.Interfaces;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Persistence;

namespace Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IInvitadoRespoitory Invitados { get; private set; }

        public IIngresosLogRepository IngresosLogs { get; private set; }
        
        public IEventoRepository Eventos { get; private set; }

        public UnitOfWork(AppDbContext context) 
        {
            _context = context;

            Invitados = new InvitadoRespository(_context);
            IngresosLogs = new IngresosLogRepository(_context);
            Eventos = new EventoRepository(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }   
    }
}
