namespace api.Models
{
    public class QuestionaireRequest
    {
        public required string Fullname { get; set; }
        public required IEnumerable<Question> Answer { get; set; }
    }

    public class QuestionaireResponse
    {
        public int Id { get; set; }
        public required string Text { get; set; }
        public required IEnumerable<QuestionOption> Options { get; set; }
        public string? AnswerId { get; set; }
    }
}
