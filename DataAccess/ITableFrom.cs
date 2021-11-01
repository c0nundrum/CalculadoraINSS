using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraINSS.DataAccess
{
    /// <summary>
    /// Implementado pelos drivers de desserialização para possiblitar o acessos das mantendo o acoplamento baixo.
    /// </summary>
    /// <returns>
    /// Dictionary com os anos e os valores de desconto de cada nível salarial em cada ano.
    /// </returns>
    interface ITableFrom
    {
        public bool GetDiscountValue(int ano, decimal salario, out TableDataStruct tableDataStruct);
    }
}
