using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LitleDatabase.Client;
using LiveServer.Client.Configuration;
using Microsoft.Extensions.Configuration;

namespace LiveServer.Client
{
    internal class Program
    {
        private const int Items = 1_000_000;
        private static readonly Random Random = new Random();

        private static void Main()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);

            var configuration = builder.Build();

            var settings = new ServerConviguration();
            configuration.GetSection("Server").Bind(settings);

            Console.WriteLine(configuration.GetConnectionString("Storage"));

            var addTasks = Enumerable.Range(0, 16)
                .Select(x => GenerateAddTask(settings.Address, settings.Port))
                .ToArray();
            Task.WaitAll(addTasks);
        }

        private static Task GenerateAddTask(string address, int port)
        {
            Thread.Sleep(Random.Next(10000));
            while(true)
            {
                using (var client = new DatabaseClient(address, port))
                {
                    var action = CreateAction(client);
                    var key = Random.Next(0, Items);
                    action(key);
                }
            }
        }

        private static Action<int> CreateAction(DatabaseClient client)
        {
            var randomItem = Random.Next(0, 110);

            if (randomItem < 80)
                return i => client.Post(i.ToString(), GetTitleById(i));

            if (randomItem < 95)
                return i => client.Delete(i.ToString());

            return i => client.Get(i.ToString());
        }

        private static string GetTitleById(int id)
        {
            return Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString() + Random.Next(0, id).ToString() + Random.Next(0, id).ToString() +
                   Random.Next(0, id).ToString();
        }
    }
}
