using System.Web;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace Elmah.Io.Examples.ApiClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Logging a new error...");
            var request = (HttpWebRequest)WebRequest.Create("https://elmah.io/api/errors?logid=cc6043e9-5d7b-4986-8056-cb76d4d52e5e");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            var elmahError = new Error(new ApplicationException());
            var errorString = ErrorXml.EncodeString(elmahError);
            var errorStringEncoded = HttpUtility.UrlEncode(errorString);
            var bytes = Encoding.UTF8.GetBytes("=" + errorStringEncoded);
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
            request = (HttpWebRequest)WebRequest.Create("https://elmah.io/api/errors?id=" + error.Id + "&logid=cc6043e9-5d7b-4986-8056-cb76d4d52e5e");
            request.Method = "GET";
            response = request.GetResponse();

            string fullErrorJson;
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                fullErrorJson = streamReader.ReadToEnd();
            }

            Console.WriteLine("Successfully loaded: {0}", fullErrorJson);
            Console.WriteLine("Loading errors...");
            request = (HttpWebRequest)WebRequest.Create("https://elmah.io/api/errors?logid=cc6043e9-5d7b-4986-8056-cb76d4d52e5e");
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
