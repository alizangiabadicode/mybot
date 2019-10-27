using System;
using System.Threading.Tasks;
using NetTelegramBotApi;
using NetTelegramBotApi.Requests;
using NetTelegramBotApi.Types;
namespace tele
{
    class Program
    {
        private const string Token = "1035888623:AAHcNqWECfZi2aN0Vb2vAmFuv29bh6RumkA";
        private static ReplyKeyboardMarkup rkstart;
        static void Main(string[] args)
        {
            rkstart = new ReplyKeyboardMarkup();
            rkstart.Keyboard = new[]
            {
                new[]
                {
                    new KeyboardButton("salam"), new KeyboardButton("khodafez")
                }
            };
            RunBot().Wait();
        }

        private async static Task RunBot()
        {
            var mybot = new TelegramBot(Token);
            var me = await mybot.MakeRequestAsync(new GetMe());
            Console.WriteLine("username : ", me.Username);
            long offset = 0;
            int whileCount = 0;
            while (true)
            {
                Write($"while count = {whileCount}");
                whileCount += 1;
                var updates = await mybot.MakeRequestAsync(new GetUpdates()
                {
                    Offset = offset
                });

                Write($"update count = {updates.Length}\n----------");

                try
                {
                    foreach (Update u in updates)
                    {
                        offset = u.UpdateId + 1;
                        var msg = u.Message.Text;
                        if(msg == "/start")
                        {
                            var req = new SendMessage(u.Message.Chat.Id, "this the response to /start");
                            req.ReplyMarkup = rkstart;
                            await mybot.MakeRequestAsync(req);
                        }
                        else if(msg != null && msg == "salam")
                        {
                            var req = new SendMessage(u.Message.Chat.Id, "this is the response to salam");
                            req.ReplyMarkup = rkstart;
                            await mybot.MakeRequestAsync(req);
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private static void Write(string msg)
        {
            Console.WriteLine(msg);
            
        }
    }
}
