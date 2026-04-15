using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Ticket
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CodigoUnico { get; set; } = Guid.NewGuid().ToString();
        public string QrUrl { get; set; } = string.Empty;
        public bool EstaEscaneado { get; set; } = false;
        public DateTime FechaEscaneado { get; set; }

        public Guid InvitadoId { get; set; }
        public Invitado Invitado { get; set; } = null!;
    }
}
