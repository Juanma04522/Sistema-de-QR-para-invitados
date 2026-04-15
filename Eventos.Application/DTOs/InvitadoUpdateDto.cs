using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class InvitadoUpdateDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public int MesaId { get; set; }
        public int TotalPersonas { get; set; }
        public int CuposIngresado { get; set; }
    }
}
