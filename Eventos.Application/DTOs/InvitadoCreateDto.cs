using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class InvitadoCreateDto
    {
        public string Nombre {  get; set; }
        public string Telefono { get; set; }
        public int TotalPersonas { get; set; }
        public int MesaId { get; set; }
        public Guid EventoId { get; set; }  
    }
}
