using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YtbDown.Util
{
    public class FormNome
    {
        public static string FormatarNomeArquivo (string nomeOriginal, int limiteTamanho)
        {
            var caracteresInvalidos = Path.GetInvalidFileNameChars();
            foreach (char c in caracteresInvalidos)
            {
                nomeOriginal = nomeOriginal.Replace(c, '_');
            }

            if (nomeOriginal.Length > limiteTamanho)
            {
                nomeOriginal = nomeOriginal.Substring(0, limiteTamanho);
            }

            return nomeOriginal;
        }
    }
}