using CalculadoraINSS.Calculadora;
using CalculadoraINSS.Tests;
using System;
using System.Globalization;

namespace CalculadoraINSS
{
    /// <summary>
    /// Ponto de Entrada do programa, Mostra tela de testes com os resultados e performa os testes de Assertion
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //O código assume que os elementos são gravados na tabela no formato InvariantCulture
            CultureInfo.CurrentCulture = new CultureInfo("pt-BR");

            Calcular calculadora = new();
#if DEBUG
            PerformAssertionTests(calculadora, false);
#endif

            try
            {
                Exemplo(calculadora);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        /// <summary>
        /// Método de entrada que chama o teste para a Calculadora,
        /// preenche os arrays de teste, testa as asserções e potencialmente mostra os resultados na tela.
        /// </summary>
        /// <param name="calculadora">Calculadora para ser testada.</param>
        /// <param name="display">Mostra os resultados no console.</param>
        private static void PerformAssertionTests(Calcular calculadora, bool display = true)
        {
            UnitTests testes = new(calculadora);

            testes.Populate2011(false);
            testes.Populate2012(false);

            testes.PerformTests();

            if (display)
            {
                testes.Salario2011.DisplayOnConsole();
                testes.Salario2012.DisplayOnConsole();
            }

        }

        /// <summary>
        /// Mostra exemplos na tela com pelo menos um salario em cada nível de desconto em cada ano
        /// </summary>
        /// <param name="calculadora">Calculadora utilizada</param>
        private static void Exemplo(Calcular calculadora)
        {
            DateTime date2011 = new(2011, 07, 28, 22, 35, 5, new CultureInfo("pt-BR", false).Calendar);
            DateTime date2012 = new(2012, 07, 28, 22, 35, 5, new CultureInfo("pt-BR", false).Calendar);

            decimal[] salarios2011 = { 1106.90m, 1844.83m, 3689.66m, 3689.67m };
            decimal[] salarios2012 = { 1000.00m, 1500.00m, 3000.00m, 4000.00m, 4000.01m };

            Console.WriteLine(String.Format("2011"));
            Console.WriteLine(String.Format("|{0,30}|{1,30}|{2,30}|", "Salario Original", "Desconto", "Salario Final"));

            CalculateAndDisplay(calculadora, date2011, salarios2011);
            CalculateAndDisplay(calculadora, date2011, salarios2012);

            Console.WriteLine(String.Format("=================================================================================================="));

            Console.WriteLine(String.Format("2012"));
            Console.WriteLine(String.Format("|{0,30}|{1,30}|{2,30}|", "Salario Original", "Desconto", "Salario Final"));

            CalculateAndDisplay(calculadora, date2012, salarios2012);
            CalculateAndDisplay(calculadora, date2012, salarios2011);

            Console.WriteLine(String.Format("=================================================================================================="));
        }

        /// <summary>
        /// Método de ajuda para o display de exemplo.
        /// Mostra uma linha da tabela.
        /// </summary>
        /// <param name="c">Calculadora utilizada</param>
        /// <param name="date">Data</param>
        /// <param name="salarios">Lista de Salarios</param>
        private static void CalculateAndDisplay(Calcular c, DateTime date, decimal[] salarios)
        {
            foreach (var salario in salarios)
            {
                decimal desconto = c.CalcularDesconto(date, salario);

                Console.WriteLine(String.Format("|{0,30}|{1,30}|{2,30}|", salario, desconto, salario - desconto));
            }
        }
    }
}
