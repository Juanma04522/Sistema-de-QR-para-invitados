using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IEventoRepository
    {
        Task AddAsync(Evento evento);
    }
}
