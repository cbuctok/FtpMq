namespace FtpMq
{
    using FluentFTP;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Net;

    internal static class Helper
    {
        internal static FtpClient ConnectToFtp(NameValueCollection appSettings)
        {
            var client = new FtpClient(appSettings["server"])
            {
                Port = int.Parse(appSettings["serverPort"]),
                Credentials = new NetworkCredential(appSettings["login"], appSettings["password"])
            };

            client.Connect();
            client.SetWorkingDirectory(appSettings["rootDir"]);
            return client;
        }

        internal class Command
        {
            public string FileName { get; set; } = $"{Guid.NewGuid().ToString()}.json";
            public List<string> Arguments { get; set; }
            public string Program { get; set; }
        }
    }
}