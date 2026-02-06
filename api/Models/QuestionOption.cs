namespace api.Models
{
    public class QuestionOption
    {
        public int Id { get; set; }
        public required string Text { get; set; }

        public int QuestionId { get; set; }
    }
}
