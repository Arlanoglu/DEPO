using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace MVCMailSender.Utils
{
    public class MailSender
    {
        //MailSender
        //Email: yzl3153ba@gmail.com
        //Şifre: Yzl3153--
        public static void SendEmail(string email, string subject, string message)
        {
            //MailMessage
            MailMessage sender = new MailMessage();
            sender.From = new MailAddress("yzl3153ba@gmail.com", "YZL3153");
            sender.To.Add(email);
            sender.Subject = subject;
            sender.Body = message;

            //SmtpClient
            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential("yzl3153ba@gmail.com", "Yzl3153--");
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;

            smtp.Send(sender);

            //NOT!!!!
            //Eğer tanımlı gmail hesabınız üzerinden mail göndermek istediğinizde bir hata ile karşılaşıyorsanız aşağıdaki link üzerinden izin tanımlaması gerçekleştirmeniz gerekmetedir.
            //https://www.google.com/settings/security/lesssecureapps

        }
    }
}