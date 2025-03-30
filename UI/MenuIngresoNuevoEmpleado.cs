using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using BLL;
namespace UI
{
    public class MenuIngresoNuevoEmpleado
    {
        private LiquidacionService _liquidacionService;

        public MenuIngresoNuevoEmpleado()
        {
            _liquidacionService = new LiquidacionService();
        }

        public void Menu()
        {
            Console.Clear();
            Console.WriteLine("=== REGISTRO DE NUEVO EMPLEADO E INCAPACIDAD ===");

            // Registro del empleado
            Console.Write("Ingrese el nombre del empleado: ");
            string nombre = Console.ReadLine();

            Console.Write("Ingrese el salario mensual (>= 1.300.000): ");
            decimal salario;
            while (!decimal.TryParse(Console.ReadLine(), out salario) || salario < 1300000)
            {
                Console.Write("Valor inválido. Ingrese el salario mensual (>= 1.300.000): ");
            }

            Console.Write("Ingrese los días de incapacidad (1-540): ");
            int dias;
            while (!int.TryParse(Console.ReadLine(), out dias) || dias < 1 || dias > 540)
            {
                Console.Write("Valor inválido. Ingrese los días de incapacidad (1-540): ");
            }

            // Obtener el número de liquidación basado en el conteo de registros actuales
            int numeroLiquidacion = _liquidacionService.ConsultarTodas().Count + 1;

            // Determinar el responsable del pago según los días de incapacidad
            ResponsablePago responsable = DeterminarResponsable(dias);

            // Crear la liquidación
            Liquidacion nuevaLiquidacion = new Liquidacion
            {
                NumeroLiquidacion = numeroLiquidacion,
                SalarioDevengado = salario,
                DiasIncapacidad = dias,
                FechaInicio = DateTime.Now,
                Responsable = responsable
            };

            // Registrar la liquidación
            string resultado = _liquidacionService.AgregarLiquidacion(nuevaLiquidacion);

            // Mostrar el resultado
            Console.WriteLine($"\n{resultado}");
            Console.WriteLine($"\nEmpleado '{nombre}' registrado exitosamente.");
            Console.WriteLine($"Número de Liquidación: {numeroLiquidacion}");
            Console.WriteLine($"Salario mensual: {salario}");
            Console.WriteLine($"Días de incapacidad: {dias}");
            Console.WriteLine($"Responsable del pago: {responsable}");

            Console.WriteLine("\nPresione cualquier tecla para volver al menú principal.");
            Console.ReadKey();
        }

        private ResponsablePago DeterminarResponsable(int dias)
        {
            if (dias <= 2)
                return ResponsablePago.Empleador;
            if (dias <= 180)
                return ResponsablePago.EPS;
            return ResponsablePago.FondoPensiones;
        }
    }

}
