using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace CalculadoraINSS.DataAccess.Drivers
{
    /// <summary>
    /// Desserializador de um arquivo Json com as informações de dados
    /// </summary>
    class TableFromJson : TableReader
    {
        /// <summary>
        /// Constrói um Dictionary que pode ser fornecido com a interface <see cref="ITableFrom"/> para as classes de acesso aos dados
        /// </summary>
        /// <param name="tableAddress">Endereço do arquivo Json a ser carregado</param>
        public TableFromJson(string jsonAddress)
        {

            JObject jsonObj = JObject.Parse(File.ReadAllText(jsonAddress));

            _discountTable = new Dictionary<int, Dictionary<decimal, TableDataStruct>>(jsonObj.Count);

            foreach (var item in jsonObj)
            {
                string data = item.Key;
                int key = int.Parse(data);

                var children = item.Value.Children()["Salarios"];

                Dictionary<decimal, TableDataStruct> _discounts = new(children.Count());

                foreach (var child in children)
                {
                    foreach (var element in child)
                    {
                        string valor = element.Value<string>("Valor");
                        string desconto = element.Value<string>("Desconto");

                        decimal flKey = decimal.Parse(valor, CultureInfo.InvariantCulture);
                        decimal flValue = decimal.Parse(desconto, CultureInfo.InvariantCulture);

                        TableDataStruct s = new(flValue);

                        _discounts.Add(flKey, s);
                    }

                }

                var teto = item.Value.Children()["Teto"];
                foreach (var child in teto)
                {
                    string value = child.ToString();
                    var flValue = decimal.Parse(value, CultureInfo.InvariantCulture);
                    TableDataStruct s = new(flValue, true);

                    _discounts.Add(decimal.MaxValue, s);

                }

                _discountTable.Add(key, _discounts);
            }
        }
    }

}
