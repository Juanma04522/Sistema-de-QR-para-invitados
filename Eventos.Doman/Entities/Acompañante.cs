using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Acompañante
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public Guid InvitadoId { get; set; }
        public Invitado Invitado { get; set; } = null!;
    }
}
