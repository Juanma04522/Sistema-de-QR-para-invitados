using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class IngresosLog
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public int CantidadPersonas { get; set; }

        public Guid InvitadoId { get; set; }
        public Invitado Invitado { get; set; } = null!;
    }
}
