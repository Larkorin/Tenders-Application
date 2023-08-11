using DTOs;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;

namespace SistemaProveeduriaCORE
{
    public class NotificationManager
    {
        public void Notify(Usuario user) { }
        public async void NotifyUserByEmail(string otp, string mail)
        {
            UserExecute(otp, mail).Wait();
        }
        public void NotifyUserBySMS(string otp, string phoneNumber)
        {
            // Find your Account SID and Auth Token at twilio.com/console
            // and set the environment variables. See http://twil.io/secure
            string accountSid = "AC9ca21e4538967dceaed6877d7bf4482c";
            string authToken = "bc229000f60a2530dd7c9f6d0c192558";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: "El OTP es: " + otp,
                from: new Twilio.Types.PhoneNumber("+12674154231"),
                to: new Twilio.Types.PhoneNumber("+" + phoneNumber)
            );

            Console.WriteLine(message.Sid);

        }
        static async Task UserExecute(string otp, string mail)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("fcastror@ucenfotec.ac.cr", "Franklin Castro");
            var subject = "Creacion de la cuenta";
            var to = new EmailAddress(mail, "Usuario");
            var plainTextContent = $"Hola, el codigo es {otp}";
            var htmlContent = $"Hola, el codigo es {otp}";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}