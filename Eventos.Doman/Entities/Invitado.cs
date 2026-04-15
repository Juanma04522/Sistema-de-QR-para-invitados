using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public class Invitado
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nombre { get; set; } = string.Empty;  
        public string Telefono { get; set; } = string.Empty;
        public int TotalPersonas { get; set; }
        public List<Acompañante> Acompañantes { get; set; } = new List<Acompañante>();
        public int CuposIngresado { get; set; }
        public EstadoInvitado Estado { get; set; } = EstadoInvitado.Pendiente;
        public Guid EventoId { get; set; }
        public Evento Evento { get; set; } = null!;
        public int MesaId { get; set; }
        public Mesa Mesa { get; set; }
        public Guid TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public int IngresosLogId { get; set; }
        public List<IngresosLog> IngresosLogs { get; set; } = new List<IngresosLog>();

    }
}
