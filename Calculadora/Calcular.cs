using CalculadoraINSS.DataAccess;
using System;

namespace CalculadoraINSS.Calculadora
{
    /// <summary>
    /// Calcula o Desconto sofrido por determinado salário em determinada data.
    /// </summary>
    class Calcular : ICalculadorInss
    {
        private readonly DataInterface _table;

        public Calcular()
        {
            _table = new DataInterface(ETableDriver.JSON);
        }

        /// <inheritdoc />
        public decimal CalcularDesconto(DateTime data, decimal salario)
        {

            var discountData = _table.GetDiscountValue(data, salario);

            decimal discount = discountData.AcimaTeto ? discountData.Desconto : discountData.Desconto * salario;

            return discount;
        }
    }
}
