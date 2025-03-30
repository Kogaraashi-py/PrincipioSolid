using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entity;

namespace UI
{
    public class MenuPrincipal
    {
        public void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("╔════════════════════════════════════════╗");
                Console.WriteLine("║         SISTEMA DE LIQUIDACIONES         ║");
                Console.WriteLine("╚════════════════════════════════════════╝");
                Console.ResetColor();
                Console.WriteLine("\nSeleccione una opción:");
                Console.WriteLine("1. Registrar nuevo empleado (e incapacidad)");
                Console.WriteLine("2. Ingresar nueva entidad responsable");
                Console.WriteLine("3. Consultar todas las liquidaciones");
                Console.WriteLine("4. Salir");
                Console.Write("\nOpción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        MenuIngresoNuevoEmpleado menuEmpleado = new MenuIngresoNuevoEmpleado();
                        menuEmpleado.Menu();
                        break;
                    case "2":
                        MenuIngresoNuevaEntidadResponsable menuEntidad = new MenuIngresoNuevaEntidadResponsable();
                        menuEntidad.Menu();
                        break;
                    case "3":
                        MenuConsultarDatos menuConsultar = new MenuConsultarDatos();
                        menuConsultar.ConsultarDatos();
                        break;
                    case "4":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nSaliendo del sistema... ¡Hasta pronto!");
                        Console.ResetColor();
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nOpción no válida. Presione cualquier tecla para continuar.");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}