using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Elmah.Io.Examples.ApiClient.V2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Logging a new error...");
            var request = (HttpWebRequest)WebRequest.Create("https://elmah.io/api/v2/messages?logid=e9d63bbe-94e9-4949-9c6d-e74d0ab120e0");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            var createError = new
            {
                title = "This is a test message",
                application = "ApiClient.V2",
                detail = "This is a very long description telling more details about this message",
            };

            var createErrorString = JsonConvert.SerializeObject(createError);
            var bytes = Encoding.UTF8.GetBytes(createErrorString);
            request.ContentLength = bytes.Length;
            request.ContentType = "application/json";
            var outputStream = request.GetRequestStream();
            outputStream.Write(bytes, 0, bytes.Length);

            var response = request.GetResponse();
            var location = response.Headers[HttpResponseHeader.Location];

            Console.WriteLine("Successfully logged: {0}", location);
            Console.WriteLine("Loading the error...");

            request = (HttpWebRequest) WebRequest.Create(location);
            request.Method = "GET";
            response = request.GetResponse();

            string fullErrorJson;
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                fullErrorJson = streamReader.ReadToEnd();
            }

            Console.WriteLine("Successfully loaded: {0}", fullErrorJson);
            Console.WriteLine("Loading errors...");
            request = (HttpWebRequest)WebRequest.Create("https://elmah.io/api/v2/messages?logid=e9d63bbe-94e9-4949-9c6d-e74d0ab120e0");
            request.Method = "GET";
            response = request.GetResponse();

            string errorsJson;
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                errorsJson = streamReader.ReadToEnd();
            }

            Console.WriteLine("Successfully loaded: {0}", errorsJson);
            Console.ReadLine();
        }
    }
}
