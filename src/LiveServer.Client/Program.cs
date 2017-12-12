using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LitleDatabase.Core.Client;

namespace LiveServer.Client
{
    internal class Program
    {
        private const int Items = 10_000_000;
        private static readonly Random Random = new Random();

        private static void Main()
        {
            var addTasks = Enumerable.Range(0, 16)
                .Select(x => GenerateAddTask())
                .ToArray();
            Task.WaitAll(addTasks);
        }

        private static Task GenerateAddTask()
        {
            Thread.Sleep(Random.Next(10000));
            while(true)
            {
                using (var client = new DatabaseClient("127.0.0.1", 8080))
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

            if (randomItem < 75)
                return i => client.Post(i.ToString(), i.ToString());

            if (randomItem < 95)
                return i => client.Delete(i.ToString());

            return i => client.Get(i.ToString());
        }
    }
}
