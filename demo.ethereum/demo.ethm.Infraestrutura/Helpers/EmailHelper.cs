using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace demo.ethm.Infraestrutura.Helpers
{
    public class SmtpConfig
    {
        public string Server { get; set; }
        public string User { get; set; }
        public string Pass { get; set; }
        public int Port { get; set; }
    }

    public class EmailHelper
    {
        private readonly SmtpClient _smtpServer;

        public EmailHelper()
        {
            _smtpServer = new SmtpClient();
        }

        public EmailHelper(string smtp, bool ssl, int porta, string usuario, string senha)
        {
            _smtpServer = new SmtpClient(smtp)
            {
                Port = porta,
                Credentials = new System.Net.NetworkCredential(usuario, senha),
                EnableSsl = ssl
            };
        }

        public bool Enviar(string de, string para, string assunto, string corpo)
        {
            return Enviar(de, para, string.Empty, string.Empty, assunto, corpo, null);
        }

        public bool Enviar(string de, string para, string assunto, string corpo, Dictionary<string, byte[]> anexos)
        {
            return Enviar(de, para, string.Empty, string.Empty, assunto, corpo, anexos);
        }

        public bool Enviar(string de, string para, string cc, string cco, string assunto, string corpo, Dictionary<string, byte[]> anexos)
        {
            try
            {
                MemoryStream stream = null;
                var mail = new MailMessage();

                mail.From = new MailAddress(de);
                mail.To.Add(para.Replace(";", ","));

                if (cc != string.Empty)
                    mail.CC.Add(cc.Replace(";", ","));

                if (cco != string.Empty)
                    mail.Bcc.Add(cco.Replace(";", ","));

                if (anexos != null)
                {
                    foreach (var anexo in anexos)
                    {
                        stream = new MemoryStream(anexo.Value);
                        mail.Attachments.Add(new Attachment(stream, anexo.Key));
                    }
                }

                mail.Subject = assunto;
                mail.IsBodyHtml = true;
                mail.Body = corpo;

                _smtpServer.Send(mail);

                mail.Dispose();

                if (stream != null)
                {
                    stream.Dispose();
                }

                return true;
            }
            catch (Exception)
            {
                // TODO: Log error
                return false;
            }
        }
    }
}
