using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Mesa
    {
        public int Id { get; set; }
        public int NumeroMesa { get; set; }
        public int CantidadSillas { get; set; }

        public Guid EventoId { get; set; }
        public Evento Eventos { get; set; }
        public List<Invitado> Invitados { get; set; } = new List<Invitado>();
    }
}
