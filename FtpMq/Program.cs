namespace FtpMq
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Text;

    internal static class Program
    {
        private static void Main(string[] args)
        {
            var exampleCommand = new Helper.Command
            {
                Arguments = new List<string> { "upgrade", "-y" },
                Program = "apt"
            };

            var client = Helper.ConnectToFtp(ConfigurationManager.AppSettings);
            var files = client.GetListing();
            files.ToList().ForEach(file => Console.WriteLine(file));
            Console.ReadKey();

            Write(exampleCommand, client);
            client.Disconnect();
        }

        private static void Write(Helper.Command exampleCommand, FluentFTP.FtpClient client)
        {
            var commandJson = JsonConvert.SerializeObject(exampleCommand);
            using (Stream stream = client.OpenWrite(exampleCommand.FileName))
            {
                try
                {
                    stream.Write(Encoding.ASCII.GetBytes(commandJson));
                }
                finally
                {
                    stream.Close();
                }
            }
        }
    }
}