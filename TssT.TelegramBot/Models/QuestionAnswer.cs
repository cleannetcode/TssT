namespace TssT.TelegramBot.Models
{
    internal class QuestionAnswer
    {
        public string Question { get; }
        public string Answer { get; set; }

        internal QuestionAnswer(string question, string answer = null)
        {
            Question = question;
            Answer = answer;
        }
    }
}