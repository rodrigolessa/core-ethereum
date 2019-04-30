using System;
using System.Collections.Generic;
using System.Text;

namespace demo.ethm.Infraestrutura
{
    public class Constants
    {
        #region Geral

        public static class Geral
        {
            public static class Acao
            {
                public const string Alterar = "Alterar";
                public const string Excluir = "Excluir";
                public const string Incluir = "Incluir";
                public const string Consultar = "Consultar";
                public const string EnviarEmail = "Enviar E-mail";
            }

            public static class Sistema
            {
                public const string Demo = "Demo";

                public static class SubSistema
                {
                    public const string Contratos = "Contratos";

                    public static class Modulo
                    {
                        public const string RegistroDeObras = "Registro de Obras";
                    }
                }
            }
        }

        #endregion

        #region Configuração

        public static class ArquivoDeConfiguracao
        {
            public static class Default
            {
                // TODO: Solicitar criação de e-mails
                public static string Email = "team@demo.ethm.com";
                public static string EmailSuporte = "team@demo.ethm.com";
                public static string EmailComercial = "team@demo.ethm.com";
                public static string EmailFinanceiro = "team@demo.ethm.com";

                // Controle de erros por e-mail
                public static string EmailToWarning = "team@demo.ethm.com";
            }

            public static string BaseDir = System.AppDomain.CurrentDomain.BaseDirectory;
            public static string CsvDir = BaseDir + @"\csv\";
            public static string LogDir = BaseDir + @"\log\";

            public static string ArquivoLogErro = "log_de_erro_em_" + System.DateTime.Now.ToString("yyyy_MM_dd") + ".txt";
        }

        #endregion
    }
}
