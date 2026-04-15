using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IInvitadoRespoitory
    {
        Task<List<Invitado>> ListarInvitados(Guid eventoId);
        Task<Invitado?> ObtenerInvitadoConTicket(string code);
        Task<Invitado> ObtenerId(Guid id);
        Task<List<Invitado>> ObtenerInvitadoPorNombre(string nombre);
        Task<Invitado> Created(Invitado invitado);
        Task Update(Invitado invitado);
        Task Delete(Invitado invitado);
        Task<int> SumarTotalEsperado(Guid eventoId);
    }
}
