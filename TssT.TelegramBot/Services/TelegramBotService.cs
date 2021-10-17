using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TssT.TelegramBot.Services
{
    public class TelegramBotService
    {
        public delegate void DefaultEventHandler (string message);
        public event DefaultEventHandler OnMessage;
        public event DefaultEventHandler OnError;
        
        private readonly ITelegramBotClient _botClient;
        private readonly CommandsService _commandsService;
        
        public TelegramBotService(string telegramBotToken, string apiUrl)
        {
            _botClient = new TelegramBotClient(telegramBotToken);
            
            var apiClient = new ApiClient.ApiClient(apiUrl);
            _commandsService = new CommandsService(apiClient, _botClient);
        }

        public async Task StartAsync()
        {
            await _botClient.SetMyCommandsAsync(_commandsService.Commands.Values);
            
            OnMessage?.Invoke("Старт бот-сервиса");
            _botClient.StartReceiving(new DefaultUpdateHandler(UpdateHandlerAsync, ErrorHandlerAsync));
        }

        private async Task UpdateHandlerAsync(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    await UpdateHandlerTextMessage(update, cancellationToken);
                    break;
                
                case UpdateType.CallbackQuery:
                    await CallbackQueryHandler(update, cancellationToken);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private async Task CallbackQueryHandler(Update update, CancellationToken cancellationToken)
        {
            var chatId = update.CallbackQuery?.Message?.Chat.Id;

            if (chatId.HasValue)
                await _commandsService.ExecuteAsync(
                    update.CallbackQuery?.Data, 
                    chatId.Value, 
                    cancellationToken);
        }

        private async Task UpdateHandlerTextMessage(Update update, CancellationToken cancellationToken)
        {
            if (update.Type != UpdateType.Message || update.Message?.Type != MessageType.Text)
                return;
            
            var chatId = update.Message.Chat.Id;
            OnMessage?.Invoke($"Received a '{update.Message.Text}' message in chat {chatId}.");
            
            await _commandsService.ExecuteAsync(update.Message.Text, chatId, cancellationToken);
        }

        private Task ErrorHandlerAsync(ITelegramBotClient client, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException => 
                    $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}", 
                        _ => exception.ToString()
            };
            
            OnError?.Invoke(errorMessage);
            return Task.CompletedTask;
        }
    }
}