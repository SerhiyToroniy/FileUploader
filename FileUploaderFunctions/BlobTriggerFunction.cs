using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;
using SendGrid;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;

namespace FileUploaderFunctions
{
    public class BlobTriggerFunction
    {
        [FunctionName("BlobTriggerFunction")]
        public void Run([BlobTrigger("files/{name}", Connection = "Default")] Stream myBlob, string name, ILogger log, IDictionary<string, string> metadata)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");

            if (metadata.TryGetValue("email", out var email))
            {
                log.LogInformation($"Blob uploaded by {email}");

                // Set up the email message
                MailMessage message = new();
                message.From = new MailAddress("toroniyserhiy@gmail.com", "FileUploader by Serhiy Toroniy");
                message.To.Add(email);
                message.Body = "Your file is successfully uploaded!";

                // Set up the SMTP client
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("toroniyserhiy@gmail.com", "lzercyxhmgharuwo");
                smtpClient.EnableSsl = true;

                // Send the email
                smtpClient.Send(message);
            }
        }

    }
}
