using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;


namespace DAL
{
    public interface ILiquidacionRepository
    {
        void GuardarLiquidacion(Liquidacion liquidacion);
        List<Liquidacion> LeerArchivos();
        bool ExisteLiquidacion(int numero);
    }
}