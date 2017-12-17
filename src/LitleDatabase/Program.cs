using System;
using System.Threading;
using LitleDatabase.Server;

namespace LitleDatabase
{
    internal class Program
    {
        private static void Main()
        {

            // Определим нужное максимальное количество потоков
            // Пусть будет по 4 на каждый процессор
            int maxThreadsCount = Environment.ProcessorCount * 4;
            // Установим максимальное количество рабочих потоков
            ThreadPool.SetMaxThreads(maxThreadsCount, maxThreadsCount);
            // Установим минимальное количество рабочих потоков
            ThreadPool.SetMinThreads(2, 2);
            var serverThread = new LitleServerThread(8080, new DatabaseServerClient());
            serverThread.Start();
        }
    }
}