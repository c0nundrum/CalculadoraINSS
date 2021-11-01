using System;

namespace CalculadoraINSS.Calculadora
{
	public interface ICalculadorInss
	{
		/// <summary>
		/// Deve retornar o deconto do INSS aplicado ao salário, na determinada data.
		/// </summary>
		/// <param name="data">Data para realização do cálculo.</param>
		/// <param name="salario">Salário para base do desconto.</param>
		/// <returns>
		/// Valor do desconto.
		/// </returns>
		decimal CalcularDesconto(DateTime data, decimal salario);
	}
}
