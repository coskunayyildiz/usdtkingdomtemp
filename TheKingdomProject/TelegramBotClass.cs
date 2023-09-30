using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace TheKingdomProject
{
    internal class TelegramBotClass
    {
        CancellationTokenSource cts = new CancellationTokenSource();

        public void SendMessageNormal()
        {
            string botToken = "6404752431:AAHoHYXdI6pae_1qUj-c7djXRGa8PKdxGvc";
            long groupId = -940383798; // https://api.telegram.org/bot6404752431:AAHoHYXdI6pae_1qUj-c7djXRGa8PKdxGvc/getUpdates

            var botClient = new TelegramBotClient(botToken);

            string messageText = "Krallara gunaydin. Bu mesaji Kingdom yazilimi gonderiyor!!! Hedef 1B$ amkk";

            try
            {
                botClient.SendTextMessageAsync(groupId, messageText, (int)ParseMode.Html);
                Console.WriteLine("Message sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        public async Task SendMessage(string message)
        {
            string botToken = "6404752431:AAHoHYXdI6pae_1qUj-c7djXRGa8PKdxGvc";
            long groupId = -940383798; // https://api.telegram.org/bot6404752431:AAHoHYXdI6pae_1qUj-c7djXRGa8PKdxGvc/getUpdates

            var botClient = new TelegramBotClient(botToken);

            try
            {
                //await botClient.SendTextMessageAsync(groupId, messageText, (int)ParseMode.Html);
                await botClient.SendTextMessageAsync(groupId, message);
                Console.WriteLine("Message sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
