using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
//using System.Net.Mail;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using Microsoft.Exchange.WebServices.Data;
using Attachment = Microsoft.Exchange.WebServices.Data.Attachment;

namespace DigitalVaccination.Libs
{
    public class Mailer
    {
        public void SendMail()
        {
            try
            {
                
                
                ExchangeService service = new ExchangeService();

                service.Credentials = new NetworkCredential("user", "pass", "banglalink");
                service.Url = new System.Uri(@"https://mail.banglalinkgsm.com/EWS/Exchange.asmx");
                service.AutodiscoverUrl("abhossain@banglalinkgsm.com");
                EmailMessage message = new EmailMessage(service);

                message.Subject = "Test Mail";
                message.Body = "Auto Generated Test Mail";
                message.ToRecipients.Add("amahamudunnabi@banglalinkgsm.com");
                
               // message.Attachments.AddFileAttachment("");
                message.Send();




                //ExchangeService service = new ExchangeService();
                //service.Credentials = new WebCredentials("onm_iss", "password", "banglalink"); ; //new NetworkCredential("OnMSharingDB", "password","banglalinkgsm");
                //service.AutodiscoverUrl("onm_iss@banglalinkgsm.com");
                //service.ImpersonatedUserId = new ImpersonatedUserId(ConnectingIdType.SmtpAddress, "abhossain@banglalinkgsm.com");

                ////service.na
                //EmailMessage message = new EmailMessage(service);
                //message.From = "abhossain@banglalinkgsm.com";
                //message.Subject = "Test Mail";
                //message.Body = "Auto Generated Test Mail";
                //message.ToRecipients.Add("amahamudunnabi@banglalinkgsm.com");
                ////message.ToRecipients.Add("mdrahmed@banglalinkgsm.com");
                //message.Save();

                //message.SendAndSaveCopy();
                ////message.Send();
               
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }


}