using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Entity;

namespace DAL
{
    public class LiquidacionRepository : ILiquidacionRepository
    {
        private readonly string FileName = "liquidaciones.txt";

        public void GuardarLiquidacion(Liquidacion liquidacion)
        {
            try
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), FileName);
                using (StreamWriter escritor = new StreamWriter(filePath, true))
                {
                    escritor.WriteLine(
                        $"{liquidacion.NumeroLiquidacion};" +
                        $"{liquidacion.SalarioDevengado};" +
                        $"{liquidacion.DiasIncapacidad};" +
                        $"{liquidacion.FechaInicio:yyyy-MM-dd};" +
                        $"{(int)liquidacion.Responsable};" +
                        $"{liquidacion.SalarioDiario};" +
                        $"{liquidacion.ValorDejadoPercibir};" +
                        $"{liquidacion.PorcentajeAplicado};" +
                        $"{liquidacion.ValorCalculado};" +
                        $"{liquidacion.ValorSMLMD};" +
                        $"{liquidacion.ValorAPagar}"
                    );
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al guardar la liquidación", ex);
            }
        }

        public List<Liquidacion> LeerArchivos()
        {
            List<Liquidacion> liquidaciones = new List<Liquidacion>();
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), FileName);

            if (!File.Exists(filePath)) return liquidaciones;

            using (StreamReader reader = new StreamReader(filePath))
            {
                string linea;
                while ((linea = reader.ReadLine()) != null)
                {
                    liquidaciones.Add(MappingType(linea));
                }
            }
            return liquidaciones;
        }

        public bool ExisteLiquidacion(int numero)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), FileName);
            if (!File.Exists(filePath)) return false;

            using (StreamReader reader = new StreamReader(filePath))
            {
                string linea;
                while ((linea = reader.ReadLine()) != null)
                {
                    string[] datos = linea.Split(';');
                    if (int.TryParse(datos[0], out int num) && num == numero)
                        return true;
                }
            }
            return false;
        }

        private Liquidacion MappingType(string linea)
        {
            string[] datos = linea.Split(';');
            if (datos.Length != 11)
                throw new FormatException("Formato de línea inválido.");

            return new Liquidacion
            {
                NumeroLiquidacion = int.Parse(datos[0]),
                SalarioDevengado = decimal.Parse(datos[1]),
                DiasIncapacidad = int.Parse(datos[2]),
                FechaInicio = DateTime.Parse(datos[3]),
                Responsable = (ResponsablePago)int.Parse(datos[4]),
                SalarioDiario = decimal.Parse(datos[5]),
                ValorDejadoPercibir = decimal.Parse(datos[6]),
                PorcentajeAplicado = decimal.Parse(datos[7]),
                ValorCalculado = decimal.Parse(datos[8]),
                ValorSMLMD = decimal.Parse(datos[9]),
                ValorAPagar = decimal.Parse(datos[10])
            };
        }
    }
}