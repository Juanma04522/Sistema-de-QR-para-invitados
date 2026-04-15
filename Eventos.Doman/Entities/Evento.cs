using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Evento
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nombre { get; set; } = string.Empty;
        public string Lugar { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }

        public List<Invitado> Invitados = new List<Invitado>();
    }
}
