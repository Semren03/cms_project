using System.Net;
using System.Net.Mail;
using cms_project.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace cms_project.Services
{
    public class EmailService(ApplicationDbContext context)
    {
      public  void Send(string toEmail,string subject,string body)
        {
            var emailSetting = context.EmailSettings.FirstOrDefault(x => x.Id == 1);
            if (emailSetting is null)
                return;
            // message => subject, body ,to ,from
            var message = new MailMessage(emailSetting.FromEmail, toEmail, subject,body);

            SmtpClient smtp = new SmtpClient(emailSetting.Host, emailSetting.Port);
 
            smtp.Credentials = new NetworkCredential(emailSetting.UserName, emailSetting.Password);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = emailSetting.EnableSsl;

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
