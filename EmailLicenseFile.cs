using System;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace pluralsight_azurefunctions
{
    public class EmailLicenseFile
    {
        [FunctionName("EmailLicenseFile")]
        public void Run(
            [BlobTrigger("licenses/{name}", Connection = "AzureWebJobsStorage")]string licenseFileContent, 
            string name, 
            ILogger log)
        {
            var email = Regex.Match(licenseFileContent, @"^Email\:\ (.+)$", RegexOptions.Multiline).Groups[1].Value;
            log.LogInformation($"Got order from {email}\n License file Name:{name}");
        }
    }
}
