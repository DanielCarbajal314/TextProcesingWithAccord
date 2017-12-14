using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApplication1.Parser
{
    internal class PreprocemientoDeTexto
    {
        public static string LimpiarTextoInicial(string texto)
        {
            return RemoverLineasPrimerasLineas(texto).ToLower();
        }

        private static string RemoverLineasPrimerasLineas(string texto)
        {
            string[] textoSeparado = Regex.Split(texto, "\r\n|\r|\n").ToArray();
            string[] textoSinLasPrimerasLineas = textoSeparado.Take(textoSeparado.Count() - 4).Skip(8).ToArray();
            string textoLimpiado = string.Join(Environment.NewLine, textoSinLasPrimerasLineas).Trim();
            return Regex.Replace(textoLimpiado, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);
        }
    }
}
