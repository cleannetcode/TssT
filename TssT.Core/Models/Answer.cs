namespace TssT.Core.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public int LevelKnowledgeId { get; set; }
    }
}