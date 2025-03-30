using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Liquidacion 
    {

        public int NumeroLiquidacion { get; set; }
        public decimal SalarioDevengado { get; set; }
        public int DiasIncapacidad { get; set; }
        public DateTime FechaInicio { get; set; }
        public ResponsablePago Responsable { get; set; }
        public decimal SalarioDiario { get; set; }
        public decimal ValorDejadoPercibir { get; set; }
        public decimal PorcentajeAplicado { get; set; }
        public decimal ValorCalculado { get; set; }
        public decimal ValorSMLMD { get; set; }
        public decimal ValorAPagar { get; set; }
    }
}
