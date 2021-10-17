using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TssT.Api.Contracts;
using TssT.TelegramBot.Common;
using TssT.TelegramBot.Exceptions;
using TssT.TelegramBot.Models;

namespace TssT.TelegramBot.Services
{
    internal class CommandsService
    {
        private readonly ITelegramBotClient _botClient;
        private readonly ApiClient.ApiClient _apiClient;
        
        internal readonly Dictionary<string, Command> Commands;

        private BotCommand _lastCommand;
        
        private readonly Dictionary<long, List<QuestionAnswer>> _questions = new ();

        internal CommandsService(ApiClient.ApiClient apiClient, ITelegramBotClient botClient)
        {
            _apiClient = apiClient;
            _botClient = botClient;

            #region Init commands

            var commandsList = new List<Command>
            {
                new(CommandsConstants.Start, "справка по командам бота", StartAsync),
                new(CommandsConstants.CreateNewTest, "создание нового теста", CreateNewTestAsync, "Тесты"),
                new(CommandsConstants.GetTests, "получить все тесты", GetTestsAsync, "Тесты"),
                new(CommandsConstants.SignUp,"Регистрация",SignUpAsync,"Учетная запись"),
                new(CommandsConstants.SignIn,"Авторизация",SignInAsync,"Учетная запись")
            };

            Commands = commandsList.ToDictionary(x => x.Command);

            #endregion
        }
        
        internal async Task ExecuteAsync(string botCommand, long chatId, CancellationToken cancellationToken = default)
        {
            try
            {
                if (!Commands.ContainsKey(botCommand))
                    throw new CommandNotFoundException();

                await Commands[botCommand].ExecuteAsync(chatId, cancellationToken);
                _lastCommand = Commands[botCommand];
            }
            catch (CommandNotFoundException)
            {
                if (_questions.ContainsKey(chatId))
                {
                    var noAnswerQuestionIndex = _questions[chatId].FindIndex(x => x.Answer == null);

                    if (noAnswerQuestionIndex >= default(int))
                        _questions[chatId][noAnswerQuestionIndex].Answer = botCommand;

                    await ExecuteAsync(_lastCommand.Command, chatId, cancellationToken);
                }
                else await SendErrorMessageAsync(chatId, "Команда не распознана", cancellationToken);
            }
            catch (HttpResponseException e)
            {
                await SendErrorMessageAsync(chatId, e.Message, cancellationToken);
            }
            catch (Exception e)
            {
                await SendErrorMessageAsync(chatId, e.Message, cancellationToken);
            }
        }

        private async Task SendErrorMessageAsync(long chatId, string message, CancellationToken cancellationToken = default)
        {
            message = $"*Во время выполнения команды произошла ошибка*\n{Regex.Escape(message).Replace("=","\\=")}";
            await _botClient.SendTextMessageAsync(chatId, message, ParseMode.MarkdownV2, cancellationToken: cancellationToken);
        }

        private async Task StartAsync(long chatId, CancellationToken cancellationToken = default)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append("Добро пожаловать в *Систему опросов по технологиям*\n");

            if (Commands.Count > 0)
            {
                var commandGroups = Commands.Values
                    .GroupBy(x => x.Group)
                    .OrderBy(x=>x.Key);

                foreach (var group in commandGroups)
                {
                    stringBuilder.Append('\n');
                    
                    if (!string.IsNullOrWhiteSpace(group.Key))
                        stringBuilder.Append($"\n*{group.Key}*\n");
                    
                    var commands = group.Select(x => $"{x.Command} \\- {x.Description}");
                    stringBuilder.Append(string.Join('\n', commands));
                }
            }
            
            await _botClient.SendTextMessageAsync(
                chatId, 
                stringBuilder.ToString(),
                ParseMode.MarkdownV2,
                cancellationToken: cancellationToken);
        }

        private async Task CreateNewTestAsync(long chatId, CancellationToken cancellationToken = default)
        {
            QuestionAnswer noAnswerQuestion;
            
            if (_questions.ContainsKey(chatId))
            {
                noAnswerQuestion = _questions[chatId].FirstOrDefault(x => x.Answer == null);

                if (noAnswerQuestion != null)
                {
                    await _botClient.SendTextMessageAsync(
                        chatId, 
                        noAnswerQuestion.Question,
                        cancellationToken: cancellationToken);
                    return;
                }
            }
            
            if (!_questions.ContainsKey(chatId))
            {
                _questions.Add(chatId, new List<QuestionAnswer>());
                _questions[chatId].AddRange(new QuestionAnswer[]
                {
                    new ("Задайте имя новому тесту"),
                    new ("Описание для нового теста"),
                    new ("Список топиков, каждый на новой строке")
                });
            }
            
            noAnswerQuestion = _questions[chatId].FirstOrDefault(x => x.Answer == null);
            
            if (noAnswerQuestion is not null)
                await _botClient.SendTextMessageAsync(chatId, noAnswerQuestion.Question, cancellationToken: cancellationToken);
            else
            {
                var questions = _questions[chatId];
                
                var newTest = new NewTest
                {
                    Name = questions[0].Answer,
                    Description = questions[1].Answer,
                    Topics = questions[2].Answer?
                        .Split('\n')
                        .ToList()
                };
                
                var createResponse = await _apiClient.Tests.CreateAsync(newTest, cancellationToken);
                
                await _botClient.SendTextMessageAsync(
                    chatId, 
                    $"Тест сохранен под индентификатором {createResponse.Id}", 
                    cancellationToken: cancellationToken);
                
                _questions.Remove(chatId);
            }
        }

        private async Task GetTestsAsync(long chatId, CancellationToken cancellationToken = default)
        {
            var getAllTestsResponse = await _apiClient.Tests.GetAsync(cancellationToken);

            var sendTextMessage =
                (getAllTestsResponse.TotalCount > 0)
                    ? string.Join('\n', getAllTestsResponse.Items.Select(x => x.Name))
                    : "Нет результатов";

            sendTextMessage = $"Доступные тесты:\n{sendTextMessage}";

            await _botClient.SendTextMessageAsync(
                chatId, 
                sendTextMessage, 
                cancellationToken: cancellationToken);
        }

        private async Task SignInAsync(long chatId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        
        private async Task SignUpAsync(long chatId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}