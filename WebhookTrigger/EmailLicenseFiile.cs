using System;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;

namespace WebhookTrigger
{
    public static class EmailLicenseFiile
    {
        [FunctionName("EmailLicenseFiile")]
        public static void Run([BlobTrigger("licenses/{name}", Connection = "AzureWebJobsStorage")] string licenseFileContent,
        [SendGrid(ApiKey = "SendGridApiKey")] out SendGridMessage message,
        string name,
        ILogger log)
        {
            //var email = Regex.Match(licenseFileContent, @"^Email\:\(.+)$", RegexOptions.Multiline).Groups[1].Value;
            var email ="shahabazure@gmail.com";
            log.LogInformation($"Got order from {email}\n License file name:{name} ");

            message = new SendGridMessage();
            message.From = new EmailAddress(Environment.GetEnvironmentVariable("EmailSender"));
            message.AddTo(email);
            var plainTestBytes = System.Text.Encoding.UTF8.GetBytes(licenseFileContent);
            var base64 = Convert.ToBase64String(plainTestBytes);
            message.AddAttachment(name, base64, "text/plain");
            message.Subject = "Your license file";
            message.HtmlContent = "Thank you for your order";
        }
    }
}
