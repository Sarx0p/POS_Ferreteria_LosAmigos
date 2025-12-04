using Proyecto_POSFerreteria.Datos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_POSFerreteria.Utilidades
{
    public class MailService
    {
         private readonly string host;
            private readonly int port;
            private readonly string user;
            private readonly string pass;
            private readonly string from;

            public MailService(string host, int port, string user, string pass, string from)
            {
                this.host = host;
                this.port = port;
                this.user = user;
                this.pass = pass;
                this.from = from;
            }

        // metodos para el correo
        private static readonly string smtpHost = "smtp.gmail.com";
        private static readonly int smtpPort = 587;
        private static readonly string smtpUser = "ferreterialosamigossv@gmail.com";
        private static readonly string smtpPass = "rhew obnv wcag lscb"; // clave APP
        private static readonly string fromAddress = "ferreterialosamigossv@gmail.com";

        public void Send(string to, string subject, string body, bool isHtml = false)
        {
            var msg = new MailMessage();
            msg.From = new MailAddress(from);
            msg.To.Add(new MailAddress(to));
            msg.Subject = subject;
            msg.Body = body;
            msg.IsBodyHtml = isHtml;

            using (var smtp = new SmtpClient(host, port))
            {
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential(user, pass);
                smtp.Send(msg);
            }
        }
    }
}


        // Configuración SMTP (pon tu App Password en smtpPass)
        // readonly string smtppass = "jtgc ceve fqxk fdhv";



