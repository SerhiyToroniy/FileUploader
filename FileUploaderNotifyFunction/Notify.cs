using System;
using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FileUploaderNotifyFunction
{
    public class Notify
    {
        [FunctionName("Notify")]
        public void Run([BlobTrigger("files/{name}", Connection = "notify_uploaded")] Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
        }
    }
}
