using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Net;
using System.Net.Mail;

namespace Company.S03.PL.Helpers
{
    public static class EmailSetting
    {
        public static bool SendEmail(Email email)
        {
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true; 
                client.Credentials = new NetworkCredential("abdonassar6689@gmail.com", "yxjxxjfgjrememix");
                client.Send("abdonassar6689@gmail.com", email.To, email.Subject, email.Body);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message); // أو log it
                return false;
            }
        }

        //public static bool SendEmail(Email email)
        //{
        //    try
        //    {
        //        var client = new SmtpClient("smtp.gmail.com", 587);
        //        client.EnableSsl = true;
        //        client.Credentials = new NetworkCredential("abdelrahmanemad765@gmail.com", "yxjxxjfgjrememix");

        //        client.Send("abdelrahmanemad765@gmail.com", email.To, email.Subject, email.Body);
        //        return true;
        //    }
        //    catch (Exception e)
        //    {              
        //        return false;
        //    }
        //}

        //public static bool SendEmail(Email email)
        //{
        //    //Mail Server : Gmail
        //    //SMTP 

        //    try
        //    {
        //        var client = new SmtpClient("smtp.gmail.com", 578);
        //        client.EnableSsl = true;
        //        client.Credentials = new NetworkCredential("abdelrahmanemad765@gmail.com", "cyhxyuhevrqnvfil");
        //        client.Send("abdelrahmanemad765@gmail.com", email.To, email.Subject, email.Body);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception                
        //        return false;
        //    }
        //}
    }
}
