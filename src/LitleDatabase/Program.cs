using System;
using System.IO;
using System.Net;
using System.Threading;
using LitleDatabase.Configuration;
using LitleDatabase.Server;
using Microsoft.Extensions.Configuration;

namespace LitleDatabase
{
    internal class Program
    {
        private static void Main()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);

            IConfigurationRoot configuration = builder.Build();

            var settings = new ServerConviguration();
            configuration.GetSection("Server").Bind(settings);


            // Определим нужное максимальное количество потоков
            int maxThreadsCount = Environment.ProcessorCount * 8;
            // Установим максимальное количество рабочих потоков
            ThreadPool.SetMaxThreads(maxThreadsCount, maxThreadsCount);
            // Установим минимальное количество рабочих потоков
            ThreadPool.SetMinThreads(2, 2);
            var localAddr = IPAddress.Parse(settings.Address);
            var serverThread = new LitleServerThread(localAddr, settings.Port, new DatabaseServerClient());
            serverThread.Start();
        }
    }
}