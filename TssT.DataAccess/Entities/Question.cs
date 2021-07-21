namespace TssT.DataAccess.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int LevelImportanceId { get; set;  }
    }
}