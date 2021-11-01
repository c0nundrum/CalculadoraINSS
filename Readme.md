# Calculadora INSS

Implementação Data Driven de uma calculadora de descontos INSS, de acordo com o requisitado, que pode ser extendida com drivers para leitura de tabelas de desconto de diversos formatos.
Feito em C# com as especificações do .Net 5.0.

## Dependências

* Cuidado foi tomado para evitar o uso de bibliotecas de terceiros. A única exceção é a biblioteca Newsoft.Json, utilizado no desserializador de Jsons.
* As demais partes do programa foram feitas com as bibliotecas nativas do C#.
* O software assume que as tabelas estejam com os números gravados no formato InvariantCulture.

## Notas

* As features foram desenvolvidas de acordo com o requisitado. Toda a informação dos descontos vem de tabelas encontradas na pasta DataTables, as quais podem ser modificadas e extendidas sem tocar no código fonte.
* Em alguns lugares foi escolhida performance em detrimento da legibilidade, principalmente no framework de testes. Os lugares estão devidamente notados.
* Em lugares chaves, onde poderia ter implementado features de forma diferente, busquei elucidar as decisões de arquitetura do software nos comentários.