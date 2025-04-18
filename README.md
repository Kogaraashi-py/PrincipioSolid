# PrincipiosSolid 1. Principio de Responsabilidad Única (SRP) ✅
Cumplimiento:

LiquidacionService: Coordina validación, cálculo y persistencia, pero no realiza estas tareas directamente (las delega a LiquidacionValidator, LiquidacionCalculator y ILiquidacionRepository).

LiquidacionValidator: Encargado exclusivamente de validaciones.

LiquidacionCalculator: Solo realiza cálculos.

LiquidacionRepository: Gestiona el acceso a datos.

2. Principio Abierto/Cerrado (OCP) ✅
Cumplimiento:

Estrategias de cálculo: Usa el patrón Strategy con ICalculadorResponsable, permitiendo añadir nuevos responsables (ej: ARL) sin modificar LiquidacionCalculator.

Ejemplo de extensión:

csharp
Copy
public class CalculadorARL : ICalculadorResponsable
{
    public bool Aplica(Liquidacion liquidacion) => liquidacion.DiasIncapacidad > 365;
    public void Calcular(Liquidacion liquidacion)
    {
        liquidacion.Responsable = ResponsablePago.ARL;
        liquidacion.PorcentajeAplicado = 0.8m;
    }
}
3. Inversión de Dependencias (DIP) ✅
Cumplimiento:

LiquidacionService depende de ILiquidacionRepository (abstracción), no de LiquidacionRepository (implementación concreta).

LiquidacionValidator también usa ILiquidacionRepository.
