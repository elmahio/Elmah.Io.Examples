using System;
using System.Collections;

namespace Elmah.Io.Examples.ErrorLogApiClient
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Logging a new error...");
            var errorLog = new ErrorLog(new Hashtable { { "LogId", new Guid("cc6043e9-5d7b-4986-8056-cb76d4d52e5e") } });
            var elmahError = new Error(new ApplicationException());
            var id = errorLog.Log(elmahError);

            Console.WriteLine("Successfully logged: {0}", id);
            Console.WriteLine("Loading the error...");

            var errorLogEntry = errorLog.GetError(id);

            Console.WriteLine("Successfully loaded: {0}", errorLogEntry.Error);
            Console.WriteLine("Loading errors...");

            var errorEntryList = new ArrayList();
            errorLog.GetErrors(0, 15, errorEntryList);
            Console.WriteLine("Successfully loaded: {0} ", errorEntryList.Count);
            Console.ReadLine();
        }
    }
}
