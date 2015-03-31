using System;
using Elmah.Io.Client;

namespace Elmah.Io.Examples.LoggerClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Logging a new messsage...");
            var logger = new Logger(new Guid("cc6043e9-5d7b-4986-8056-cb76d4d52e5e"));
            var id = logger.Log(new Message("Message"));

            Console.WriteLine("Successfully logged: {0}", id);
            Console.WriteLine("Loading the message...");

            var message = logger.GetMessage(id);

            Console.WriteLine("Successfully loaded: {0}", message.Title);
            Console.WriteLine("Loading messages...");

            var messages = logger.GetMessages(0, 15);
            Console.WriteLine("Successfully loaded: {0} ", messages.Total);
            Console.ReadLine();
        }
    }
}
