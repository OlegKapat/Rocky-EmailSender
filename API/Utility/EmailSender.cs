using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Newtonsoft.Json.Linq;

namespace API.Utility
{
    public class EmailSender : IEmailSender
    
    {
        private  MailJetSettings _mailJetSettings;
        private readonly IConfiguration _configuration;
        public EmailSender(MailJetSettings mailJetSettings,IConfiguration configuration)
        {
           
            _configuration = configuration;
        }
       
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(email, subject, htmlMessage);
        }
        public async Task Execute(string email, string subject, string body)
        {
           _mailJetSettings = _configuration.GetSection("MailJet").Get<MailJetSettings>();

            MailjetClient client = new MailjetClient(_mailJetSettings.ApiKey, _mailJetSettings.SecretKey);

            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
            .Property(Send.Messages, new JArray {
                new JObject {
                    {"From", new JObject {
                        {"Email", "pilot@mailjet.com"},
                        {"Name", "Mailjet Pilot"}
                       
                    }},
                    {"To", new JArray {
                        new JObject {
                            {"Email", "send@mail.com"},
                            {"Name", "Vlad the Impaler"}
                        }
                    }},
                    {"Subject", "Your email flight plan!"}, 
                    {"TextPart", "Dear passenger, welcome to Mailjet! May the delivery force be with you!"},
                    {"HTMLPart",  "<h3>Dear passenger 1, welcome to <a href=\"https://www.mailjet.com/\">Mailjet</a>!</h3><br />May the delivery force be with you!"},
                    {"CustomID", "AppGettingStartedTest"}

                   
                }
            });
        }
    
}
}