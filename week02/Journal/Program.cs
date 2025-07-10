using System;

class Program
{
    static void Main(string[] args)
    {
        PromptGenerator promptGenerator = new PromptGenerator();
        Journal myJournal = new Journal();

        // Simulate one journal entry
        string prompt = promptGenerator.GetRandomPrompt();
        Console.WriteLine(prompt);
        Console.Write("Your response: ");
        string response = Console.ReadLine();

        Entry newEntry = new Entry();
        newEntry._date = DateTime.Now.ToShortDateString();
        newEntry._prompt = prompt;
        newEntry._response = response;

        myJournal.AddEntry(newEntry);
        myJournal.DisplayAll();
    }
}
