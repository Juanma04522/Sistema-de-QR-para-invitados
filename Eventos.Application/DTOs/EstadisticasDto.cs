using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class EstadisticasDto
    {
        public int TotalEsperado { get; set; }
        public int TotalIngresado { get; set; }
        public int Pendientes { get; set; }
    }
}
