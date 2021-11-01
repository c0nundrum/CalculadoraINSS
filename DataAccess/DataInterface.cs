using CalculadoraINSS.DataAccess.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CalculadoraINSS.DataAccess
{
    /// <summary>
    /// Enum com os tipos de drivers suportados pelo desserializador
    /// </summary>
    public enum ETableDriver { XML, JSON }

    /// <summary>
    /// Classe que é responsável pela comunicação com os Drivers de Serialização
    /// <seealso cref="TableReader"/>
    /// </summary>
    /// <exception cref="NotImplementedException">Caso um driver não implementado seja passado</exception>
    /// <exception cref="KeyNotFoundException">Caso a chave do ano não seja encontrada</exception>
    class DataInterface
    {
        private ITableFrom _table;

        /// <summary>
        /// Carrega as tabelas na inicialização de acordo com o driver carregado.
        /// </summary>
        /// <param name="driver">Tipo de Driver a ser Carregado</param>
        public DataInterface(ETableDriver driver = ETableDriver.XML)
        {
            switch (driver)
            {
                case ETableDriver.XML:
                    _table = new TableFromXML(@"Datatables/TabelasDesconto.xml");
                    break;
                case ETableDriver.JSON:
                    _table = new TableFromJson(@"Datatables/TabelasDesconto.json");
                    break;
                default:
                    { throw new NotImplementedException(); }

            }
        }

        /// <summary>
        /// Busca o desconto lido pela tabela de dado salário em uma data.
        /// </summary>
        /// <param name="date">Data do salário</param>
        /// <param name="salario">Salario para base de cálculo</param>
        /// <returns>
        /// <see cref="TableDataStruct"/> com o desconto e o método de aplicação no cálculo.
        /// </returns>
        public TableDataStruct GetDiscountValue(DateTime date, decimal salario)
        {
            if (salario < 0)
                throw new Exception("Salario negativo");

            if (_table.GetDiscountValue(date.Year, salario, out var value))
            {
                //Sempre retorna um valor, a query no dict na verdade retorna que existe pelo menos um valor, portanto Single não pode ser usado.
                //First(x => x.Key >= salario).Value tambem é uma opção, mas a testagem mostrou que ela é em geral duas vezes mais lenta.
                return value;
            }
            else
            {
                throw new KeyNotFoundException(string.Format("Ano {0} nao encontrado na tabela", date.Year));
            }

        }
    }
}
