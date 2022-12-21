namespace ITHS.Domain.Models;

public class TheTriviaApiProblem {
    public string? id { get; set; }
    public string? category { get; set; }
    public string? correctAnswer { get; set; }
    public List<string>? incorrectAnswers { get; set; }
    public string? question { get; set; }
}
