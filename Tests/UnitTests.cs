using CalculadoraINSS.Calculadora;
using CalculadoraINSS.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace CalculadoraINSS.Tests
{
    /// <summary>
    /// Classe imutável de Dados Puros que guarda o Salario Original e o Desconto para fins de teste
    /// </summary>
    public struct SalarioData
    {
        public readonly decimal SalarioOriginal { get; }
        public readonly decimal DescontoSalario { get; }

        public SalarioData(decimal salarioOriginal, decimal desconto)
        {
            SalarioOriginal = salarioOriginal;
            DescontoSalario = desconto;
        }
    }
    /// <summary>
    /// Classe que realiza testes de asserção em dada calculadora.
    /// </summary>
    class UnitTests
    {
        private readonly int TESTNUMBER = 30000;

        /// <summary>
        /// Lista de <see cref="SalarioData"/> do ano de 2011 gerada com dados aleatórios.
        /// </summary>
        public List<SalarioData> Salario2011 { get; private set; }

        /// <summary>
        /// Lista de <see cref="SalarioData"/> do ano de 2012 gerada com dados aleatórios.
        /// </summary>
        public List<SalarioData> Salario2012 { get; private set; }

        private readonly Calcular _calculadora;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="c">Calculadora a ser testada.</param>
        /// <param name="numberOfTests">Numero de testes a serem realizados.</param>
        public UnitTests(Calcular c, int numberOfTests = 3000)
        {
            _calculadora = c;
            TESTNUMBER = numberOfTests;
        }

        /// <summary>
        /// Realiza os testes de Asserção para cada Lista criada. Utilizando <see cref="GetBruteForceDesconto2011(decimal)"/> e <see cref="GetBruteForceDesconto2012(decimal)"/>
        /// para gerar os valores comprovatórios.
        /// </summary>
        public void PerformTests()
        {
            foreach (var item in Salario2011)
            {
                var desconto = GetBruteForceDesconto2011(item.SalarioOriginal);
                Debug.Assert(desconto == item.DescontoSalario, string.Format("Ano 2011 Calculadora = {0} : BruteForce = {1}", desconto, item.DescontoSalario));
            }

            DateTime e = new(2012, 07, 28, 22, 35, 5, new CultureInfo("pt-BR", false).Calendar);
            foreach (var item in Salario2012)
            {
                var desconto = GetBruteForceDesconto2012(item.SalarioOriginal);
                Debug.Assert(desconto == item.DescontoSalario, string.Format("Ano 2012 Calculadora = {0} : BruteForce = {1}", desconto, item.DescontoSalario));
            }

        }

        /// <summary>
        /// Constroi uma <see cref="SalarioData"/> a partir dos dados de salário
        /// </summary>
        /// <param name="d">Data para cálculo</param>
        /// <param name="random">Random utilizado para geração aleatória</param>
        /// <param name="useDecimal">Se verdadeiro utiliza o algoritmo <see cref="RandomExtensions.NextDecimal(Random, bool)"/> para 
        /// Realização dos testes</param>
        /// <returns>
        /// Instância de <see cref="SalarioData"/>
        /// </returns>
        private SalarioData BuildRandomSalarioData(DateTime d, Random random, bool useDecimal = true)
        {
            decimal salario = useDecimal? random.NextDecimal(true) : random.Next(1,6000);
            decimal desconto = _calculadora.CalcularDesconto(d, salario);

            return new SalarioData(salario, desconto);
        }

        /// <summary>
        /// Cria uma lista de <see cref="SalarioData"/> para testes utilizando o ano de 2011 como default.
        /// Gera números aleatórios para cada salário.
        /// </summary>
        /// <param name="useDecimal">Se verdadeiro utiliza o algoritmo <see cref="RandomExtensions.NextDecimal(Random, bool)"/> para 
        /// Realização dos testes</param>
        public void Populate2011(bool useDecimal)
        {
            Random random = new(231);
            DateTime d = new(2011, 07, 28, 22, 35, 5, new CultureInfo("pt-BR", false).Calendar);

            Salario2011 = new List<SalarioData>();
            for (int i = 0; i < TESTNUMBER; i++)
            {               
                Salario2011.Add(BuildRandomSalarioData(d, random, useDecimal));
            }
        }

        /// <summary>
        /// Cria uma lista de <see cref="SalarioData"/> para testes utilizando o ano de 2012 como default.
        /// Gera números aleatórios para cada salário.
        /// </summary>
        /// <param name="useDecimal">Se verdadeiro utiliza o algoritmo <see cref="RandomExtensions.NextDecimal(Random, bool)"/> para 
        /// Realização dos testes</param>
        public void Populate2012(bool useDecimal)
        {
            Random random = new(65456);
            DateTime d = new(2012, 07, 28, 22, 35, 5, new CultureInfo("pt-BR", false).Calendar);

            Salario2012 = new List<SalarioData>();
            for (int i = 0; i < TESTNUMBER; i++)
            {
                Salario2012.Add(BuildRandomSalarioData(d, random, useDecimal));
            }
        }

        /// <summary>
        /// Algoritmo que calcula o desconto do ano de 2011 de forma bruta. Utilizado apenas para testes.
        /// </summary>
        /// <param name="salario">Salario a ser testado</param>
        /// <returns>
        /// O Desconto a ser aplicado no salário
        /// </returns>
        private static decimal GetBruteForceDesconto2011(decimal salario)
        {
            decimal desconto;
            if (salario <= 1106.90m)
            {
                desconto = salario * 0.08m;
            }
            else if (salario <= 1844.93m)
            {
                desconto = salario * 0.09m;
            }
            else if (salario <= 3689.66m)
            {
                desconto = salario * 0.11m;
            }
            else
            {
                desconto = 405.86m;
            }

            return desconto;
        }

        /// <summary>
        /// Algoritmo que calcula o desconto do ano de 2012 de forma bruta. Utilizado apenas para testes.
        /// </summary>
        /// <param name="salario">Salario a ser testado</param>
        /// <returns>
        /// O Desconto a ser aplicado no salário
        /// </returns>
        private static decimal GetBruteForceDesconto2012(decimal salario)
        {
            decimal desconto;
            if (salario <= 1000.00m)
            {
                desconto = salario * 0.07m;
            }
            else if (salario <= 1500.00m)
            {
                desconto = salario * 0.08m;
            }
            else if (salario <= 3000.00m)
            {
                desconto = salario * 0.09m;
            }
            else if (salario <= 4000.00m)
            {
                desconto = salario * 0.11m;
            }
            else
            {
                desconto = 500.00m;
            }

            return desconto;
        }
    }
}
