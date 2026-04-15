using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ChechInResponseDto
    {
        public string NombreInvitado { get; set; } = string.Empty;
        public int NumeroMesa { get; set; }
        public int CuposRestantes { get; set; }
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public string EstadoActual { get; set; } = string.Empty;    
    }
}
