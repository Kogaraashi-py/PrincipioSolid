using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class MenuIngresoNuevaEntidadResponsable
    {
       
            public void Menu()
            {
                Console.Clear();
                Console.WriteLine("=== REGISTRO DE NUEVA ENTIDAD RESPONSABLE ===");

                Console.Write("Ingrese el nombre de la entidad responsable: ");
                string nombreEntidad = Console.ReadLine();

                // Aquí se podría llamar a la lógica del BLL para registrar la entidad
                Console.WriteLine($"\nEntidad Responsable '{nombreEntidad}' registrada exitosamente.");
                Console.WriteLine("\nPresione cualquier tecla para volver al menú principal.");
                Console.ReadKey();
            }
        
    }
}
