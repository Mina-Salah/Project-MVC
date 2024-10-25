using System.Net;
using System.Net.Mail;

namespace DEMO.PL.Helper
{
    public class EmailSiting
    {

        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com",587);
            
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("new14121998@gmail.com", "cqkq zbcq hqfg lkdc");
            client.Send("new14121998@gmail.com",email.To,email.Title,email.Body);

        }
    }
}
