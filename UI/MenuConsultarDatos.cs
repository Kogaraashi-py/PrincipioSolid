using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using Entity;
namespace UI
{
    public class MenuConsultarDatos
    {
        public void ConsultarDatos()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== CONSULTA DE LIQUIDACIONES ===");
            Console.ResetColor();

            // Instanciar el servicio de liquidaciones
            LiquidacionService service = new LiquidacionService();

            // Obtener la lista de liquidaciones desde BLL
            List<Liquidacion> liquidaciones = service.ConsultarTodas();

            if (liquidaciones.Count == 0)
            {
                Console.WriteLine("No hay liquidaciones registradas.");
            }
            else
            {
                foreach (var liquidacion in liquidaciones)
                {
                    Console.WriteLine("--------------------------------------------------");
                    Console.WriteLine($"Número Liquidación: {liquidacion.NumeroLiquidacion}");
                    Console.WriteLine($"Salario Devengado: {liquidacion.SalarioDevengado}");
                    Console.WriteLine($"Días de Incapacidad: {liquidacion.DiasIncapacidad}");
                    Console.WriteLine($"Fecha Inicio: {liquidacion.FechaInicio}");
                    Console.WriteLine($"Responsable: {liquidacion.Responsable}");
                    Console.WriteLine($"Salario Diario: {liquidacion.SalarioDiario}");
                    Console.WriteLine($"Valor Dejado de Percibir: {liquidacion.ValorDejadoPercibir}");
                    Console.WriteLine($"Porcentaje Aplicado: {liquidacion.PorcentajeAplicado}");
                    Console.WriteLine($"Valor Calculado: {liquidacion.ValorCalculado}");
                    Console.WriteLine($"Valor SMLMD: {liquidacion.ValorSMLMD}");
                    Console.WriteLine($"Valor a Pagar: {liquidacion.ValorAPagar}");
                    Console.WriteLine("--------------------------------------------------");
                }
            }

            Console.WriteLine("\nPresione cualquier tecla para volver al menú principal.");
            Console.ReadKey();
        }
    }
}