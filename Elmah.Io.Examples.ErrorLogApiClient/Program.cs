using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elmah.Io.Examples.ErrorLogApiClient
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Logging a new error...");
            var errorLog = new ErrorLog(new Guid("9732b804-a82e-4537-a280-114dfe4de375"));
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
