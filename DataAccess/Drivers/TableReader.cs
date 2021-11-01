using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraINSS.DataAccess.Drivers
{
    /// <summary>
    /// Classe Abstrata para a implementação de Drivers de desserialização de tabela.
    /// Guarda e fornece os valores dos descontos.
    /// </summary>
    abstract class TableReader : ITableFrom
    {
        protected Dictionary<int, Dictionary<decimal, TableDataStruct>> _discountTable;

        /// <summary>
        /// Busca os valores do desconto do salário.
        /// </summary>
        /// <param name="ano">Ano do cálculo</param>
        /// <param name="salario">Salário para base de cálculo.</param>
        /// <param name="tableDataStruct"><see cref="TableDataStruct"/> imutável com os valores do desconto e a forma de aplicação no cálculo.</param>
        /// <returns>
        /// Falso caso o ano não exista, verdadeiro de qualquer outra forma
        /// </returns>
        public bool GetDiscountValue(int ano, decimal salario, out TableDataStruct tableDataStruct)
        {
            if (_discountTable.TryGetValue(ano, out var value))
            {
                //Devido ao decimal.MaxValue sempre retorna um valor, a query no dict na verdade retorna que existe pelo menos um valor, portanto Single não pode ser usado.
                //First(x => x.Key >= salario).Value tambem é uma opção, mas a testagem mostrou que ela é em geral duas vezes mais lenta.
                var dataStruct = value.Where(x => x.Key >= salario).First().Value;

                tableDataStruct = dataStruct;

                return true;
            }
            else
            {
                tableDataStruct = new TableDataStruct(decimal.MinusOne, false); //dummy data, não é lido, nullables diminuem a velocidade do processamento devido ao unboxing
                return false;
            }
        }
    }
}
