using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TssT.TelegramBot.Models
{
    internal class Command: BotCommand
    {
        internal string Group { get; set; }

        internal Func<long,CancellationToken,Task> ExecuteAsync;

        internal Command(string commandText, string description, Func<long,CancellationToken,Task> func, string group = null)
        {
            Command = commandText;
            Description = description;
            Group = group;

            ExecuteAsync = func;
        }
    }
}