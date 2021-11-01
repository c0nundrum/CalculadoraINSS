using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace CalculadoraINSS.DataAccess.Drivers
{
    /// <summary>
    /// Desserializador de um arquivo XML com as informações de dados
    /// </summary>
    class TableFromXML : TableReader
    {
        /// <summary>
        /// Constrói um Dictionary que pode ser fornecido com a interface <see cref="ITableFrom"/> para as classes de acesso aos dados
        /// </summary>
        /// <param name="tableAddress">Endereço da tabela XML a ser carregada</param>
        public TableFromXML(string tableAddress)
        {
            XElement root = XElement.Load(tableAddress);

            IEnumerable<XElement> anos = from el in root.Elements("Ano")
                                         select el;

            _discountTable = new Dictionary<int, Dictionary<decimal, TableDataStruct>>(anos.Count());

            foreach (var ano in anos)
            {
                string data = ano.Attribute("Data").Value;

                int key = int.Parse(data, CultureInfo.InvariantCulture);

                IEnumerable<XElement> q = from c in ano.Descendants("Salario")
                                          select c;

                Dictionary<decimal, TableDataStruct> _discounts = new Dictionary<decimal, TableDataStruct>(q.Count());


                foreach (var el in q)
                {
                    string Key = el.Attribute("Valor").Value;
                    string Value = el.Descendants("Desconto").Single().Value;



                    decimal flKey = decimal.Parse(Key, CultureInfo.InvariantCulture);
                    decimal flValue = decimal.Parse(Value, CultureInfo.InvariantCulture);

                    TableDataStruct s = new TableDataStruct(flValue);

                    _discounts.Add(flKey, s);
                }

                IEnumerable<XElement> t = from c in ano.Descendants("Teto")
                                          select c;

                foreach (var el in t)
                {
                    decimal flValue = decimal.Parse(el.Value, CultureInfo.InvariantCulture);
                    TableDataStruct s = new TableDataStruct((decimal)flValue, true);

                    _discounts.Add(decimal.MaxValue, s);
                }

                _discountTable.Add(key, _discounts);

            }
        }
    }
}
