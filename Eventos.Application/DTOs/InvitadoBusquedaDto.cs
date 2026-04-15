using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class InvitadoBusquedaDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public int NumeroMesa { get; set; }
        public int TotalPersonas { get; set; }
        public int CuposIngresado { get; set; }
        public string CodigoTicket { get; set; }
        public string Estado { get; set; }
        public List<string> Acompañantes { get; set; } = new List<string>();
    }
}
