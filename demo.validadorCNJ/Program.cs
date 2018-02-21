using System;
using System.Text.RegularExpressions;

namespace testeCNJ
{
    class Program
    {
        static void Main(string[] args)
        {
            // 0033739-92.1999.8.26.0100 -- ok
            // 1016768-15.1999.8.26.0100 -- erro com valores maiores que Long

            if (args.Length == 0)
                throw new Exception("Informa o número do processo");
            
            if (args[0].Length < 20)
                throw new Exception("Informa um número válido");

            string n = Regex.Replace(args[0], "[^0-9]", "");

            if (n.Length != 20)
                throw new Exception("Número inválido");

            var processo = new Tuple<string, string, string, string, string, string>(
                n.Substring(0, 7),  // Sequencial
                n.Substring(7, 2),  // Digito verificador
                n.Substring(9, 4),  // Ano do ajuizamento
                n.Substring(13, 1), // Segmento do judiciário
                n.Substring(14, 2), // Tribunal
                n.Substring(16, 4)  // Unidade de origem
            );

            string c = processo.Item1 + processo.Item3 + processo.Item4 + processo.Item5 + processo.Item6 + "00";
            
            System.UInt64 s;
            System.UInt64.TryParse(c, out s);

            Console.WriteLine($"Concat-str: {c}");
            Console.WriteLine($"Concat-int: {s}");

            System.UInt64 digito;
            System.UInt64.TryParse(processo.Item2, out digito);

            var r = 98 - (s % 97);

            Console.WriteLine($"Resultado: {digito} == {r}");

            if (digito == r)
                Console.WriteLine(" -- válido -- ");
        }
    }
}
