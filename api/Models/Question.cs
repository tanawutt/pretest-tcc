namespace api.Models
{
    public class Question
    {
        public int Id { get; set; }
        public required string Text { get; set; }

        public int AnswerId { get; set; }
    }
}
