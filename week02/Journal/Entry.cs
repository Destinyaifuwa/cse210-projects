using System;

public class Entry
{
    public string _date;
    public string _prompt;
    public string _response;

    // Displays a single journal entry
    public void Display()
    {
        Console.WriteLine($"Date: {_date}");
        Console.WriteLine($"Prompt: {_prompt}");
        Console.WriteLine($"Response: {_response}");
        Console.WriteLine();
    }
}
