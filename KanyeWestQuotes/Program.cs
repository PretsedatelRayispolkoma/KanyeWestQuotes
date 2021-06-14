using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace KanyeWestQuotes
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = $"https://api.kanye.rest";

            var request = WebRequest.Create(url);

            var response = request.GetResponse();
            var httpStatusCode = (response as HttpWebResponse).StatusCode;

            if (httpStatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine(httpStatusCode);
                return;
            }

            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                string result = streamReader.ReadToEnd();
                var yeQuote = JsonConvert.DeserializeObject<Root>(result);
                Console.WriteLine(yeQuote.quote);
            }

        }
    }
}
