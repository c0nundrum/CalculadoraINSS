using System;
namespace CalculadoraINSS.Extensions
{
    /// <summary>
    /// Extensões de alta performance para geração de números aleatórios
    /// </summary>
    static class RandomExtensions
    {
        /// <summary>
        /// Constrói uma int32 com todo o range possível
        /// </summary>
        public static int NextInt32(this Random rng)
        {
            int firstBits = rng.Next(0, 1 << 4) << 28;
            int lastBits = rng.Next(0, 1 << 28);
            return firstBits | lastBits;
        }

        /// <summary>
        /// Constrói um decimal a partir do construtor de bytes da classe.
        /// </summary>
        /// <param name="allowNegative">Permite a geração de números negativos</param>
        /// <returns></returns>
        public static decimal NextDecimal(this Random rng, bool allowNegative = false)
        {
            //Aqui foi utilizado performance em detrimento da legibilidade.
            //Maiores instruções poderiam causar "Register Spilling"
            //Como o código é feito para ser chamado milhares de vezes no teste
            //qualquer melhoria de performance é notável
            byte scale = (byte)rng.Next(29);
            bool sign = !allowNegative && rng.Next(2) == 1;
            return new decimal(rng.NextInt32(),
                               rng.NextInt32(),
                               rng.NextInt32(),
                               sign,
                               scale);
        }
    }
}
