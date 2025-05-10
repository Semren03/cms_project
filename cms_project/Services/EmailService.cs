using System.Net;
using System.Net.Mail;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace cms_project.Services
{
    public class EmailService
    {
      public  void Send(string toEmail,string subject,string body)
        {
            // message => subject, body ,to ,from
            var message = new MailMessage("3220601094@std.wise.edu.jo", "ahmadsk1921@gmail.com", subject,body);

            SmtpClient smtp = new SmtpClient("smtp-mail.outlook.com", 587);
 
            smtp.Credentials = new NetworkCredential("3220601094@std.wise.edu.jo", "Ahmad123$");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(message);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }



        }
    }
}
