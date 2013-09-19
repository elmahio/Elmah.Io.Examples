using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Elmah.Io.Examples.ApiClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Logging a new error...");
            var request = (HttpWebRequest)WebRequest.Create("http://localhost:54212/api/errors?logid=9732b804-a82e-4537-a280-114dfe4de375");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            var elmahError = new Error(new ApplicationException());
            var errorString = ErrorXml.EncodeString(elmahError);
            var bytes = Encoding.UTF8.GetBytes("=" + errorString);
            request.ContentLength = bytes.Length;
            var outputStream = request.GetRequestStream();
            outputStream.Write(bytes, 0, bytes.Length);

            var response = request.GetResponse();
            string errorJson;
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                errorJson = streamReader.ReadToEnd();
            }

            Console.WriteLine("Successfully logged: {0}", errorJson);
            Console.WriteLine("Loading the error...");

            dynamic error = JsonConvert.DeserializeObject(errorJson);
            request = (HttpWebRequest)WebRequest.Create("http://localhost:54212/api/errors?id=" + error.Id + "&logid=9732b804-a82e-4537-a280-114dfe4de375");
            request.Method = "GET";
            response = request.GetResponse();

            string fullErrorJson;
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                fullErrorJson = streamReader.ReadToEnd();
            }

            Console.WriteLine("Successfully loaded: " + fullErrorJson);
            Console.WriteLine("Loading errors...");
            request = (HttpWebRequest)WebRequest.Create("http://localhost:54212/api/errors?logid=9732b804-a82e-4537-a280-114dfe4de375");
            request.Method = "GET";
            response = request.GetResponse();

            string errorsJson;
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                errorsJson = streamReader.ReadToEnd();
            }

            Console.WriteLine("Successfully loaded: " + errorsJson);
            Console.ReadLine();
        }
    }
}
