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
            // 5006601-96.2012.4.04.7200

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

            UInt64.TryParse(processo.Item1, out UInt64 p1);

            string resto1 = (p1 % 97).ToString();

            UInt64.TryParse($"{resto1}{processo.Item3}{processo.Item4}{processo.Item5}", out UInt64 p2);

            string resto2 = (p2 % 97).ToString();

            string c = $"{resto2}{processo.Item6}00";
            
            UInt64.TryParse(c, out UInt64 s);
            UInt64.TryParse(processo.Item2, out UInt64 d);

            Console.WriteLine($"Concat-str: {c}");
            Console.WriteLine($"Concat-int: {s}");

            var r = 98 - (s % 97);

            Console.WriteLine($"Resultado: {d} == {r}");

            if (d == r)
                Console.WriteLine("Válido *** ");
        }
    }
}
