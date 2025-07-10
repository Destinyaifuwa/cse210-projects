public class PromptGenerator
{
    private List<string> _prompts = new List<string>()
    {
        "What made you smile today?",
        "What did you learn today?",
        "What are you grateful for?",
        "Describe a challenge you faced.",
        "What are your goals for tomorrow?"
    };

    // Returns a random prompt from the list
    public string GetRandomPrompt()
    {
        Random rand = new Random();
        int index = rand.Next(_prompts.Count);
        return _prompts[index];
    }
}