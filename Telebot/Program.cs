using System;
using System.IO;
using Telegram.Bot;
using Newtonsoft.Json;
using System.Net;

namespace TelegramBot
{
    class Program
    {
        static void Main(string[] args)
        {

            TelegramBotClient bot = new TelegramBotClient("5085818592:AAF4P4GUksDleR37VdtVT8rdR3fZgN-YZio");

            bot.OnMessage += (s, arg) =>
            {
                if(arg.Message.Text == "/getquot")
                {
                    Console.WriteLine($"{arg.Message.Chat.FirstName}: {arg.Message.Text}");
                    bot.SendTextMessageAsync(arg.Message.Chat.Id, $"Your Yeezus quote is ''{GetQuote(arg.Message.Text)}''");
                }
                else
                {
                    bot.SendTextMessageAsync(arg.Message.Chat.Id, "Unknown command");
                }
                
            };

            bot.StartReceiving();

            Console.ReadKey();
        }

        static string GetQuote(string s )
        {
            var url = $"https://api.kanye.rest";

            var request = WebRequest.Create(url);

            var response = request.GetResponse();
            var httpStatusCode = (response as HttpWebResponse).StatusCode;

            if (httpStatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine(httpStatusCode);
                return "Error";
            }

            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                string result = streamReader.ReadToEnd();
                var yeQuote = JsonConvert.DeserializeObject<Root>(result);
                return $"{yeQuote.quote}";
            }
        }
    }
}
