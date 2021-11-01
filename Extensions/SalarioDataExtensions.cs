using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraINSS.Tests
{
    static class SalarioDataExtensions
    {
        /// <summary>
        /// Mostra a lista de <see cref="SalarioData"/> na forma de uma tabela na tela do console.
        /// </summary>
        public static void DisplayOnConsole(this List<SalarioData> list)
        {
            int c = list.Count;

            Console.WriteLine(String.Format("|{0,30}|{1,30}|{2,30}|", "Salario Original", "Desconto", "Salario Final"));

            for (int i = 0; i < c; i++)
            {
                var item = list[i];

                decimal salarioOriginal = Math.Round(item.SalarioOriginal, 2);
                decimal desconto = Math.Round(item.DescontoSalario, 2);

                Console.WriteLine(String.Format("|{0,30}|{1,30}|{2,30}|", salarioOriginal, desconto, salarioOriginal - desconto));
            }

            Console.WriteLine(String.Format("=================================================================================================="));
        }
    }
}
