using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BiletVarmi
{
    public static class EmailSend
    {

        public static void email_send(string message, string email)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("yourMailAdress@gmail.com");
                mail.To.Add(email);
                mail.Subject = "Yer Buldummmm!!!!";
                mail.Body = message;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("yourMailAdress@gmail.com", "password");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }

            }
        }
    }
}




