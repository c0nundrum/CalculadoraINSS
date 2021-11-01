using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraINSS.DataAccess
{
    /// <summary>
    /// Estrutura imutável de Dados Puros que guarda o desconto e o modo que ele deve ser aplicado ao cálculo.
    /// </summary>
    struct TableDataStruct
    {
        public readonly decimal Desconto { get; }
        //Aqui escolhi um boolean para definir o método de calculo que será interpretado na Calculadora
        //Outra opção seria passar um Delegate ou lambda, porém aumentaria a complexidade e o número de dados
        //para serem transferidos.
        public readonly bool AcimaTeto { get; }

        public TableDataStruct(decimal desconto, bool acimaTeto = false)
        {
            Desconto = desconto;
            AcimaTeto = acimaTeto;
        }

        public override string ToString()
        {
            return string.Format("Desconto = {0}; Acima do teto = {1}", Desconto, AcimaTeto);
        }
    }

}
