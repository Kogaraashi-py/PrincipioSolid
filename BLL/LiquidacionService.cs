using DAL;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    // ======================================================
    // Ejemplo que NO cumple SOLID (Comentado)
    // ======================================================
    /*
    public class LiquidacionService 
    {
        private LiquidacionRepository _repository; // Viola DIP (dependencia concreta)
        private const decimal SMLMV = 1300000m;

        public LiquidacionService() // Acoplamiento fuerte
        {
            _repository = new LiquidacionRepository(); // Viola DIP
        }

        public string AgregarLiquidacion(Liquidacion liquidacion)
        {
            // SRP violado: Validación, cálculo y DAL en una clase
            if (liquidacion == null) return "Error..."; // Validación aquí
            CalcularLiquidacion(liquidacion); // Cálculo aquí
            _repository.GuardarLiquidacion(liquidacion); // DAL aquí
            return "Éxito";
        }

        private void CalcularLiquidacion(Liquidacion liquidacion)
        {
            // OCP violado: Si añades un nuevo responsable, debes modificar este método
            if (liquidacion.DiasIncapacidad < 2)
            {
                // Lógica para Empleador...
            }
            else if (...) // Más condiciones
        }
    }
    */

    // ======================================================
    // Código que SÍ cumple SOLID
    // ======================================================

    // --------------------------------------
    // Interfaces para OCP (Principio Abierto/Cerrado)
    // --------------------------------------
    public interface ICalculadorResponsable
    {
        bool Aplica(Liquidacion liquidacion);
        void Calcular(Liquidacion liquidacion);
    }

    // --------------------------------------
    // Estrategias de cálculo (OCP)
    // --------------------------------------
    public class CalculadorEmpleador : ICalculadorResponsable
    {
        public bool Aplica(Liquidacion liquidacion) => liquidacion.DiasIncapacidad < 2;

        public void Calcular(Liquidacion liquidacion)
        {
            liquidacion.Responsable = ResponsablePago.Empleador;
            liquidacion.PorcentajeAplicado = 0.666m;
        }
    }

    public class CalculadorEPS : ICalculadorResponsable
    {
        public bool Aplica(Liquidacion liquidacion) => liquidacion.DiasIncapacidad <= 180;

        public void Calcular(Liquidacion liquidacion)
        {
            liquidacion.Responsable = ResponsablePago.EPS;
            liquidacion.PorcentajeAplicado = 0.6m;
        }
    }

    public class CalculadorFondoPensiones : ICalculadorResponsable
    {
        public bool Aplica(Liquidacion liquidacion) => liquidacion.DiasIncapacidad > 180;

        public void Calcular(Liquidacion liquidacion)
        {
            liquidacion.Responsable = ResponsablePago.FondoPensiones;
            liquidacion.PorcentajeAplicado = 1.0m;
        }
    }

    // --------------------------------------
    // LiquidacionCalculator (OCP aplicado)
    // --------------------------------------
    public class LiquidacionCalculator
    {
        private const decimal SMLMV = 1300000m;
        private readonly List<ICalculadorResponsable> _calculadores;

        public LiquidacionCalculator()
        {
            _calculadores = new List<ICalculadorResponsable>
            {
                new CalculadorEmpleador(),
                new CalculadorEPS(),
                new CalculadorFondoPensiones()
            };
        }

        public void Calcular(Liquidacion liquidacion)
        {
            CalcularValoresBase(liquidacion);
            AplicarEstrategia(liquidacion);
            CalcularValoresFinales(liquidacion);
        }

        private void CalcularValoresBase(Liquidacion liquidacion)
        {
            liquidacion.SalarioDiario = liquidacion.SalarioDevengado / 30;
            liquidacion.ValorDejadoPercibir = liquidacion.SalarioDiario * liquidacion.DiasIncapacidad;
        }

        private void AplicarEstrategia(Liquidacion liquidacion)
        {
            foreach (var calculador in _calculadores)
            {
                if (calculador.Aplica(liquidacion))
                {
                    calculador.Calcular(liquidacion);
                    break;
                }
            }
        }

        private void CalcularValoresFinales(Liquidacion liquidacion)
        {
            liquidacion.ValorCalculado = liquidacion.ValorDejadoPercibir * liquidacion.PorcentajeAplicado;
            liquidacion.ValorSMLMD = (SMLMV / 30) * liquidacion.DiasIncapacidad;
            liquidacion.ValorAPagar = Math.Max(liquidacion.ValorCalculado, liquidacion.ValorSMLMD);
        }
    }

    // --------------------------------------
    // LiquidacionService (SRP y DIP aplicados)
    // --------------------------------------

    public class LiquidacionValidator
    {
        private readonly ILiquidacionRepository _repository;

        // Constructor con inyección de dependencia (DIP)
        public LiquidacionValidator(ILiquidacionRepository repository)
        {
            _repository = repository;
        }

        public string Validar(Liquidacion liquidacion)
        {
            if (liquidacion == null)
                return "Error: Liquidación inválida.";

            if (liquidacion.SalarioDevengado <= 0)
                return "Error: El salario devengado debe ser mayor a 0.";

            if (liquidacion.DiasIncapacidad <= 0)
                return "Error: Los días de incapacidad deben ser mayores a 0.";

            if (liquidacion.FechaInicio > DateTime.Now)
                return "Error: La fecha de inicio no puede ser futura.";

            if (!Enum.IsDefined(typeof(ResponsablePago), liquidacion.Responsable))
                return "Error: Responsable de pago no válido.";

            if (_repository.ExisteLiquidacion(liquidacion.NumeroLiquidacion))
                return "Error: Ya existe una liquidación con este número.";

            return null; // Validación exitosa
        }
    }
    public class LiquidacionService
    {
        private readonly ILiquidacionRepository _repository;
        private readonly LiquidacionValidator _validator;
        private readonly LiquidacionCalculator _calculator;

        public LiquidacionService(ILiquidacionRepository repository) // DIP: Depende de abstracción
        {
            _repository = repository;
            _validator = new LiquidacionValidator(repository);
            _calculator = new LiquidacionCalculator();
        }

        public string AgregarLiquidacion(Liquidacion liquidacion)
        {
            string error = _validator.Validar(liquidacion);
            if (error != null) return error;

            _calculator.Calcular(liquidacion); // SRP: Cálculo delegado
            _repository.GuardarLiquidacion(liquidacion); // SRP: DAL delegado
            return "Liquidación registrada exitosamente.";
        }

        public List<Liquidacion> ConsultarTodas() => _repository.LeerArchivos();
    }
}