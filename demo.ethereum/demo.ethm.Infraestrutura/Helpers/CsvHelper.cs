using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace demo.ethm.Infraestrutura.Helpers
{
    public interface IEscritorCsv
    {
        IEscritorCsv Faz(List<int> linhas);
        IEscritorCsv Fecha();
    }

    public class EscritorCsv : IEscritorCsv
    {
        private StreamWriter _sw;

        public EscritorCsv(string nomeDoArquivo)
        {
            var dir = Constants.ArquivoDeConfiguracao.CsvDir;
            var arquivo = dir + nomeDoArquivo;

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            if (!File.Exists(arquivo))
                _sw = new StreamWriter(arquivo);
            else
                _sw = File.AppendText(arquivo);
        }

        public IEscritorCsv Faz(List<int> linhas)
        {
            _sw.WriteLine(string.Join(",", linhas));
            return this;
        }

        public IEscritorCsv Fecha()
        {
            _sw.Close();
            return this;
        }
    }

    public interface ILeitorCsv
    {
        string Faz();
    }

    public class LeitorCsv : ILeitorCsv
    {
        private string _arquivo = string.Empty;

        public LeitorCsv(string nomeDoArquivo)
        {
            _arquivo = Constants.ArquivoDeConfiguracao.CsvDir + nomeDoArquivo;
        }

        public string Faz()
        {
            var sb = new StringBuilder();

            if (File.Exists(_arquivo))
            {
                using (StreamReader reader = new StreamReader(_arquivo))
                {
                    while (!reader.EndOfStream)
                    {
                        sb.Append(reader.ReadLine() + ",");
                    }
                }
            }

            return sb.ToString();
        }
    }
}
